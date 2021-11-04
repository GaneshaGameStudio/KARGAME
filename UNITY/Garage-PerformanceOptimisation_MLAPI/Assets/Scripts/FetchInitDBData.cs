using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using System;
using TMPro;


public class FetchInitDBData : MonoBehaviour
{
    private string apiUrl = "http://kardb.kargame.in/";
    private Init initData;
    private bool tokenAuthenticated = false;
    private string dbTimestamp;
    public static string apiToken;
    private string apiTokenData;
    private string playerPrefsTimestamp;

    private string unityUserID;
    private JSONNode dbInitData;
    private JSONNode tokenData;
    private JSONNode tokenAuthData;
    private string NetworkCheck = "";
    public TextMeshProUGUI Playername;
    public TextMeshProUGUI Money;
    public TextMeshProUGUI TotalDistance;
    public TextMeshProUGUI FoodPrice;
    public TextMeshProUGUI PetrolPrice;
    public TextMeshProUGUI LifePrice;
    // public Translater Translate;

    public string id = "1";
    public GameObject[] Objectlistshow;
    public GameObject[] Objectlisthide;
    SimpleJSON.JSONObject playerJson = new SimpleJSON.JSONObject();
    Dictionary<string,string> stringValues = new Dictionary<string, string>();
    Dictionary<string,int> intValues = new Dictionary<string, int>();
    Dictionary<string,float> floatValues = new Dictionary<string, float>();

    Dictionary<string,string> stringVehicleValues = new Dictionary<string, string>();
    Dictionary<string,int> intVehicleValues = new Dictionary<string, int>();
    Dictionary<string,float> floatVehicleValues = new Dictionary<string, float>();


    // Start is called before the first frame update
    void Start()
    {   
        HideTheseObjects();
        Init initDat = new Init();
        StartCoroutine(initDat.Download(apiUrl, result => {
            NetworkCheck = result;
            // Debug.Log(NetworkCheck);
        }));
        SetStatsInClassroom();
    }

    public void GetDBDataOnClick()
    {
        // Debug.Log(NetworkCheck);
        bool skipTimeCheck = false;
        string playerID="0";
        unityUserID = PlayerPrefs.GetString("unity.cloud_userid");
        //Check if playerID is 0 in playerprefs and initialize all PP with default values
        if (PlayerPrefs.HasKey("PlayerID"))
        {
            Debug.Log("The key PlayerID exists");
            playerID = PlayerPrefs.GetString("PlayerID");
        }
        else{
            // Debug.Log("The key PlayerID does not exist "+playerID);
            //Do nothing
        }
            
        
        initData = new Init();
        
        if(playerID.Equals("0")){
            if(NetworkCheck != null){
                PlayerPrefs.SetString("PlayerID",unityUserID);
            }

            allDefaultPPData();

            initData.apiParam = "playerStats/default/"+unityUserID;
            Debug.Log(initData.apiParam);
            skipTimeCheck = true;
        }else{
            allDefaultPPData();

            PlayerPrefs.SetString("Timestamp",DateTime.Now.ToString());
            id = PlayerPrefs.GetString("PlayerID");
            initData.apiParam = "playerStats/"+id;
            // ppUpdateStuff();
        }
        
        #region API Token Check
        StartCoroutine(initData.TokenGet(apiUrl +"/login/"+PlayerPrefs.GetString("PlayerID"), result => {
            tokenData = result;
            Debug.Log(tokenData);

  
            apiToken = tokenData["token"];
            Debug.Log(apiToken);

            // TODO : Implement token logic here to validate if token is authenticated
            #region  Authentication
            StartCoroutine(initData.TokenAuth(apiUrl +"/protected?token="+ apiToken, result => {
                tokenAuthData = result;
                Debug.Log(tokenAuthData);

                apiTokenData = tokenAuthData["message"];

                if(apiTokenData.Equals("This is only available for people with valid tokens.")){
                    Debug.Log("Token authenticated");
                    tokenAuthenticated = true;
                }else if(apiTokenData.Equals("Token is invalid!")){
                    Debug.Log("Invalid token");
                    tokenAuthenticated = false;
                }else{
                    Debug.Log("Server error");
                }
                    
            }));
            #endregion

        }));
        #endregion API Token Check
        
        if(tokenAuthenticated){
            StartCoroutine(initData.Download(apiUrl+initData.apiParam+"?token="+apiToken, result => {
            Debug.Log(initData.apiParam);
            dbInitData = result;
            Debug.Log(dbInitData);
            string timeStamp = DateTime.Now.ToString();
            Debug.Log(timeStamp);
            

  
            if(skipTimeCheck==false){
                //Check which is newer and write the older one with the new data.
                dbTimestamp = dbInitData["data"][0]["Timestamp"];
                playerPrefsTimestamp = PlayerPrefs.GetString("Timestamp");

                Debug.Log("PP Timestamp "+playerPrefsTimestamp);

                DateTime dbDate = Convert.ToDateTime(dbTimestamp);
                DateTime ppDate = Convert.ToDateTime(playerPrefsTimestamp);

                int value = DateTime.Compare(dbDate, ppDate);


                // comparing date and time
                if (value > 0)
                {
                    Debug.Log("dbDate is after ppDate. ");
                    //write pp with db data
                    pushDataToPlayerPrefs();

                    Debug.Log("Updated PlayerPrefs with DB data");
                }
                
                else
                {
                    Debug.Log("dbDate is the same as ppDate. ");
                    //use pp data
                }
            }else{
                PlayerPrefs.SetString("Timestamp",DateTime.Now.ToString());
            }
                
        }));
        }
        
        
        
    }

    private void allDefaultPPData(){
        #region Setting Default Stats to PlayerPrefs
            
        #region THIRD PERSON
        stringValues.Add("Timestamp",DateTime.Now.ToString());
        intValues.Add("2WheelerLicense",1);
        intValues.Add("3WheelerLicense",1);
        intValues.Add("4WheelerLicense",1);
        intValues.Add("6WheelerLicense",1);
        intValues.Add("MoneyBank",20000);
        intValues.Add("MoneyPocket",2000);
        intValues.Add("Health",50);
        intValues.Add("MoneyPerHealth",5);
        intValues.Add("FoodPrice",50);
        intValues.Add("PetrolPrice",105);
        floatValues.Add("TotalDistanceTraveled",0f);
        intValues.Add("Garbage_Money",200);
        intValues.Add("Drinage_Money",200);
        intValues.Add("Fire_Money",200);

        //Vibe
        stringValues.Add("Vibe2009rig-redoCSY_Unlocked","1");
        floatValues.Add("Vibe2009rig-redoCSY_Accl",0.075f);
        floatValues.Add("Vibe2009rig-redoCSY_MaxSpeed",0.080f);
        floatValues.Add("Vibe2009rig-redoCSY_TankCapacity",12f);
        floatValues.Add("Vibe2009rig-redoCSY_Mileage",0.180f);
        floatValues.Add("Vibe2009rig-redoCSY_Brake",-0.075f);
        floatValues.Add("Vibe2009rig-redoCSY_Steer",0.045f);
        floatValues.Add("Vibe2009rig-redoCSY_FR",1f);
        floatValues.Add("Vibe2009rig-redoCSY_TotalDistance",0f);
        stringValues.Add("Vibe2009rig-redoCSY_KIT","Stock0");
        stringValues.Add("Vibe2009rig-redoCSY_MAT","Vibe2009rig-redoCSY_Stock");

        #endregion THIRD PERSON


        #region TWO WHEELERS
        //HN-Dio
        stringVehicleValues.Add("HN-Dio_Unlocked","1");
        floatVehicleValues.Add("HN-Dio_Accl",0.075f);
        floatVehicleValues.Add("HN-Dio_MaxSpeed",0.080f);
        floatVehicleValues.Add("HN-Dio_TankCapacity",12f);
        floatVehicleValues.Add("HN-Dio_Mileage",0.180f);
        floatVehicleValues.Add("HN-Dio_Brake",-0.075f);
        floatVehicleValues.Add("HN-Dio_Steer",0.045f);
        floatVehicleValues.Add("HN-Dio_FR",1f);
        floatVehicleValues.Add("HN-Dio_TotalDistance",0f);
        stringVehicleValues.Add("HN-Dio_KIT","Stock0");
        stringVehicleValues.Add("HN-Dio_MAT","HN-Dio_Stock");

        //BJ-Chetak
        stringVehicleValues.Add("BJ-Chetak_Unlocked","1");
        floatVehicleValues.Add("BJ-Chetak_Accl",0.075f);
        floatVehicleValues.Add("BJ-Chetak_MaxSpeed",0.070f);
        floatVehicleValues.Add("BJ-Chetak_TankCapacity",12f);
        floatVehicleValues.Add("BJ-Chetak_Mileage",0.180f);
        floatVehicleValues.Add("BJ-Chetak_Brake",-0.075f);
        floatVehicleValues.Add("BJ-Chetak_Steer",0.045f);
        floatVehicleValues.Add("BJ-Chetak_FR",1f);
        floatVehicleValues.Add("BJ-Chetak_TotalDistance",0f);
        stringVehicleValues.Add("BJ-Chetak_KIT","Stock0");
        stringVehicleValues.Add("BJ-Chetak_MAT","BJ-Chetak_Stock");

        //BJ-Pulsar
        stringVehicleValues.Add("BJ-Pulsar_Unlocked","1");
        floatVehicleValues.Add("BJ-Pulsar_Accl",0.075f);
        floatVehicleValues.Add("BJ-Pulsar_MaxSpeed",0.085f);
        floatVehicleValues.Add("BJ-Pulsar_TankCapacity",12f);
        floatVehicleValues.Add("BJ-Pulsar_Mileage",0.180f);
        floatVehicleValues.Add("BJ-Pulsar_Brake",-0.075f);
        floatVehicleValues.Add("BJ-Pulsar_Steer",0.045f);
        floatVehicleValues.Add("BJ-Pulsar_FR",1f);
        floatVehicleValues.Add("BJ-Pulsar_TotalDistance",0f);
        stringVehicleValues.Add("BJ-Pulsar_KIT","Stock0");
        stringVehicleValues.Add("BJ-Pulsar_MAT","BJ-Pulsar_Stock");

        #endregion TWO WHEELERS


        #region FOUR WHEELERS
        //Ace
        stringVehicleValues.Add("Ace_Unlocked","1");
        floatVehicleValues.Add("Ace_Accl",0.75f);
        floatVehicleValues.Add("Ace_MaxSpeed",0.040f);
        floatVehicleValues.Add("Ace_TankCapacity",50f);
        floatVehicleValues.Add("Ace_Mileage",0.100f);
        floatVehicleValues.Add("Ace_Brake",-0.075f);
        floatVehicleValues.Add("Ace_Steer",0.030f);
        floatVehicleValues.Add("Ace_FR",1f);
        floatVehicleValues.Add("Ace_TotalDistance",0f);
        stringVehicleValues.Add("Ace_KIT","Stock0");
        stringVehicleValues.Add("Ace_MAT","Ace_Stock");

        //Ambassador
        stringVehicleValues.Add("Ambassador_Unlocked","1");
        floatVehicleValues.Add("Ambassador_Accl",0.75f);
        floatVehicleValues.Add("Ambassador_MaxSpeed",0.040f);
        floatVehicleValues.Add("Ambassador_TankCapacity",50f);
        floatVehicleValues.Add("Ambassador_Mileage",0.100f);
        floatVehicleValues.Add("Ambassador_Brake",-0.075f);
        floatVehicleValues.Add("Ambassador_Steer",0.030f);
        floatVehicleValues.Add("Ambassador_FR",1f);
        floatVehicleValues.Add("Ambassador_TotalDistance",0f);
        stringVehicleValues.Add("Ambassador_KIT","Stock0");
        stringVehicleValues.Add("Ambassador_MAT","Ambassador_Stock");

        //Indica
        stringVehicleValues.Add("Indica_Unlocked","1");
        floatVehicleValues.Add("Indica_Accl",0.50f);
        floatVehicleValues.Add("Indica_MaxSpeed",0.070f);
        floatVehicleValues.Add("Indica_TankCapacity",50f);
        floatVehicleValues.Add("Indica_Mileage",0.100f);
        floatVehicleValues.Add("Indica_Brake",-0.050f);
        floatVehicleValues.Add("Indica_Steer",0.030f);
        floatVehicleValues.Add("Indica_FR",1f);
        floatVehicleValues.Add("Indica_TotalDistance",0f);
        stringVehicleValues.Add("Indica_KIT","Stock0");
        stringVehicleValues.Add("Indica_MAT","Indica_Stock");

        //MS-800
        stringVehicleValues.Add("MS-800_Unlocked","1");
        floatVehicleValues.Add("MS-800_Accl",0.75f);
        floatVehicleValues.Add("MS-800_MaxSpeed",0.030f);
        floatVehicleValues.Add("MS-800_TankCapacity",50f);
        floatVehicleValues.Add("MS-800_Mileage",0.100f);
        floatVehicleValues.Add("MS-800_Brake",-0.075f);
        floatVehicleValues.Add("MS-800_Steer",0.030f);
        floatVehicleValues.Add("MS-800_FR",1f);
        floatVehicleValues.Add("MS-800_TotalDistance",0f);
        stringVehicleValues.Add("MS-800_KIT","Stock0");
        stringVehicleValues.Add("MS-800_MAT","MS-800_Stock");

        //MS-Alto
        stringVehicleValues.Add("MS-Alto_Unlocked","1");
        floatVehicleValues.Add("MS-Alto_Accl",0.60f);
        floatVehicleValues.Add("MS-Alto_MaxSpeed",0.120f);
        floatVehicleValues.Add("MS-Alto_TankCapacity",50f);
        floatVehicleValues.Add("MS-Alto_Mileage",0.100f);
        floatVehicleValues.Add("MS-Alto_Brake",-0.060f);
        floatVehicleValues.Add("MS-Alto_Steer",0.050f);
        floatVehicleValues.Add("MS-Alto_FR",1f);
        floatVehicleValues.Add("MS-Alto_TotalDistance",0f);
        stringVehicleValues.Add("MS-Alto_KIT","Stock0");
        stringVehicleValues.Add("MS-Alto_MAT","MS-Alto_Stock");

        //Nano
        stringVehicleValues.Add("Nano_Unlocked","1");
        floatVehicleValues.Add("Nano_Accl",0.50f);
        floatVehicleValues.Add("Nano_MaxSpeed",0.030f);
        floatVehicleValues.Add("Nano_TankCapacity",50f);
        floatVehicleValues.Add("Nano_Mileage",0.100f);
        floatVehicleValues.Add("Nano_Brake",-0.050f);
        floatVehicleValues.Add("Nano_Steer",0.030f);
        floatVehicleValues.Add("Nano_FR",1f);
        floatVehicleValues.Add("Nano_TotalDistance",0f);
        stringVehicleValues.Add("Nano_KIT","Stock0");
        stringVehicleValues.Add("Nano_MAT","Nano_Stock");

        //Scorpio
        stringVehicleValues.Add("Scorpio_Unlocked","1");
        floatVehicleValues.Add("Scorpio_Accl",0.80f);
        floatVehicleValues.Add("Scorpio_MaxSpeed",0.150f);
        floatVehicleValues.Add("Scorpio_TankCapacity",50f);
        floatVehicleValues.Add("Scorpio_Mileage",0.100f);
        floatVehicleValues.Add("Scorpio_Brake",-0.080f);
        floatVehicleValues.Add("Scorpio_Steer",0.030f);
        floatVehicleValues.Add("Scorpio_FR",1f);
        floatVehicleValues.Add("Scorpio_TotalDistance",0f);
        stringVehicleValues.Add("Scorpio_KIT","Stock0");
        stringVehicleValues.Add("Scorpio_MAT","Scorpio_Stock");

        //VJM02
        stringVehicleValues.Add("VJM02_Unlocked","1");
        floatVehicleValues.Add("VJM02_Accl",2f);
        floatVehicleValues.Add("VJM02_MaxSpeed",0.3f);
        floatVehicleValues.Add("VJM02_TankCapacity",143f);
        floatVehicleValues.Add("VJM02_Mileage",0.3f);
        floatVehicleValues.Add("VJM02_Brake",-2f);
        floatVehicleValues.Add("VJM02_Steer",0.030f);
        floatVehicleValues.Add("VJM02_FR",1f);
        floatVehicleValues.Add("VJM02_TotalDistance",0f);
        stringVehicleValues.Add("VJM02_KIT","Stock0");
        stringVehicleValues.Add("VJM02_MAT","VJM02_Stock");

        //BMTC_1wopass
        stringVehicleValues.Add("BMTC_1wopass_Unlocked","1");
        floatVehicleValues.Add("BMTC_1wopass_Accl",4.5f);
        floatVehicleValues.Add("BMTC_1wopass_MaxSpeed",0.3f);
        floatVehicleValues.Add("BMTC_1wopass_TankCapacity",50f);
        floatVehicleValues.Add("BMTC_1wopass_Mileage",0.3f);
        floatVehicleValues.Add("BMTC_1wopass_Brake",-4.5f);
        floatVehicleValues.Add("BMTC_1wopass_Steer",0.030f);
        floatVehicleValues.Add("BMTC_1wopass_FR",1f);
        floatVehicleValues.Add("BMTC_1wopass_TotalDistance",0f);
        stringVehicleValues.Add("BMTC_1wopass_KIT","Stock0");
        stringVehicleValues.Add("BMTC_1wopass_MAT","BMTC_1wopass_Stock");

        //Tempo
        stringVehicleValues.Add("Tempo_Unlocked","1");
        floatVehicleValues.Add("Tempo_Accl",4.5f);
        floatVehicleValues.Add("Tempo_MaxSpeed",0.3f);
        floatVehicleValues.Add("Tempo_TankCapacity",50f);
        floatVehicleValues.Add("Tempo_Mileage",0.3f);
        floatVehicleValues.Add("Tempo_Brake",-4.5f);
        floatVehicleValues.Add("Tempo_Steer",0.030f);
        floatVehicleValues.Add("Tempo_FR",1f);
        floatVehicleValues.Add("Tempo_TotalDistance",0f);
        stringVehicleValues.Add("Tempo_KIT","Stock0");
        stringVehicleValues.Add("Tempo_MAT","Tempo_Stock");

        #endregion FOUR WHEELERS


        #region THREE WHEELERS
        //BA-RE
        stringVehicleValues.Add("BA-RE_Unlocked","1");
        floatVehicleValues.Add("BA-RE_Accl",0.4f);
        floatVehicleValues.Add("BA-RE_MaxSpeed",0.060f);
        floatVehicleValues.Add("BA-RE_TankCapacity",30f);
        floatVehicleValues.Add("BA-RE_Mileage",0.040f);
        floatVehicleValues.Add("BA-RE_Brake",-0.4f);
        floatVehicleValues.Add("BA-RE_Steer",0.030f);
        floatVehicleValues.Add("BA-RE_FR",1f);
        floatVehicleValues.Add("BA-RE_TotalDistance",0f);
        stringVehicleValues.Add("BA-RE_KIT","Stock0");
        stringVehicleValues.Add("BA-RE_MAT","BA-RE_Stock");

        //BA-RE-goods
        stringVehicleValues.Add("BA-RE-goods_Unlocked","1");
        floatVehicleValues.Add("BA-RE-goods_Accl",0.4f);
        floatVehicleValues.Add("BA-RE-goods_MaxSpeed",0.060f);
        floatVehicleValues.Add("BA-RE-goods_TankCapacity",30f);
        floatVehicleValues.Add("BA-RE-goods_Mileage",0.040f);
        floatVehicleValues.Add("BA-RE-goods_Brake",-0.4f);
        floatVehicleValues.Add("BA-RE-goods_Steer",0.030f);
        floatVehicleValues.Add("BA-RE-goods_FR",1f);
        floatVehicleValues.Add("BA-RE-goods_TotalDistance",0f);
        stringVehicleValues.Add("BA-RE-goods_KIT","Stock0");
        stringVehicleValues.Add("BA-RE-goods_MAT","BA-RE_Stock");

        //Ape
        stringVehicleValues.Add("Ape_Unlocked","1");
        floatVehicleValues.Add("Ape_Accl",0.30f);
        floatVehicleValues.Add("Ape_MaxSpeed",0.030f);
        floatVehicleValues.Add("Ape_TankCapacity",30f);
        floatVehicleValues.Add("Ape_Mileage",0.040f);
        floatVehicleValues.Add("Ape_Brake",-0.030f);
        floatVehicleValues.Add("Ape_Steer",0.030f);
        floatVehicleValues.Add("Ape_FR",1f);
        floatVehicleValues.Add("Ape_TotalDistance",0f);
        stringVehicleValues.Add("Ape_KIT","Stock0");
        stringVehicleValues.Add("Ape_MAT","Ape_Stock");

        //BullockCart_1
        stringVehicleValues.Add("BullockCart_1_Unlocked","1");
        floatVehicleValues.Add("BullockCart_1_Accl",0.050f);
        floatVehicleValues.Add("BullockCart_1_MaxSpeed",0.030f);
        floatVehicleValues.Add("BullockCart_1_TankCapacity",30f);
        floatVehicleValues.Add("BullockCart_1_Mileage",0.040f);
        floatVehicleValues.Add("BullockCart_1_Brake",-0.050f);
        floatVehicleValues.Add("BullockCart_1_Steer",0.030f);
        floatVehicleValues.Add("BullockCart_1_FR",1f);
        floatVehicleValues.Add("BullockCart_1_TotalDistance",0f);
        stringVehicleValues.Add("BullockCart_1_KIT","Stock0");
        stringVehicleValues.Add("BullockCart_1_MAT","BullockCart_1_Stock");

        #endregion THREE WHEELERS


        #endregion Setting Default Vehicle Stats to PlayerPrefs




        //Check if PP already exist; if not, then add.
        //string values
        foreach(KeyValuePair<string,string> stringValue in stringValues){
            if(!PlayerPrefs.HasKey(stringValue.Key)){
                PlayerPrefs.SetString(stringValue.Key,stringValue.Value);
            }
        }

        //int values
        foreach(KeyValuePair<string,int> intValue in intValues){
            if(!PlayerPrefs.HasKey(intValue.Key)){
                PlayerPrefs.SetInt(intValue.Key,intValue.Value);
            }
        }

        //float values
        foreach(KeyValuePair<string,float> floatValue in floatValues){
            if(!PlayerPrefs.HasKey(floatValue.Key)){
                PlayerPrefs.SetFloat(floatValue.Key,floatValue.Value);
            }
        }

        //Vehicle string values
        foreach(KeyValuePair<string,string> stringValue in stringVehicleValues){
            if(!PlayerPrefs.HasKey(stringValue.Key)){
                PlayerPrefs.SetString(stringValue.Key,stringValue.Value);
            }
        }

        //Vehicle int values
        foreach(KeyValuePair<string,int> intValue in intVehicleValues){
            if(!PlayerPrefs.HasKey(intValue.Key)){
                PlayerPrefs.SetInt(intValue.Key,intValue.Value);
            }
        }

        //Vehicle float values
        foreach(KeyValuePair<string,float> floatValue in floatVehicleValues){
            if(!PlayerPrefs.HasKey(floatValue.Key)){
                PlayerPrefs.SetFloat(floatValue.Key,floatValue.Value);
            }
        }

        renamePlayerPrefsInNewUpdate();
        deletePlayerPrefsInNewUpdate();
    }


    private void deletePlayerPrefsInNewUpdate(){
        //Add PP key name here which needs to be deleted in new game update. For example, the below:
        //string[] toDelete = new string[] {"PlayerName","TotalDistance"};
        //DO NOT DELETE ANY EXISTING PP KEY.THESE OLD PP KEYS SHOULD BE PRESENT HERE FOR LIFETIME

        string[] toDelete = new string[] {};

        foreach(string del in toDelete){
            PlayerPrefs.DeleteKey(del);
        }
    }

    private void renamePlayerPrefsInNewUpdate(){
        //Add PP key name here which needs to be renamed in new game update. For example, the below:
        //toRename.Add("OldName:string","NewName");
        //DO NOT DELETE ANY EXISTING PP KEY.THESE OLD PP KEYS SHOULD BE PRESENT HERE FOR LIFETIME

        Dictionary<string,string> toRename = new Dictionary<string, string>();
        // toRename.Add("OldName:string","NewName");

        foreach(string ren in toRename.Keys){
            string dataType = ren.Split(':')[1];
            string oldName = ren.Split(':')[0];
            string newName = toRename[ren];

            string oldStringPPValue = "";
            int oldIntPPValue;
            float oldFloatPPValue;

            switch(dataType){
                case "string":
                    oldStringPPValue = PlayerPrefs.GetString(oldName);
                    PlayerPrefs.DeleteKey(oldName);
                    PlayerPrefs.SetString(newName,oldStringPPValue);
                    break;
                case "int":
                    oldIntPPValue = PlayerPrefs.GetInt(oldName);
                    PlayerPrefs.DeleteKey(oldName);
                    PlayerPrefs.SetInt(newName,oldIntPPValue);
                    break;
                case "float":
                    oldFloatPPValue = PlayerPrefs.GetFloat(oldName);
                    PlayerPrefs.DeleteKey(oldName);
                    PlayerPrefs.SetFloat(newName,oldFloatPPValue);
                    break;
                default:
                    Debug.Log("Data type not found to fetch from the PP");
                    break;
            }

        }


    }


    public void validateAndPushToDB(){
        initData = new Init();
        id = PlayerPrefs.GetString("PlayerID");
        initData.apiParam = "playerStats/"+id;

        if(tokenAuthenticated){
            StartCoroutine(initData.Download(apiUrl+initData.apiParam+"?token="+apiToken, result => {
            Debug.Log(initData.apiParam);
            dbInitData = result;
            Debug.Log(dbInitData);
            string timeStamp = DateTime.Now.ToString();
            Debug.Log(timeStamp);
            
            dbTimestamp = dbInitData["data"][0]["Timestamp"];
            playerPrefsTimestamp = PlayerPrefs.GetString("Timestamp");
            Debug.Log("PP Timestamp "+playerPrefsTimestamp);

            DateTime dbDate = Convert.ToDateTime(dbTimestamp);
            DateTime ppDate = Convert.ToDateTime(playerPrefsTimestamp);

            Debug.Log(dbDate);
            Debug.Log(ppDate);

            int value = DateTime.Compare(dbDate, ppDate);
            Debug.Log("date compared: "+value);

            // comparing date and time
            if (value < 0)
            {
                Debug.Log("dbDate is before the ppDate. ");
                //write db with pp data
                pushDataToDB();

                StartCoroutine(initData.Post(playerJson,apiToken, result =>{
                    string res = result;
                    Debug.Log("Post status "+res);
                }));
            }

        }));
        
        }
    }


    private void pushDataToDB(){
        //Read Data from PlayerPrefs
        //And then push data to DB
        
        playerJson.Add("Timestamp",PlayerPrefs.GetString("Timestamp"));
        playerJson.Add("ID",PlayerPrefs.GetString("PlayerID"));
        playerJson.Add("License2W",PlayerPrefs.GetInt("2WheelerLicense"));
        playerJson.Add("License3W",PlayerPrefs.GetInt("3WheelerLicense"));
        playerJson.Add("License4W",PlayerPrefs.GetInt("4WheelerLicense"));
        playerJson.Add("License6W",PlayerPrefs.GetInt("6WheelerLicense"));
        playerJson.Add("MoneyBank",PlayerPrefs.GetInt("MoneyBank"));
        playerJson.Add("Health",PlayerPrefs.GetInt("Health"));
        playerJson.Add("FoodPrice",PlayerPrefs.GetInt("FoodPrice"));
        playerJson.Add("PetrolPrice",PlayerPrefs.GetInt("PetrolPrice"));
        playerJson.Add("MoneyPerHealth",PlayerPrefs.GetInt("MoneyPerHealth"));
        playerJson.Add("TotalDistanceTraveled",PlayerPrefs.GetFloat("TotalDistanceTraveled"));
        
        
        //Vibe
        playerJson.Add("Vibe2009rig-redoCSY_Unlocked",PlayerPrefs.GetString("Vibe2009rig-redoCSY_Unlocked"));
        playerJson.Add("Vibe2009rig-redoCSY_KIT",PlayerPrefs.GetString("Vibe2009rig-redoCSY_KIT"));
        playerJson.Add("Vibe2009rig-redoCSY_MAT",PlayerPrefs.GetString("Vibe2009rig-redoCSY_MAT"));
        playerJson.Add("Vibe2009rig-redoCSY_Accl",PlayerPrefs.GetFloat("Vibe2009rig-redoCSY_Accl"));
        playerJson.Add("Vibe2009rig-redoCSY_MaxSpeed",PlayerPrefs.GetFloat("Vibe2009rig-redoCSY_MaxSpeed"));
        playerJson.Add("Vibe2009rig-redoCSY_TankCapacity",PlayerPrefs.GetFloat("Vibe2009rig-redoCSY_TankCapacity"));
        playerJson.Add("Vibe2009rig-redoCSY_Mileage",PlayerPrefs.GetFloat("Vibe2009rig-redoCSY_Mileage"));
        playerJson.Add("Vibe2009rig-redoCSY_Brake",PlayerPrefs.GetFloat("Vibe2009rig-redoCSY_Brake"));
        playerJson.Add("Vibe2009rig-redoCSY_Steer",PlayerPrefs.GetFloat("Vibe2009rig-redoCSY_Steer"));
        playerJson.Add("Vibe2009rig-redoCSY_FR",PlayerPrefs.GetFloat("Vibe2009rig-redoCSY_FR"));
        playerJson.Add("Vibe2009rig-redoCSY_TotalDistance",PlayerPrefs.GetFloat("Vibe2009rig-redoCSY_TotalDistance"));

        //HN-Dio
        playerJson.Add("HN-Dio_Unlocked",PlayerPrefs.GetString("HN-Dio_Unlocked"));
        playerJson.Add("HN-Dio_KIT",PlayerPrefs.GetString("HN-Dio_KIT"));
        playerJson.Add("HN-Dio_MAT",PlayerPrefs.GetString("HN-Dio_MAT"));
        playerJson.Add("HN-Dio_Accl",PlayerPrefs.GetFloat("HN-Dio_Accl"));
        playerJson.Add("HN-Dio_MaxSpeed",PlayerPrefs.GetFloat("HN-Dio_MaxSpeed"));
        playerJson.Add("HN-Dio_TankCapacity",PlayerPrefs.GetFloat("HN-Dio_TankCapacity"));
        playerJson.Add("HN-Dio_Mileage",PlayerPrefs.GetFloat("HN-Dio_Mileage"));
        playerJson.Add("HN-Dio_Brake",PlayerPrefs.GetFloat("HN-Dio_Brake"));
        playerJson.Add("HN-Dio_Steer",PlayerPrefs.GetFloat("HN-Dio_Steer"));
        playerJson.Add("HN-Dio_FR",PlayerPrefs.GetFloat("HN-Dio_FR"));
        playerJson.Add("HN-Dio_TotalDistance",PlayerPrefs.GetFloat("HN-Dio_TotalDistance"));

        //BJ-Chetak
        playerJson.Add("BJ-Chetak_Unlocked",PlayerPrefs.GetString("BJ-Chetak_Unlocked"));
        playerJson.Add("BJ-Chetak_KIT",PlayerPrefs.GetString("BJ-Chetak_KIT"));
        playerJson.Add("BJ-Chetak_MAT",PlayerPrefs.GetString("BJ-Chetak_MAT"));
        playerJson.Add("BJ-Chetak_Accl",PlayerPrefs.GetFloat("BJ-Chetak_Accl"));
        playerJson.Add("BJ-Chetak_MaxSpeed",PlayerPrefs.GetFloat("BJ-Chetak_MaxSpeed"));
        playerJson.Add("BJ-Chetak_TankCapacity",PlayerPrefs.GetFloat("BJ-Chetak_TankCapacity"));
        playerJson.Add("BJ-Chetak_Mileage",PlayerPrefs.GetFloat("BJ-Chetak_Mileage"));
        playerJson.Add("BJ-Chetak_Brake",PlayerPrefs.GetFloat("BJ-Chetak_Brake"));
        playerJson.Add("BJ-Chetak_Steer",PlayerPrefs.GetFloat("BJ-Chetak_Steer"));
        playerJson.Add("BJ-Chetak_TotalDistance",PlayerPrefs.GetFloat("BJ-Chetak_TotalDistance"));
        playerJson.Add("BJ-Chetak_FR",PlayerPrefs.GetFloat("BJ-Chetak_FR"));

        //BJ-Pulsar
        playerJson.Add("BJ-Pulsar_Unlocked",PlayerPrefs.GetString("BJ-Pulsar_Unlocked"));
        playerJson.Add("BJ-Pulsar_KIT",PlayerPrefs.GetString("BJ-Pulsar_KIT"));
        playerJson.Add("BJ-Pulsar_MAT",PlayerPrefs.GetString("BJ-Pulsar_MAT"));
        playerJson.Add("BJ-Pulsar_Accl",PlayerPrefs.GetFloat("BJ-Pulsar_Accl"));
        playerJson.Add("BJ-Pulsar_MaxSpeed",PlayerPrefs.GetFloat("BJ-Pulsar_MaxSpeed"));
        playerJson.Add("BJ-Pulsar_TankCapacity",PlayerPrefs.GetFloat("BJ-Pulsar_TankCapacity"));
        playerJson.Add("BJ-Pulsar_Mileage",PlayerPrefs.GetFloat("BJ-Pulsar_Mileage"));
        playerJson.Add("BJ-Pulsar_Brake",PlayerPrefs.GetFloat("BJ-Pulsar_Brake"));
        playerJson.Add("BJ-Pulsar_Steer",PlayerPrefs.GetFloat("BJ-Pulsar_Steer"));
        playerJson.Add("BJ-Pulsar_FR",PlayerPrefs.GetFloat("BJ-Pulsar_FR"));
        playerJson.Add("BJ-Pulsar_TotalDistance",PlayerPrefs.GetFloat("BJ-Pulsar_TotalDistance"));

        //Ace
        playerJson.Add("Ace_Unlocked",PlayerPrefs.GetString("Ace_Unlocked"));
        playerJson.Add("Ace_KIT",PlayerPrefs.GetString("Ace_KIT"));
        playerJson.Add("Ace_MAT",PlayerPrefs.GetString("Ace_MAT"));
        playerJson.Add("Ace_Accl",PlayerPrefs.GetFloat("Ace_Accl"));
        playerJson.Add("Ace_MaxSpeed",PlayerPrefs.GetFloat("Ace_MaxSpeed"));
        playerJson.Add("Ace_TankCapacity",PlayerPrefs.GetFloat("Ace_TankCapacity"));
        playerJson.Add("Ace_Mileage",PlayerPrefs.GetFloat("Ace_Mileage"));
        playerJson.Add("Ace_Brake",PlayerPrefs.GetFloat("Ace_Brake"));
        playerJson.Add("Ace_Steer",PlayerPrefs.GetFloat("Ace_Steer"));
        playerJson.Add("Ace_FR",PlayerPrefs.GetFloat("Ace_FR"));
        playerJson.Add("Ace_TotalDistance",PlayerPrefs.GetFloat("Ace_TotalDistance"));

        //Ambassador
        playerJson.Add("Ambassador_Unlocked",PlayerPrefs.GetString("Ambassador_Unlocked"));
        playerJson.Add("Ambassador_KIT",PlayerPrefs.GetString("Ambassador_KIT"));
        playerJson.Add("Ambassador_MAT",PlayerPrefs.GetString("Ambassador_MAT"));
        playerJson.Add("Ambassador_Accl",PlayerPrefs.GetFloat("Ambassador_Accl"));
        playerJson.Add("Ambassador_MaxSpeed",PlayerPrefs.GetFloat("Ambassador_MaxSpeed"));
        playerJson.Add("Ambassador_TankCapacity",PlayerPrefs.GetFloat("Ambassador_TankCapacity"));
        playerJson.Add("Ambassador_Mileage",PlayerPrefs.GetFloat("Ambassador_Mileage"));
        playerJson.Add("Ambassador_Brake",PlayerPrefs.GetFloat("Ambassador_Brake"));
        playerJson.Add("Ambassador_Steer",PlayerPrefs.GetFloat("Ambassador_Steer"));
        playerJson.Add("Ambassador_FR",PlayerPrefs.GetFloat("Ambassador_FR"));
        playerJson.Add("Ambassador_TotalDistance",PlayerPrefs.GetFloat("Ambassador_TotalDistance"));

        //Indica
        playerJson.Add("Indica_Unlocked",PlayerPrefs.GetString("Indica_Unlocked"));
        playerJson.Add("Indica_KIT",PlayerPrefs.GetString("Indica_KIT"));
        playerJson.Add("Indica_MAT",PlayerPrefs.GetString("Indica_MAT"));
        playerJson.Add("Indica_Accl",PlayerPrefs.GetFloat("Indica_Accl"));
        playerJson.Add("Indica_MaxSpeed",PlayerPrefs.GetFloat("Indica_MaxSpeed"));
        playerJson.Add("Indica_TankCapacity",PlayerPrefs.GetFloat("Indica_TankCapacity"));
        playerJson.Add("Indica_Mileage",PlayerPrefs.GetFloat("Indica_Mileage"));
        playerJson.Add("Indica_Brake",PlayerPrefs.GetFloat("Indica_Brake"));
        playerJson.Add("Indica_Steer",PlayerPrefs.GetFloat("Indica_Steer"));
        playerJson.Add("Indica_FR",PlayerPrefs.GetFloat("Indica_FR"));
        playerJson.Add("Indica_TotalDistance",PlayerPrefs.GetFloat("Indica_TotalDistance"));

        //MS-800
        playerJson.Add("MS-800_Unlocked",PlayerPrefs.GetString("MS-800_Unlocked"));
        playerJson.Add("MS-800_KIT",PlayerPrefs.GetString("MS-800_KIT"));
        playerJson.Add("MS-800_MAT",PlayerPrefs.GetString("MS-800_MAT"));
        playerJson.Add("MS-800_Accl",PlayerPrefs.GetFloat("MS-800_Accl"));
        playerJson.Add("MS-800_MaxSpeed",PlayerPrefs.GetFloat("MS-800_MaxSpeed"));
        playerJson.Add("MS-800_TankCapacity",PlayerPrefs.GetFloat("MS-800_TankCapacity"));
        playerJson.Add("MS-800_Mileage",PlayerPrefs.GetFloat("MS-800_Mileage"));
        playerJson.Add("MS-800_Brake",PlayerPrefs.GetFloat("MS-800_Brake"));
        playerJson.Add("MS-800_Steer",PlayerPrefs.GetFloat("MS-800_Steer"));
        playerJson.Add("MS-800_FR",PlayerPrefs.GetFloat("MS-800_FR"));
        playerJson.Add("MS-800_TotalDistance",PlayerPrefs.GetFloat("MS-800_TotalDistance"));

        //MS-Alto
        playerJson.Add("MS-Alto_Unlocked",PlayerPrefs.GetString("MS-Alto_Unlocked"));
        playerJson.Add("MS-Alto_KIT",PlayerPrefs.GetString("MS-Alto_KIT"));
        playerJson.Add("MS-Alto_MAT",PlayerPrefs.GetString("MS-Alto_MAT"));
        playerJson.Add("MS-Alto_Accl",PlayerPrefs.GetFloat("MS-Alto_Accl"));
        playerJson.Add("MS-Alto_MaxSpeed",PlayerPrefs.GetFloat("MS-Alto_MaxSpeed"));
        playerJson.Add("MS-Alto_TankCapacity",PlayerPrefs.GetFloat("MS-Alto_TankCapacity"));
        playerJson.Add("MS-Alto_Mileage",PlayerPrefs.GetFloat("MS-Alto_Mileage"));
        playerJson.Add("MS-Alto_Brake",PlayerPrefs.GetFloat("MS-Alto_Brake"));
        playerJson.Add("MS-Alto_Steer",PlayerPrefs.GetFloat("MS-Alto_Steer"));
        playerJson.Add("MS-Alto_FR",PlayerPrefs.GetFloat("MS-Alto_FR"));
        playerJson.Add("MS-Alto_TotalDistance",PlayerPrefs.GetFloat("MS-Alto_TotalDistance"));

        //Nano
        playerJson.Add("Nano_Unlocked",PlayerPrefs.GetString("Nano_Unlocked"));
        playerJson.Add("Nano_KIT",PlayerPrefs.GetString("Nano_KIT"));
        playerJson.Add("Nano_MAT",PlayerPrefs.GetString("Nano_MAT"));
        playerJson.Add("Nano_Accl",PlayerPrefs.GetFloat("Nano_Accl"));
        playerJson.Add("Nano_MaxSpeed",PlayerPrefs.GetFloat("Nano_MaxSpeed"));
        playerJson.Add("Nano_TankCapacity",PlayerPrefs.GetFloat("Nano_TankCapacity"));
        playerJson.Add("Nano_Mileage",PlayerPrefs.GetFloat("Nano_Mileage"));
        playerJson.Add("Nano_Brake",PlayerPrefs.GetFloat("Nano_Brake"));
        playerJson.Add("Nano_Steer",PlayerPrefs.GetFloat("Nano_Steer"));
        playerJson.Add("Nano_FR",PlayerPrefs.GetFloat("Nano_FR"));
        playerJson.Add("Nano_TotalDistance",PlayerPrefs.GetFloat("Nano_TotalDistance"));
        
        //Scorpio
        playerJson.Add("Scorpio_Unlocked",PlayerPrefs.GetString("Scorpio_Unlocked"));
        playerJson.Add("Scorpio_KIT",PlayerPrefs.GetString("Scorpio_KIT"));
        playerJson.Add("Scorpio_MAT",PlayerPrefs.GetString("Scorpio_MAT"));
        playerJson.Add("Scorpio_Accl",PlayerPrefs.GetFloat("Scorpio_Accl"));
        playerJson.Add("Scorpio_MaxSpeed",PlayerPrefs.GetFloat("Scorpio_MaxSpeed"));
        playerJson.Add("Scorpio_TankCapacity",PlayerPrefs.GetFloat("Scorpio_TankCapacity"));
        playerJson.Add("Scorpio_Mileage",PlayerPrefs.GetFloat("Scorpio_Mileage"));
        playerJson.Add("Scorpio_Brake",PlayerPrefs.GetFloat("Scorpio_Brake"));
        playerJson.Add("Scorpio_Steer",PlayerPrefs.GetFloat("Scorpio_Steer"));
        playerJson.Add("Scorpio_FR",PlayerPrefs.GetFloat("Scorpio_FR"));
        playerJson.Add("Scorpio_TotalDistance",PlayerPrefs.GetFloat("Scorpio_TotalDistance"));

        //VJM02
        playerJson.Add("VJM02_Unlocked",PlayerPrefs.GetString("VJM02_Unlocked"));
        playerJson.Add("VJM02_KIT",PlayerPrefs.GetString("VJM02_KIT"));
        playerJson.Add("VJM02_MAT",PlayerPrefs.GetString("VJM02_MAT"));
        playerJson.Add("VJM02_Accl",PlayerPrefs.GetFloat("VJM02_Accl"));
        playerJson.Add("VJM02_MaxSpeed",PlayerPrefs.GetFloat("VJM02_MaxSpeed"));
        playerJson.Add("VJM02_TankCapacity",PlayerPrefs.GetFloat("VJM02_TankCapacity"));
        playerJson.Add("VJM02_Mileage",PlayerPrefs.GetFloat("VJM02_Mileage"));
        playerJson.Add("VJM02_Brake",PlayerPrefs.GetFloat("VJM02_Brake"));
        playerJson.Add("VJM02_Steer",PlayerPrefs.GetFloat("VJM02_Steer"));
        playerJson.Add("VJM02_FR",PlayerPrefs.GetFloat("VJM02_FR"));
        playerJson.Add("VJM02_TotalDistance",PlayerPrefs.GetFloat("VJM02_TotalDistance"));

        //BMTC_1wopass
        playerJson.Add("BMTC_1wopass_Unlocked",PlayerPrefs.GetString("BMTC_1wopass_Unlocked"));
        playerJson.Add("BMTC_1wopass_KIT",PlayerPrefs.GetString("BMTC_1wopass_KIT"));
        playerJson.Add("BMTC_1wopass_MAT",PlayerPrefs.GetString("BMTC_1wopass_MAT"));
        playerJson.Add("BMTC_1wopass_Accl",PlayerPrefs.GetFloat("BMTC_1wopass_Accl"));
        playerJson.Add("BMTC_1wopass_MaxSpeed",PlayerPrefs.GetFloat("BMTC_1wopass_MaxSpeed"));
        playerJson.Add("BMTC_1wopass_TankCapacity",PlayerPrefs.GetFloat("BMTC_1wopass_TankCapacity"));
        playerJson.Add("BMTC_1wopass_Mileage",PlayerPrefs.GetFloat("BMTC_1wopass_Mileage"));
        playerJson.Add("BMTC_1wopass_Brake",PlayerPrefs.GetFloat("BMTC_1wopass_Brake"));
        playerJson.Add("BMTC_1wopass_Steer",PlayerPrefs.GetFloat("BMTC_1wopass_Steer"));
        playerJson.Add("BMTC_1wopass_FR",PlayerPrefs.GetFloat("BMTC_1wopass_FR"));
        playerJson.Add("BMTC_1wopass_TotalDistance",PlayerPrefs.GetFloat("BMTC_1wopass_TotalDistance"));

        //Tempo
        playerJson.Add("Tempo_Unlocked",PlayerPrefs.GetString("Tempo_Unlocked"));
        playerJson.Add("Tempo_KIT",PlayerPrefs.GetString("Tempo_KIT"));
        playerJson.Add("Tempo_MAT",PlayerPrefs.GetString("Tempo_MAT"));
        playerJson.Add("Tempo_Accl",PlayerPrefs.GetFloat("Tempo_Accl"));
        playerJson.Add("Tempo_MaxSpeed",PlayerPrefs.GetFloat("Tempo_MaxSpeed"));
        playerJson.Add("Tempo_TankCapacity",PlayerPrefs.GetFloat("Tempo_TankCapacity"));
        playerJson.Add("Tempo_Mileage",PlayerPrefs.GetFloat("Tempo_Mileage"));
        playerJson.Add("Tempo_Brake",PlayerPrefs.GetFloat("Tempo_Brake"));
        playerJson.Add("Tempo_Steer",PlayerPrefs.GetFloat("Tempo_Steer"));
        playerJson.Add("Tempo_FR",PlayerPrefs.GetFloat("Tempo_FR"));
        playerJson.Add("Tempo_TotalDistance",PlayerPrefs.GetFloat("Tempo_TotalDistance"));

        //BA-RE
        playerJson.Add("BA-RE_Unlocked",PlayerPrefs.GetString("BA-RE_Unlocked"));
        playerJson.Add("BA-RE_KIT",PlayerPrefs.GetString("BA-RE_KIT"));
        playerJson.Add("BA-RE_MAT",PlayerPrefs.GetString("BA-RE_MAT"));
        playerJson.Add("BA-RE_Accl",PlayerPrefs.GetFloat("BA-RE_Accl"));
        playerJson.Add("BA-RE_MaxSpeed",PlayerPrefs.GetFloat("BA-RE_MaxSpeed"));
        playerJson.Add("BA-RE_TankCapacity",PlayerPrefs.GetFloat("BA-RE_TankCapacity"));
        playerJson.Add("BA-RE_Mileage",PlayerPrefs.GetFloat("BA-RE_Mileage"));
        playerJson.Add("BA-RE_Brake",PlayerPrefs.GetFloat("BA-RE_Brake"));
        playerJson.Add("BA-RE_Steer",PlayerPrefs.GetFloat("BA-RE_Steer"));
        playerJson.Add("BA-RE_FR",PlayerPrefs.GetFloat("BA-RE_FR"));
        playerJson.Add("BA-RE_TotalDistance",PlayerPrefs.GetFloat("BA-RE_TotalDistance"));

        //BA-RE-goods
        playerJson.Add("BA-RE-goods_Unlocked",PlayerPrefs.GetString("BA-RE_Unlocked"));
        playerJson.Add("BA-RE-goods_KIT",PlayerPrefs.GetString("BA-RE_KIT"));
        playerJson.Add("BA-RE-goods_MAT",PlayerPrefs.GetString("BA-RE_MAT"));
        playerJson.Add("BA-RE-goods_Accl",PlayerPrefs.GetFloat("BA-RE_Accl"));
        playerJson.Add("BA-RE-goods_MaxSpeed",PlayerPrefs.GetFloat("BA-RE_MaxSpeed"));
        playerJson.Add("BA-RE-goods_TankCapacity",PlayerPrefs.GetFloat("BA-RE_TankCapacity"));
        playerJson.Add("BA-RE-goods_Mileage",PlayerPrefs.GetFloat("BA-RE_Mileage"));
        playerJson.Add("BA-RE-goods_Brake",PlayerPrefs.GetFloat("BA-RE_Brake"));
        playerJson.Add("BA-RE-goods_Steer",PlayerPrefs.GetFloat("BA-RE_Steer"));
        playerJson.Add("BA-RE-goods_FR",PlayerPrefs.GetFloat("BA-RE_FR"));
        playerJson.Add("BA-RE-goods_TotalDistance",PlayerPrefs.GetFloat("BA-RE_TotalDistance"));

        //Ape
        playerJson.Add("Ape_Unlocked",PlayerPrefs.GetString("Ape_Unlocked"));
        playerJson.Add("Ape_KIT",PlayerPrefs.GetString("Ape_KIT"));
        playerJson.Add("Ape_MAT",PlayerPrefs.GetString("Ape_MAT"));
        playerJson.Add("Ape_Accl",PlayerPrefs.GetFloat("Ape_Accl"));
        playerJson.Add("Ape_MaxSpeed",PlayerPrefs.GetFloat("Ape_MaxSpeed"));
        playerJson.Add("Ape_TankCapacity",PlayerPrefs.GetFloat("Ape_TankCapacity"));
        playerJson.Add("Ape_Mileage",PlayerPrefs.GetFloat("Ape_Mileage"));
        playerJson.Add("Ape_Brake",PlayerPrefs.GetFloat("Ape_Brake"));
        playerJson.Add("Ape_Steer",PlayerPrefs.GetFloat("Ape_Steer"));
        playerJson.Add("Ape_FR",PlayerPrefs.GetFloat("Ape_FR"));
        playerJson.Add("Ape_TotalDistance",PlayerPrefs.GetFloat("Ape_TotalDistance"));

        //BullockCart_1
        playerJson.Add("BullockCart_1_Unlocked",PlayerPrefs.GetString("BullockCart_1_Unlocked"));
        playerJson.Add("BullockCart_1_KIT",PlayerPrefs.GetString("BullockCart_1_KIT"));
        playerJson.Add("BullockCart_1_MAT",PlayerPrefs.GetString("BullockCart_1_MAT"));
        playerJson.Add("BullockCart_1_Accl",PlayerPrefs.GetFloat("BullockCart_1_Accl"));
        playerJson.Add("BullockCart_1_MaxSpeed",PlayerPrefs.GetFloat("BullockCart_1_MaxSpeed"));
        playerJson.Add("BullockCart_1_TankCapacity",PlayerPrefs.GetFloat("BullockCart_1_TankCapacity"));
        playerJson.Add("BullockCart_1_Mileage",PlayerPrefs.GetFloat("BullockCart_1_Mileage"));
        playerJson.Add("BullockCart_1_Brake",PlayerPrefs.GetFloat("BullockCart_1_Brake"));
        playerJson.Add("BullockCart_1_Steer",PlayerPrefs.GetFloat("BullockCart_1_Steer"));
        playerJson.Add("BullockCart_1_FR",PlayerPrefs.GetFloat("BullockCart_1_FR"));
        playerJson.Add("BullockCart_1_TotalDistance",PlayerPrefs.GetFloat("BullockCart_1_TotalDistance"));

        // Debug.Log(playerJson.ToString());
    }

    private void pushDataToPlayerPrefs(){
        //Read data from dbInitData and write it to PlayerPrefs
        PlayerPrefs.SetString("Timestamp",dbInitData["data"][0]["Timestamp"]);
        PlayerPrefs.SetInt("2WheelerLicense",int.Parse(dbInitData["data"][0]["License2W"]));
        PlayerPrefs.SetInt("3WheelerLicense",int.Parse(dbInitData["data"][0]["License3W"]));
        PlayerPrefs.SetInt("4WheelerLicense",int.Parse(dbInitData["data"][0]["License4W"]));
        PlayerPrefs.SetInt("6WheelerLicense",int.Parse(dbInitData["data"][0]["License6W"]));
        PlayerPrefs.SetInt("MoneyBank",int.Parse(dbInitData["data"][0]["MoneyBank"]));
        // PlayerPrefs.SetInt("MoneyPocket",int.Parse(dbInitData["data"][0]["MoneyPocket"]));
        PlayerPrefs.SetInt("Health",int.Parse(dbInitData["data"][0]["Health"]));
        PlayerPrefs.SetInt("MoneyPerHealth",int.Parse(dbInitData["data"][0]["MoneyPerHealth"]));
        PlayerPrefs.SetInt("FoodPrice",int.Parse(dbInitData["data"][0]["FoodPrice"]));
        PlayerPrefs.SetInt("PetrolPrice",int.Parse(dbInitData["data"][0]["PetrolPrice"]));
        PlayerPrefs.SetFloat("TotalDistanceTraveled",float.Parse(dbInitData["data"][0]["TotalDistanceTraveled"]));
        
        #region Setting PlayerPrefs with the Vehicle stats from DB
        #region THIRD PERSON
        //Vibe
        PlayerPrefs.SetString("Vibe2009rig-redoCSY_Unlocked",dbInitData["data"][0]["Vibe2009rig-redoCSY_Unlocked"]);
        PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_Accl",float.Parse(dbInitData["data"][0]["Vibe2009rig-redoCSY_Accl"]));
        PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_MaxSpeed",float.Parse(dbInitData["data"][0]["Vibe2009rig-redoCSY_MaxSpeed"]));
        PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_TankCapacity",float.Parse(dbInitData["data"][0]["Vibe2009rig-redoCSY_TankCapacity"]));
        PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_Mileage",float.Parse(dbInitData["data"][0]["Vibe2009rig-redoCSY_Mileage"]));
        PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_Brake",float.Parse(dbInitData["data"][0]["Vibe2009rig-redoCSY_Brake"]));
        PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_Steer",float.Parse(dbInitData["data"][0]["Vibe2009rig-redoCSY_Steer"]));
        PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_FR",float.Parse(dbInitData["data"][0]["Vibe2009rig-redoCSY_FR"]));
        PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_TotalDistance",float.Parse(dbInitData["data"][0]["Vibe2009rig-redoCSY_TotalDistance"]));
        PlayerPrefs.SetString("Vibe2009rig-redoCSY_KIT",dbInitData["data"][0]["Vibe2009rig-redoCSY_KIT"]);
        PlayerPrefs.SetString("Vibe2009rig-redoCSY_MAT",dbInitData["data"][0]["Vibe2009rig-redoCSY_MAT"]);
        #endregion THIRD PERSON

        #region TWO WHEELERS
        //HN-Dio
        PlayerPrefs.SetString("HN-Dio_Unlocked",dbInitData["data"][0]["HN-Dio_Unlocked"]);
        PlayerPrefs.SetFloat("HN-Dio_Accl",float.Parse(dbInitData["data"][0]["HN-Dio_Accl"]));
        PlayerPrefs.SetFloat("HN-Dio_MaxSpeed",float.Parse(dbInitData["data"][0]["HN-Dio_MaxSpeed"]));
        PlayerPrefs.SetFloat("HN-Dio_TankCapacity",float.Parse(dbInitData["data"][0]["HN-Dio_TankCapacity"]));
        PlayerPrefs.SetFloat("HN-Dio_Mileage",float.Parse(dbInitData["data"][0]["HN-Dio_Mileage"]));
        PlayerPrefs.SetFloat("HN-Dio_Brake",float.Parse(dbInitData["data"][0]["HN-Dio_Brake"]));
        PlayerPrefs.SetFloat("HN-Dio_Steer",float.Parse(dbInitData["data"][0]["HN-Dio_Steer"]));
        PlayerPrefs.SetFloat("HN-Dio_FR",float.Parse(dbInitData["data"][0]["HN-Dio_FR"]));
        PlayerPrefs.SetFloat("HN-Dio_TotalDistance",float.Parse(dbInitData["data"][0]["HN-Dio_TotalDistance"]));
        PlayerPrefs.SetString("HN-Dio_KIT",dbInitData["data"][0]["HN-Dio_KIT"]);
        PlayerPrefs.SetString("HN-Dio_MAT",dbInitData["data"][0]["HN-Dio_MAT"]);
        
        //BJ-Chetak
        PlayerPrefs.SetString("BJ-Chetak_Unlocked",dbInitData["data"][0]["BJ-Chetak_Unlocked"]);
        PlayerPrefs.SetFloat("BJ-Chetak_Accl",float.Parse(dbInitData["data"][0]["BJ-Chetak_Accl"]));
        PlayerPrefs.SetFloat("BJ-Chetak_MaxSpeed",float.Parse(dbInitData["data"][0]["BJ-Chetak_MaxSpeed"]));
        PlayerPrefs.SetFloat("BJ-Chetak_TankCapacity",float.Parse(dbInitData["data"][0]["BJ-Chetak_TankCapacity"]));
        PlayerPrefs.SetFloat("BJ-Chetak_Mileage",float.Parse(dbInitData["data"][0]["BJ-Chetak_Mileage"]));
        PlayerPrefs.SetFloat("BJ-Chetak_Brake",float.Parse(dbInitData["data"][0]["BJ-Chetak_Brake"]));
        PlayerPrefs.SetFloat("BJ-Chetak_Steer",float.Parse(dbInitData["data"][0]["BJ-Chetak_Steer"]));
        PlayerPrefs.SetFloat("BJ-Chetak_FR",float.Parse(dbInitData["data"][0]["BJ-Chetak_FR"]));
        PlayerPrefs.SetFloat("BJ-Chetak_TotalDistance",float.Parse(dbInitData["data"][0]["BJ-Chetak_TotalDistance"]));
        PlayerPrefs.SetString("BJ-Chetak_KIT",dbInitData["data"][0]["BJ-Chetak_KIT"]);
        PlayerPrefs.SetString("BJ-Chetak_MAT",dbInitData["data"][0]["BJ-Chetak_MAT"]);

        //BJ-Pulsar
        PlayerPrefs.SetString("BJ-Pulsar_Unlocked",dbInitData["data"][0]["BJ-Pulsar_Unlocked"]);
        PlayerPrefs.SetFloat("BJ-Pulsar_Accl",float.Parse(dbInitData["data"][0]["BJ-Pulsar_Accl"]));
        PlayerPrefs.SetFloat("BJ-Pulsar_MaxSpeed",float.Parse(dbInitData["data"][0]["BJ-Pulsar_MaxSpeed"]));
        PlayerPrefs.SetFloat("BJ-Pulsar_TankCapacity",float.Parse(dbInitData["data"][0]["BJ-Pulsar_TankCapacity"]));
        PlayerPrefs.SetFloat("BJ-Pulsar_Mileage",float.Parse(dbInitData["data"][0]["BJ-Pulsar_Mileage"]));
        PlayerPrefs.SetFloat("BJ-Pulsar_Brake",float.Parse(dbInitData["data"][0]["BJ-Pulsar_Brake"]));
        PlayerPrefs.SetFloat("BJ-Pulsar_Steer",float.Parse(dbInitData["data"][0]["BJ-Pulsar_Steer"]));
        PlayerPrefs.SetFloat("BJ-Pulsar_FR",float.Parse(dbInitData["data"][0]["BJ-Pulsar_FR"]));
        PlayerPrefs.SetFloat("BJ-Pulsar_TotalDistance",float.Parse(dbInitData["data"][0]["BJ-Pulsar_TotalDistance"]));
        PlayerPrefs.SetString("BJ-Pulsar_KIT",dbInitData["data"][0]["BJ-Pulsar_KIT"]);
        PlayerPrefs.SetString("BJ-Pulsar_MAT",dbInitData["data"][0]["BJ-Pulsar_MAT"]);

        #endregion TWO WHEELERS

        #region FOUR WHEELERS
        //Ace
        PlayerPrefs.SetString("Ace_Unlocked",dbInitData["data"][0]["Ace_Unlocked"]);
        PlayerPrefs.SetFloat("Ace_Accl",float.Parse(dbInitData["data"][0]["Ace_Accl"]));
        PlayerPrefs.SetFloat("Ace_MaxSpeed",float.Parse(dbInitData["data"][0]["Ace_MaxSpeed"]));
        PlayerPrefs.SetFloat("Ace_TankCapacity",float.Parse(dbInitData["data"][0]["Ace_TankCapacity"]));
        PlayerPrefs.SetFloat("Ace_Mileage",float.Parse(dbInitData["data"][0]["Ace_Mileage"]));
        PlayerPrefs.SetFloat("Ace_Brake",float.Parse(dbInitData["data"][0]["Ace_Brake"]));
        PlayerPrefs.SetFloat("Ace_Steer",float.Parse(dbInitData["data"][0]["Ace_Steer"]));
        PlayerPrefs.SetFloat("Ace_FR",float.Parse(dbInitData["data"][0]["Ace_FR"]));
        PlayerPrefs.SetFloat("Ace_TotalDistance",float.Parse(dbInitData["data"][0]["Ace_TotalDistance"]));
        PlayerPrefs.SetString("Ace_KIT",dbInitData["data"][0]["Ace_KIT"]);
        PlayerPrefs.SetString("Ace_MAT",dbInitData["data"][0]["Ace_MAT"]);

        //Ambassador
        PlayerPrefs.SetString("Ambassador_Unlocked",dbInitData["data"][0]["Ambassador_Unlocked"]);
        PlayerPrefs.SetFloat("Ambassador_Accl",float.Parse(dbInitData["data"][0]["Ambassador_Accl"]));
        PlayerPrefs.SetFloat("Ambassador_MaxSpeed",float.Parse(dbInitData["data"][0]["Ambassador_MaxSpeed"]));
        PlayerPrefs.SetFloat("Ambassador_TankCapacity",float.Parse(dbInitData["data"][0]["Ambassador_TankCapacity"]));
        PlayerPrefs.SetFloat("Ambassador_Mileage",float.Parse(dbInitData["data"][0]["Ambassador_Mileage"]));
        PlayerPrefs.SetFloat("Ambassador_Brake",float.Parse(dbInitData["data"][0]["Ambassador_Brake"]));
        PlayerPrefs.SetFloat("Ambassador_Steer",float.Parse(dbInitData["data"][0]["Ambassador_Steer"]));
        PlayerPrefs.SetFloat("Ambassador_FR",float.Parse(dbInitData["data"][0]["Ambassador_FR"]));
        PlayerPrefs.SetFloat("Ambassador_TotalDistance",float.Parse(dbInitData["data"][0]["Ambassador_TotalDistance"]));
        PlayerPrefs.SetString("Ambassador_KIT",dbInitData["data"][0]["Ambassador_KIT"]);
        PlayerPrefs.SetString("Ambassador_MAT",dbInitData["data"][0]["Ambassador_MAT"]);

        //Indica
        PlayerPrefs.SetString("Indica_Unlocked",dbInitData["data"][0]["Indica_Unlocked"]);
        PlayerPrefs.SetFloat("Indica_Accl",float.Parse(dbInitData["data"][0]["Indica_Accl"]));
        PlayerPrefs.SetFloat("Indica_MaxSpeed",float.Parse(dbInitData["data"][0]["Indica_MaxSpeed"]));
        PlayerPrefs.SetFloat("Indica_TankCapacity",float.Parse(dbInitData["data"][0]["Indica_TankCapacity"]));
        PlayerPrefs.SetFloat("Indica_Mileage",float.Parse(dbInitData["data"][0]["Indica_Mileage"]));
        PlayerPrefs.SetFloat("Indica_Brake",float.Parse(dbInitData["data"][0]["Indica_Brake"]));
        PlayerPrefs.SetFloat("Indica_Steer",float.Parse(dbInitData["data"][0]["Indica_Steer"]));
        PlayerPrefs.SetFloat("Indica_FR",float.Parse(dbInitData["data"][0]["Indica_FR"]));
        PlayerPrefs.SetFloat("Indica_TotalDistance",float.Parse(dbInitData["data"][0]["Indica_TotalDistance"]));
        PlayerPrefs.SetString("Indica_KIT",dbInitData["data"][0]["Indica_KIT"]);
        PlayerPrefs.SetString("Indica_MAT",dbInitData["data"][0]["Indica_MAT"]);

        //MS-800
        PlayerPrefs.SetString("MS-800_Unlocked",dbInitData["data"][0]["MS-800_Unlocked"]);
        PlayerPrefs.SetFloat("MS-800_Accl",float.Parse(dbInitData["data"][0]["MS-800_Accl"]));
        PlayerPrefs.SetFloat("MS-800_MaxSpeed",float.Parse(dbInitData["data"][0]["MS-800_MaxSpeed"]));
        PlayerPrefs.SetFloat("MS-800_TankCapacity",float.Parse(dbInitData["data"][0]["MS-800_TankCapacity"]));
        PlayerPrefs.SetFloat("MS-800_Mileage",float.Parse(dbInitData["data"][0]["MS-800_Mileage"]));
        PlayerPrefs.SetFloat("MS-800_Brake",float.Parse(dbInitData["data"][0]["MS-800_Brake"]));
        PlayerPrefs.SetFloat("MS-800_Steer",float.Parse(dbInitData["data"][0]["MS-800_Steer"]));
        PlayerPrefs.SetFloat("MS-800_FR",float.Parse(dbInitData["data"][0]["MS-800_FR"]));
        PlayerPrefs.SetFloat("MS-800_TotalDistance",float.Parse(dbInitData["data"][0]["MS-800_TotalDistance"]));
        PlayerPrefs.SetString("MS-800_KIT",dbInitData["data"][0]["MS-800_KIT"]);
        PlayerPrefs.SetString("MS-800_MAT",dbInitData["data"][0]["MS-800_MAT"]);

        //MS-Alto
        PlayerPrefs.SetString("MS-Alto_Unlocked",dbInitData["data"][0]["MS-Alto_Unlocked"]);
        PlayerPrefs.SetFloat("MS-Alto_Accl",float.Parse(dbInitData["data"][0]["MS-Alto_Accl"]));
        PlayerPrefs.SetFloat("MS-Alto_MaxSpeed",float.Parse(dbInitData["data"][0]["MS-Alto_MaxSpeed"]));
        PlayerPrefs.SetFloat("MS-Alto_TankCapacity",float.Parse(dbInitData["data"][0]["MS-Alto_TankCapacity"]));
        PlayerPrefs.SetFloat("MS-Alto_Mileage",float.Parse(dbInitData["data"][0]["MS-Alto_Mileage"]));
        PlayerPrefs.SetFloat("MS-Alto_Brake",float.Parse(dbInitData["data"][0]["MS-Alto_Brake"]));
        PlayerPrefs.SetFloat("MS-Alto_Steer",float.Parse(dbInitData["data"][0]["MS-Alto_Steer"]));
        PlayerPrefs.SetFloat("MS-Alto_FR",float.Parse(dbInitData["data"][0]["MS-Alto_FR"]));
        PlayerPrefs.SetFloat("MS-Alto_TotalDistance",float.Parse(dbInitData["data"][0]["MS-Alto_TotalDistance"]));
        PlayerPrefs.SetString("MS-Alto_KIT",dbInitData["data"][0]["MS-Alto_KIT"]);
        PlayerPrefs.SetString("MS-Alto_MAT",dbInitData["data"][0]["MS-Alto_MAT"]);

        //Nano
        PlayerPrefs.SetString("Nano_Unlocked",dbInitData["data"][0]["Nano_Unlocked"]);
        PlayerPrefs.SetFloat("Nano_Accl",float.Parse(dbInitData["data"][0]["Nano_Accl"]));
        PlayerPrefs.SetFloat("Nano_MaxSpeed",float.Parse(dbInitData["data"][0]["Nano_MaxSpeed"]));
        PlayerPrefs.SetFloat("Nano_TankCapacity",float.Parse(dbInitData["data"][0]["Nano_TankCapacity"]));
        PlayerPrefs.SetFloat("Nano_Mileage",float.Parse(dbInitData["data"][0]["Nano_Mileage"]));
        PlayerPrefs.SetFloat("Nano_Brake",float.Parse(dbInitData["data"][0]["Nano_Brake"]));
        PlayerPrefs.SetFloat("Nano_Steer",float.Parse(dbInitData["data"][0]["Nano_Steer"]));
        PlayerPrefs.SetFloat("Nano_FR",float.Parse(dbInitData["data"][0]["Nano_FR"]));
        PlayerPrefs.SetFloat("Nano_TotalDistance",float.Parse(dbInitData["data"][0]["Nano_TotalDistance"]));
        PlayerPrefs.SetString("Nano_KIT",dbInitData["data"][0]["Nano_KIT"]);
        PlayerPrefs.SetString("Nano_MAT",dbInitData["data"][0]["Nano_MAT"]);

        //Scorpio
        PlayerPrefs.SetString("Scorpio_Unlocked",dbInitData["data"][0]["Scorpio_Unlocked"]);
        PlayerPrefs.SetFloat("Scorpio_Accl",float.Parse(dbInitData["data"][0]["Scorpio_Accl"]));
        PlayerPrefs.SetFloat("Scorpio_MaxSpeed",float.Parse(dbInitData["data"][0]["Scorpio_MaxSpeed"]));
        PlayerPrefs.SetFloat("Scorpio_TankCapacity",float.Parse(dbInitData["data"][0]["Scorpio_TankCapacity"]));
        PlayerPrefs.SetFloat("Scorpio_Mileage",float.Parse(dbInitData["data"][0]["Scorpio_Mileage"]));
        PlayerPrefs.SetFloat("Scorpio_Brake",float.Parse(dbInitData["data"][0]["Scorpio_Brake"]));
        PlayerPrefs.SetFloat("Scorpio_Steer",float.Parse(dbInitData["data"][0]["Scorpio_Steer"]));
        PlayerPrefs.SetFloat("Scorpio_FR",float.Parse(dbInitData["data"][0]["Scorpio_FR"]));
        PlayerPrefs.SetFloat("Scorpio_TotalDistance",float.Parse(dbInitData["data"][0]["Scorpio_TotalDistance"]));
        PlayerPrefs.SetString("Scorpio_KIT",dbInitData["data"][0]["Scorpio_KIT"]);
        PlayerPrefs.SetString("Scorpio_MAT",dbInitData["data"][0]["Scorpio_MAT"]);

        //VJM02
        PlayerPrefs.SetString("VJM02_Unlocked",dbInitData["data"][0]["VJM02_Unlocked"]);
        PlayerPrefs.SetFloat("VJM02_Accl",float.Parse(dbInitData["data"][0]["VJM02_Accl"]));
        PlayerPrefs.SetFloat("VJM02_MaxSpeed",float.Parse(dbInitData["data"][0]["VJM02_MaxSpeed"]));
        PlayerPrefs.SetFloat("VJM02_TankCapacity",float.Parse(dbInitData["data"][0]["VJM02_TankCapacity"]));
        PlayerPrefs.SetFloat("VJM02_Mileage",float.Parse(dbInitData["data"][0]["VJM02_Mileage"]));
        PlayerPrefs.SetFloat("VJM02_Brake",float.Parse(dbInitData["data"][0]["VJM02_Brake"]));
        PlayerPrefs.SetFloat("VJM02_Steer",float.Parse(dbInitData["data"][0]["VJM02_Steer"]));
        PlayerPrefs.SetFloat("VJM02_FR",float.Parse(dbInitData["data"][0]["VJM02_FR"]));
        PlayerPrefs.SetFloat("VJM02_TotalDistance",float.Parse(dbInitData["data"][0]["VJM02_TotalDistance"]));
        PlayerPrefs.SetString("VJM02_KIT",dbInitData["data"][0]["VJM02_KIT"]);
        PlayerPrefs.SetString("VJM02_MAT",dbInitData["data"][0]["VJM02_MAT"]);

        //BMTC_1wopass
        PlayerPrefs.SetString("BMTC_1wopass_Unlocked",dbInitData["data"][0]["BMTC_1wopass_Unlocked"]);
        PlayerPrefs.SetFloat("BMTC_1wopass_Accl",float.Parse(dbInitData["data"][0]["BMTC_1wopass_Accl"]));
        PlayerPrefs.SetFloat("BMTC_1wopass_MaxSpeed",float.Parse(dbInitData["data"][0]["BMTC_1wopass_MaxSpeed"]));
        PlayerPrefs.SetFloat("BMTC_1wopass_TankCapacity",float.Parse(dbInitData["data"][0]["BMTC_1wopass_TankCapacity"]));
        PlayerPrefs.SetFloat("BMTC_1wopass_Mileage",float.Parse(dbInitData["data"][0]["BMTC_1wopass_Mileage"]));
        PlayerPrefs.SetFloat("BMTC_1wopass_Brake",float.Parse(dbInitData["data"][0]["BMTC_1wopass_Brake"]));
        PlayerPrefs.SetFloat("BMTC_1wopass_Steer",float.Parse(dbInitData["data"][0]["BMTC_1wopass_Steer"]));
        PlayerPrefs.SetFloat("BMTC_1wopass_FR",float.Parse(dbInitData["data"][0]["BMTC_1wopass_FR"]));
        PlayerPrefs.SetFloat("BMTC_1wopass_TotalDistance",float.Parse(dbInitData["data"][0]["BMTC_1wopass_TotalDistance"]));
        PlayerPrefs.SetString("BMTC_1wopass_KIT",dbInitData["data"][0]["BMTC_1wopass_KIT"]);
        PlayerPrefs.SetString("BMTC_1wopass_MAT",dbInitData["data"][0]["BMTC_1wopass_MAT"]);

        //Tempo
        PlayerPrefs.SetString("Tempo_Unlocked",dbInitData["data"][0]["Tempo_Unlocked"]);
        PlayerPrefs.SetFloat("Tempo_Accl",float.Parse(dbInitData["data"][0]["Tempo_Accl"]));
        PlayerPrefs.SetFloat("Tempo_MaxSpeed",float.Parse(dbInitData["data"][0]["Tempo_MaxSpeed"]));
        PlayerPrefs.SetFloat("Tempo_TankCapacity",float.Parse(dbInitData["data"][0]["Tempo_TankCapacity"]));
        PlayerPrefs.SetFloat("Tempo_Mileage",float.Parse(dbInitData["data"][0]["Tempo_Mileage"]));
        PlayerPrefs.SetFloat("Tempo_Brake",float.Parse(dbInitData["data"][0]["Tempo_Brake"]));
        PlayerPrefs.SetFloat("Tempo_Steer",float.Parse(dbInitData["data"][0]["Tempo_Steer"]));
        PlayerPrefs.SetFloat("Tempo_FR",float.Parse(dbInitData["data"][0]["Tempo_FR"]));
        PlayerPrefs.SetFloat("Tempo_TotalDistance",float.Parse(dbInitData["data"][0]["Tempo_TotalDistance"]));
        PlayerPrefs.SetString("Tempo_KIT",dbInitData["data"][0]["Tempo_KIT"]);
        PlayerPrefs.SetString("Tempo_MAT",dbInitData["data"][0]["Tempo_MAT"]);


        #endregion FOUR WHEELERS


        #region THREE WHEELERS
        //BA-RE
        PlayerPrefs.SetString("BA-RE_Unlocked",dbInitData["data"][0]["BA-RE_Unlocked"]);
        PlayerPrefs.SetFloat("BA-RE_Accl",float.Parse(dbInitData["data"][0]["BA-RE_Accl"]));
        PlayerPrefs.SetFloat("BA-RE_MaxSpeed",float.Parse(dbInitData["data"][0]["BA-RE_MaxSpeed"]));
        PlayerPrefs.SetFloat("BA-RE_TankCapacity",float.Parse(dbInitData["data"][0]["BA-RE_TankCapacity"]));
        PlayerPrefs.SetFloat("BA-RE_Mileage",float.Parse(dbInitData["data"][0]["BA-RE_Mileage"]));
        PlayerPrefs.SetFloat("BA-RE_Brake",float.Parse(dbInitData["data"][0]["BA-RE_Brake"]));
        PlayerPrefs.SetFloat("BA-RE_Steer",float.Parse(dbInitData["data"][0]["BA-RE_Steer"]));
        PlayerPrefs.SetFloat("BA-RE_FR",float.Parse(dbInitData["data"][0]["BA-RE_FR"]));
        PlayerPrefs.SetFloat("BA-RE_TotalDistance",float.Parse(dbInitData["data"][0]["BA-RE_TotalDistance"]));
        PlayerPrefs.SetString("BA-RE_KIT",dbInitData["data"][0]["BA-RE_KIT"]);
        PlayerPrefs.SetString("BA-RE_MAT",dbInitData["data"][0]["BA-RE_MAT"]);

        //BA-RE-goods
        PlayerPrefs.SetString("BA-RE-goods_Unlocked",dbInitData["data"][0]["BA-RE-goods_Unlocked"]);
        PlayerPrefs.SetFloat("BA-RE-goods_Accl",float.Parse(dbInitData["data"][0]["BA-RE-goods_Accl"]));
        PlayerPrefs.SetFloat("BA-RE-goods_MaxSpeed",float.Parse(dbInitData["data"][0]["BA-RE-goods_MaxSpeed"]));
        PlayerPrefs.SetFloat("BA-RE-goods_TankCapacity",float.Parse(dbInitData["data"][0]["BA-RE-goods_TankCapacity"]));
        PlayerPrefs.SetFloat("BA-RE-goods_Mileage",float.Parse(dbInitData["data"][0]["BA-RE-goods_Mileage"]));
        PlayerPrefs.SetFloat("BA-RE-goods_Brake",float.Parse(dbInitData["data"][0]["BA-RE-goods_Brake"]));
        PlayerPrefs.SetFloat("BA-RE-goods_Steer",float.Parse(dbInitData["data"][0]["BA-RE-goods_Steer"]));
        PlayerPrefs.SetFloat("BA-RE-goods_FR",float.Parse(dbInitData["data"][0]["BA-RE-goods_FR"]));
        PlayerPrefs.SetFloat("BA-RE-goods_TotalDistance",float.Parse(dbInitData["data"][0]["BA-RE-goods_TotalDistance"]));
        PlayerPrefs.SetString("BA-RE-goods_KIT",dbInitData["data"][0]["BA-RE-goods_KIT"]);
        PlayerPrefs.SetString("BA-RE-goods_MAT",dbInitData["data"][0]["BA-RE-goods_MAT"]);

        //Ape
        PlayerPrefs.SetString("Ape_Unlocked",dbInitData["data"][0]["Ape_Unlocked"]);
        PlayerPrefs.SetFloat("Ape_Accl",float.Parse(dbInitData["data"][0]["Ape_Accl"]));
        PlayerPrefs.SetFloat("Ape_MaxSpeed",float.Parse(dbInitData["data"][0]["Ape_MaxSpeed"]));
        PlayerPrefs.SetFloat("Ape_TankCapacity",float.Parse(dbInitData["data"][0]["Ape_TankCapacity"]));
        PlayerPrefs.SetFloat("Ape_Mileage",float.Parse(dbInitData["data"][0]["Ape_Mileage"]));
        PlayerPrefs.SetFloat("Ape_Brake",float.Parse(dbInitData["data"][0]["Ape_Brake"]));
        PlayerPrefs.SetFloat("Ape_Steer",float.Parse(dbInitData["data"][0]["Ape_Steer"]));
        PlayerPrefs.SetFloat("Ape_FR",float.Parse(dbInitData["data"][0]["Ape_FR"]));
        PlayerPrefs.SetFloat("Ape_TotalDistance",float.Parse(dbInitData["data"][0]["Ape_TotalDistance"]));
        PlayerPrefs.SetString("Ape_KIT",dbInitData["data"][0]["Ape_KIT"]);
        PlayerPrefs.SetString("Ape_MAT",dbInitData["data"][0]["Ape_MAT"]);

        //BullockCart_1
        PlayerPrefs.SetString("BullockCart_1_Unlocked",dbInitData["data"][0]["BullockCart_1_Unlocked"]);
        PlayerPrefs.SetFloat("BullockCart_1_Accl",float.Parse(dbInitData["data"][0]["BullockCart_1_Accl"]));
        PlayerPrefs.SetFloat("BullockCart_1_MaxSpeed",float.Parse(dbInitData["data"][0]["BullockCart_1_MaxSpeed"]));
        PlayerPrefs.SetFloat("BullockCart_1_TankCapacity",float.Parse(dbInitData["data"][0]["BullockCart_1_TankCapacity"]));
        PlayerPrefs.SetFloat("BullockCart_1_Mileage",float.Parse(dbInitData["data"][0]["BullockCart_1_Mileage"]));
        PlayerPrefs.SetFloat("BullockCart_1_Brake",float.Parse(dbInitData["data"][0]["BullockCart_1_Brake"]));
        PlayerPrefs.SetFloat("BullockCart_1_Steer",float.Parse(dbInitData["data"][0]["BullockCart_1_Steer"]));
        PlayerPrefs.SetFloat("BullockCart_1_FR",float.Parse(dbInitData["data"][0]["BullockCart_1_FR"]));
        PlayerPrefs.SetFloat("BullockCart_1_TotalDistance",float.Parse(dbInitData["data"][0]["BullockCart_1_TotalDistance"]));
        PlayerPrefs.SetString("BullockCart_1_KIT",dbInitData["data"][0]["BullockCart_1_KIT"]);
        PlayerPrefs.SetString("BullockCart_1_MAT",dbInitData["data"][0]["BullockCart_1_MAT"]);

        #endregion THREE WHEELERS
        
        #endregion Setting PlayerPrefs with the Vehicle stats from DB
    }

    private void HideTheseObjects(){
        for(int i =0;i<Objectlisthide.Length;i++){
            (Objectlisthide[i]).SetActive(false);
        }
        for(int j =0;j<Objectlistshow.Length;j++){
            (Objectlistshow[j]).SetActive(true);
        }
    }
    public void UnHideTheseObjects(){
        for(int i =0;i<Objectlisthide.Length;i++){
            if(Objectlisthide[i].name == "InputField (TMP)" && PlayerPrefs.HasKey("PlayerName")){
                string username = PlayerPrefs.GetString("PlayerName");
                Playername.SetText(username);
                (Objectlisthide[i]).SetActive(false);
                
            }else{
                (Objectlisthide[i]).SetActive(true);
            }
        }
        for(int j =0;j<Objectlistshow.Length;j++){
            (Objectlistshow[j]).SetActive(false);
        }
    }

    

    public void SetStatsInClassroom(){
        // Playername = GameObject.Find("Playername").GetComponent<TextMeshProUGUI>();
        try{
            string username = PlayerPrefs.GetString("PlayerName");
            Playername.SetText(username);
            Debug.Log("Set username in Classroom");

            int MoneyBank = PlayerPrefs.GetInt("MoneyBank");
            Money.SetText((MoneyBank).ToString());

            float totalDist = PlayerPrefs.GetFloat("TotalDistanceTraveled");
            TotalDistance.SetText((totalDist).ToString());

            int lifeCost = PlayerPrefs.GetInt("MoneyPerHealth");
            LifePrice.SetText((lifeCost).ToString());

            int foodCost = PlayerPrefs.GetInt("FoodPrice");
            FoodPrice.SetText((foodCost).ToString());

            int petrolCost = PlayerPrefs.GetInt("PetrolPrice");
            PetrolPrice.SetText((petrolCost).ToString());
        }catch{
            Debug.Log("Could not set username");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
