using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using System;
using TMPro;


public class FetchInitDBData : MonoBehaviour
{
    private Init initData;

    private string dbTimestamp;
    private string playerPrefsTimestamp;

    private string unityUserID;
    private JSONNode dbInitData;
    private string NetworkCheck = "";
    public TextMeshProUGUI Playername;
    // public Translater Translate;

    public string id = "1";
    public GameObject[] Objectlistshow;
    public GameObject[] Objectlisthide;
    SimpleJSON.JSONObject playerJson = new SimpleJSON.JSONObject();


    // Start is called before the first frame update
    void Start()
    {   
        HideTheseObjects();
        Init initDat = new Init();
        StartCoroutine(initDat.Download("", result => {
            NetworkCheck = result;
            Debug.Log(NetworkCheck);
        }));
        SetUsernameInClassroom();
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
            #region Setting Default Vehicle Stats to PlayerPrefs
            PlayerPrefs.SetString("Timestamp",DateTime.Now.ToString());
            PlayerPrefs.SetInt("2WheelerLicense",1);
            PlayerPrefs.SetInt("3WheelerLicense",1);
            PlayerPrefs.SetInt("4WheelerLicense",0);
            PlayerPrefs.SetInt("MoneyBank",20000);
            PlayerPrefs.SetInt("MoneyPocket",2000);
            PlayerPrefs.SetInt("Health",50);
            PlayerPrefs.SetInt("MoneyPerHealth",5);
            PlayerPrefs.SetFloat("TotalDistanceTraveled",0f);

            #region THIRD PERSON
            //Vibe
            PlayerPrefs.SetString("Vibe2009rig-redoCSY_Unlocked","1");
            PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_Accl",0.075f);
            PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_MaxSpeed",0.080f);
            PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_TankCapacity",12f);
            PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_Mileage",0.180f);
            PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_Brake",-0.075f);
            PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_Steer",0.045f);
            PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_FR",1f);
            PlayerPrefs.SetFloat("Vibe2009rig-redoCSY_TotalDistance",0f);
            PlayerPrefs.SetString("Vibe2009rig-redoCSY_KIT","Stock0");
            PlayerPrefs.SetString("Vibe2009rig-redoCSY_MAT","HN-Dio_Stock0");
            #endregion THIRD PERSON

            #region TWO WHEELERS
            //HN-Dio
            PlayerPrefs.SetString("HN-Dio_Unlocked","1");
            PlayerPrefs.SetFloat("HN-Dio_Accl",0.075f);
            PlayerPrefs.SetFloat("HN-Dio_MaxSpeed",0.080f);
            PlayerPrefs.SetFloat("HN-Dio_TankCapacity",12f);
            PlayerPrefs.SetFloat("HN-Dio_Mileage",0.180f);
            PlayerPrefs.SetFloat("HN-Dio_Brake",-0.075f);
            PlayerPrefs.SetFloat("HN-Dio_Steer",0.045f);
            PlayerPrefs.SetFloat("HN-Dio_FR",1f);
            PlayerPrefs.SetFloat("HN-Dio_TotalDistance",0f);
            PlayerPrefs.SetString("HN-Dio_KIT","Stock0");
            PlayerPrefs.SetString("HN-Dio_MAT","HN-Dio_Stock0");
            //BJ-Chetak
            PlayerPrefs.SetString("BJ-Chetak_Unlocked","1");
            PlayerPrefs.SetFloat("BJ-Chetak_Accl",0.075f);
            PlayerPrefs.SetFloat("BJ-Chetak_MaxSpeed",0.070f);
            PlayerPrefs.SetFloat("BJ-Chetak_TankCapacity",12f);
            PlayerPrefs.SetFloat("BJ-Chetak_Mileage",0.180f);
            PlayerPrefs.SetFloat("BJ-Chetak_Brake",-0.075f);
            PlayerPrefs.SetFloat("BJ-Chetak_Steer",0.045f);
            PlayerPrefs.SetFloat("BJ-Chetak_FR",1f);
            PlayerPrefs.SetFloat("BJ-Chetak_TotalDistance",0f);
            PlayerPrefs.SetString("BJ-Chetak_KIT","Stock0");
            PlayerPrefs.SetString("BJ-Chetak_MAT","BJ-Chetak_Stock0");
            //BJ-Pulsar
            PlayerPrefs.SetString("BJ-Pulsar_Unlocked","1");
            PlayerPrefs.SetFloat("BJ-Pulsar_Accl",0.075f);
            PlayerPrefs.SetFloat("BJ-Pulsar_MaxSpeed",0.085f);
            PlayerPrefs.SetFloat("BJ-Pulsar_TankCapacity",12f);
            PlayerPrefs.SetFloat("BJ-Pulsar_Mileage",0.180f);
            PlayerPrefs.SetFloat("BJ-Pulsar_Brake",-0.075f);
            PlayerPrefs.SetFloat("BJ-Pulsar_Steer",0.045f);
            PlayerPrefs.SetFloat("BJ-Pulsar_FR",1f);
            PlayerPrefs.SetFloat("BJ-Pulsar_TotalDistance",0f);
            PlayerPrefs.SetString("BJ-Pulsar_KIT","Stock0");
            PlayerPrefs.SetString("BJ-Pulsar_MAT","BJ-Pulsar_Stock0");

            #endregion TWO WHEELERS


            #region FOUR WHEELERS
            //Ace
            PlayerPrefs.SetString("Ace_Unlocked","1");
            PlayerPrefs.SetFloat("Ace_Accl",0.75f);
            PlayerPrefs.SetFloat("Ace_MaxSpeed",0.040f);
            PlayerPrefs.SetFloat("Ace_TankCapacity",50f);
            PlayerPrefs.SetFloat("Ace_Mileage",0.100f);
            PlayerPrefs.SetFloat("Ace_Brake",-0.075f);
            PlayerPrefs.SetFloat("Ace_Steer",0.030f);
            PlayerPrefs.SetFloat("Ace_FR",1f);
            PlayerPrefs.SetFloat("Ace_TotalDistance",0f);
            PlayerPrefs.SetString("Ace_KIT","Stock0");
            PlayerPrefs.SetString("Ace_MAT","Ace_Stock0");

            //Ambassador
            PlayerPrefs.SetString("Ambassador_Unlocked","1");
            PlayerPrefs.SetFloat("Ambassador_Accl",0.75f);
            PlayerPrefs.SetFloat("Ambassador_MaxSpeed",0.040f);
            PlayerPrefs.SetFloat("Ambassador_TankCapacity",50f);
            PlayerPrefs.SetFloat("Ambassador_Mileage",0.100f);
            PlayerPrefs.SetFloat("Ambassador_Brake",-0.075f);
            PlayerPrefs.SetFloat("Ambassador_Steer",0.030f);
            PlayerPrefs.SetFloat("Ambassador_FR",1f);
            PlayerPrefs.SetFloat("Ambassador_TotalDistance",0f);
            PlayerPrefs.SetString("Ambassador_KIT","Stock0");
            PlayerPrefs.SetString("Ambassador_MAT","Ambassador_Stock0");

            //Indica
            PlayerPrefs.SetString("Indica_Unlocked","1");
            PlayerPrefs.SetFloat("Indica_Accl",0.50f);
            PlayerPrefs.SetFloat("Indica_MaxSpeed",0.070f);
            PlayerPrefs.SetFloat("Indica_TankCapacity",50f);
            PlayerPrefs.SetFloat("Indica_Mileage",0.100f);
            PlayerPrefs.SetFloat("Indica_Brake",-0.050f);
            PlayerPrefs.SetFloat("Indica_Steer",0.030f);
            PlayerPrefs.SetFloat("Indica_FR",1f);
            PlayerPrefs.SetFloat("Indica_TotalDistance",0f);
            PlayerPrefs.SetString("Indica_KIT","Stock0");
            PlayerPrefs.SetString("Indica_MAT","Indica_Stock0");

            //MS-800
            PlayerPrefs.SetString("MS-800_Unlocked","1");
            PlayerPrefs.SetFloat("MS-800_Accl",0.75f);
            PlayerPrefs.SetFloat("MS-800_MaxSpeed",0.030f);
            PlayerPrefs.SetFloat("MS-800_TankCapacity",50f);
            PlayerPrefs.SetFloat("MS-800_Mileage",0.100f);
            PlayerPrefs.SetFloat("MS-800_Brake",-0.075f);
            PlayerPrefs.SetFloat("MS-800_Steer",0.030f);
            PlayerPrefs.SetFloat("MS-800_FR",1f);
            PlayerPrefs.SetFloat("MS-800_TotalDistance",0f);
            PlayerPrefs.SetString("MS-800_KIT","Stock0");
            PlayerPrefs.SetString("MS-800_MAT","MS-800_Stock0");

            //MS-Alto
            PlayerPrefs.SetString("MS-Alto_Unlocked","1");
            PlayerPrefs.SetFloat("MS-Alto_Accl",0.60f);
            PlayerPrefs.SetFloat("MS-Alto_MaxSpeed",0.120f);
            PlayerPrefs.SetFloat("MS-Alto_TankCapacity",50f);
            PlayerPrefs.SetFloat("MS-Alto_Mileage",0.100f);
            PlayerPrefs.SetFloat("MS-Alto_Brake",-0.060f);
            PlayerPrefs.SetFloat("MS-Alto_Steer",0.050f);
            PlayerPrefs.SetFloat("MS-Alto_FR",1f);
            PlayerPrefs.SetFloat("MS-Alto_TotalDistance",0f);
            PlayerPrefs.SetString("MS-Alto_KIT","Stock0");
            PlayerPrefs.SetString("MS-Alto_MAT","MS-Alto_Stock0");

            //Nano
            PlayerPrefs.SetString("Nano_Unlocked","1");
            PlayerPrefs.SetFloat("Nano_Accl",0.50f);
            PlayerPrefs.SetFloat("Nano_MaxSpeed",0.030f);
            PlayerPrefs.SetFloat("Nano_TankCapacity",50f);
            PlayerPrefs.SetFloat("Nano_Mileage",0.100f);
            PlayerPrefs.SetFloat("Nano_Brake",-0.050f);
            PlayerPrefs.SetFloat("Nano_Steer",0.030f);
            PlayerPrefs.SetFloat("Nano_FR",1f);
            PlayerPrefs.SetFloat("Nano_TotalDistance",0f);
            PlayerPrefs.SetString("Nano_KIT","Stock0");
            PlayerPrefs.SetString("Nano_MAT","Nano_Stock0");

            //Scorpio
            PlayerPrefs.SetString("Scorpio_Unlocked","1");
            PlayerPrefs.SetFloat("Scorpio_Accl",0.80f);
            PlayerPrefs.SetFloat("Scorpio_MaxSpeed",0.150f);
            PlayerPrefs.SetFloat("Scorpio_TankCapacity",50f);
            PlayerPrefs.SetFloat("Scorpio_Mileage",0.100f);
            PlayerPrefs.SetFloat("Scorpio_Brake",-0.080f);
            PlayerPrefs.SetFloat("Scorpio_Steer",0.030f);
            PlayerPrefs.SetFloat("Scorpio_FR",1f);
            PlayerPrefs.SetFloat("Scorpio_TotalDistance",0f);
            PlayerPrefs.SetString("Scorpio_KIT","Stock0");
            PlayerPrefs.SetString("Scorpio_MAT","Scorpio_Stock0");

            //VJM02
            PlayerPrefs.SetString("VJM02_Unlocked","1");
            PlayerPrefs.SetFloat("VJM02_Accl",2f);
            PlayerPrefs.SetFloat("VJM02_MaxSpeed",0.3f);
            PlayerPrefs.SetFloat("VJM02_TankCapacity",143f);
            PlayerPrefs.SetFloat("VJM02_Mileage",0.3f);
            PlayerPrefs.SetFloat("VJM02_Brake",-2f);
            PlayerPrefs.SetFloat("VJM02_Steer",0.030f);
            PlayerPrefs.SetFloat("VJM02_FR",1f);
            PlayerPrefs.SetFloat("VJM02_TotalDistance",0f);
            PlayerPrefs.SetString("VJM02_KIT","Stock0");
            PlayerPrefs.SetString("VJM02_MAT","VJM02_Stock0");

            #endregion FOUR WHEELERS


            #region THREE WHEELERS
            //BA-RE
            PlayerPrefs.SetString("BA-RE_Unlocked","1");
            PlayerPrefs.SetFloat("BA-RE_Accl",0.4f);
            PlayerPrefs.SetFloat("BA-RE_MaxSpeed",0.060f);
            PlayerPrefs.SetFloat("BA-RE_TankCapacity",30f);
            PlayerPrefs.SetFloat("BA-RE_Mileage",0.040f);
            PlayerPrefs.SetFloat("BA-RE_Brake",-0.4f);
            PlayerPrefs.SetFloat("BA-RE_Steer",0.030f);
            PlayerPrefs.SetFloat("BA-RE_FR",1f);
            PlayerPrefs.SetFloat("BA-RE_TotalDistance",0f);
            PlayerPrefs.SetString("BA-RE_KIT","Stock0");
            PlayerPrefs.SetString("BA-RE_MAT","BA-RE_Stock0");

            //Ape
            PlayerPrefs.SetString("Ape_Unlocked","1");
            PlayerPrefs.SetFloat("Ape_Accl",0.30f);
            PlayerPrefs.SetFloat("Ape_MaxSpeed",0.030f);
            PlayerPrefs.SetFloat("Ape_TankCapacity",30f);
            PlayerPrefs.SetFloat("Ape_Mileage",0.040f);
            PlayerPrefs.SetFloat("Ape_Brake",-0.030f);
            PlayerPrefs.SetFloat("Ape_Steer",0.030f);
            PlayerPrefs.SetFloat("Ape_FR",1f);
            PlayerPrefs.SetFloat("Ape_TotalDistance",0f);
            PlayerPrefs.SetString("Ape_KIT","Stock0");
            PlayerPrefs.SetString("Ape_MAT","Ape_Stock0");

            #endregion THREE WHEELERS


            #endregion Setting Default Vehicle Stats to PlayerPrefs

            initData.apiParam = "playerStats/default/"+unityUserID;
            Debug.Log(initData.apiParam);
            skipTimeCheck = true;
        }else{
            PlayerPrefs.SetString("Timestamp",DateTime.Now.ToString());
            id = PlayerPrefs.GetString("PlayerID");
            initData.apiParam = "playerStats/"+id;
        }
        

        
        
        StartCoroutine(initData.Download(initData.apiParam, result => {
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

    public void validateAndPushToDB(){
        initData = new Init();
        id = PlayerPrefs.GetString("PlayerID");
        initData.apiParam = "playerStats/"+id;
        
        StartCoroutine(initData.Download(initData.apiParam, result => {
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

                StartCoroutine(initData.Post(playerJson, result =>{
                    string res = result;
                    Debug.Log("Post status "+res);
                }));
            }

        }));
        
    }


    private void pushDataToDB(){
        //Read Data from PlayerPrefs
        //And then push data to DB
        
        playerJson.Add("Timestamp",PlayerPrefs.GetString("Timestamp"));
        playerJson.Add("ID",PlayerPrefs.GetString("PlayerID"));
        playerJson.Add("License2W",PlayerPrefs.GetInt("2WheelerLicense"));
        playerJson.Add("License4W",PlayerPrefs.GetInt("4WheelerLicense"));
        playerJson.Add("Money",PlayerPrefs.GetFloat("Money"));
        playerJson.Add("Health",PlayerPrefs.GetInt("Health"));
        playerJson.Add("TotalDistanceTraveled",PlayerPrefs.GetFloat("TotalDistanceTraveled"));
        playerJson.Add("HN-Dio_TotalDistance",PlayerPrefs.GetFloat("HN-Dio_TotalDistance"));
        playerJson.Add("BJ-Chetak_TotalDistance",PlayerPrefs.GetFloat("BJ-Chetak_TotalDistance"));


        // Debug.Log(playerJson.ToString());
    }

    private void pushDataToPlayerPrefs(){
        //Read data from dbInitData and write it to PlayerPrefs
        PlayerPrefs.SetString("Timestamp",dbInitData["data"][0]["Timestamp"]);
        PlayerPrefs.SetInt("2WheelerLicense",int.Parse(dbInitData["data"][0]["License2W"]));
        PlayerPrefs.SetInt("4WheelerLicense",int.Parse(dbInitData["data"][0]["License4W"]));
        PlayerPrefs.SetInt("MoneyBank",int.Parse(dbInitData["data"][0]["MoneyBank"]));
        PlayerPrefs.SetInt("MoneyPocket",int.Parse(dbInitData["data"][0]["MoneyPocket"]));
        PlayerPrefs.SetInt("Health",int.Parse(dbInitData["data"][0]["Health"]));
        PlayerPrefs.SetFloat("MoneyPerHealth",float.Parse(dbInitData["data"][0]["MoneyPerHealth"]));
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
                (Objectlisthide[i]).SetActive(false);
            }else{
                (Objectlisthide[i]).SetActive(true);
            }
        }
        for(int j =0;j<Objectlistshow.Length;j++){
            (Objectlistshow[j]).SetActive(false);
        }
    }

    

    public void SetUsernameInClassroom(){
        // Playername = GameObject.Find("Playername").GetComponent<TextMeshProUGUI>();
        try{
            String username = PlayerPrefs.GetString("PlayerName");
            Playername.SetText((username).ToString());
            Debug.Log("Set username in Classroom");
        }catch{
            Debug.Log("Could not set username");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
