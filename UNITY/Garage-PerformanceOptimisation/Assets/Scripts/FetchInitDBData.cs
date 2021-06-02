using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using System;


public class FetchInitDBData : MonoBehaviour
{
    private Init initData;

    private string unityUserID;
    private JSONNode dbInitData;
    private string NetworkCheck = "";

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
            
            PlayerPrefs.SetString("Timestamp",DateTime.Now.ToString());
            PlayerPrefs.SetInt("2WheelerLicense",0);
            PlayerPrefs.SetInt("4WheelerLicense",0);
            PlayerPrefs.SetFloat("Money",1000);
            PlayerPrefs.SetInt("Health",50);
            PlayerPrefs.SetFloat("MoneyPerHealth",5);
            PlayerPrefs.SetFloat("TotalDistanceTraveled",0);
            //HN_Dio
            PlayerPrefs.SetString("HN_Dio_Unlocked","1");
            PlayerPrefs.SetFloat("HN_Dio_Torque",75);
            PlayerPrefs.SetFloat("HN_Dio_MaxSpeed",80);
            PlayerPrefs.SetFloat("HN_Dio_TankCapacity",12);
            PlayerPrefs.SetFloat("HN_Dio_Mileage",180);
            PlayerPrefs.SetFloat("HN_Dio_FR",1);
            PlayerPrefs.SetFloat("HN_Dio_TotalDistance",0);
            //BJ_Chetak
            PlayerPrefs.SetString("BJ_Chetak_Unlocked","0");
            PlayerPrefs.SetFloat("BJ_Chetak_Torque",150);
            PlayerPrefs.SetFloat("BJ_Chetak_MaxSpeed",70);
            PlayerPrefs.SetFloat("BJ_Chetak_TankCapacity",10);
            PlayerPrefs.SetFloat("BJ_Chetak_Mileage",17);
            PlayerPrefs.SetFloat("BJ_Chetak_FR",1);
            PlayerPrefs.SetFloat("BJ_Chetak_TotalDistance",0);

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
                string dbTimestamp = dbInitData["data"][0]["Timestamp"];
                string playerPrefsTimestamp = PlayerPrefs.GetString("Timestamp");

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
                else if (value < 0)
                {
                    Debug.Log("dbDate is before the ppDate. ");
                    //write db with pp data
                    pushDataToDB();

                    StartCoroutine(initData.Post(playerJson, result =>{
                        string res = result;
                        Debug.Log("Post status "+res);
                    }));
                }
                else
                {
                    Debug.Log("dbDate is the same as ppDate. ");
                    //use pp data
                }
            }else{
                //initializing PP for the first time with DB data
                if(NetworkCheck != null){
                    pushDataToPlayerPrefs();
                }
                
                PlayerPrefs.SetString("Timestamp",DateTime.Now.ToString());
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
        playerJson.Add("HN_Dio_TotalDistance",PlayerPrefs.GetFloat("HN_Dio_TotalDistance"));
        playerJson.Add("BJ_Chetak_TotalDistance",PlayerPrefs.GetFloat("BJ_Chetak_TotalDistance"));


        // Debug.Log(playerJson.ToString());
    }

    private void pushDataToPlayerPrefs(){
        //Read data from dbInitData and write it to PlayerPrefs
        PlayerPrefs.SetString("Timestamp",dbInitData["data"][0]["Timestamp"]);
        PlayerPrefs.SetInt("2WheelerLicense",int.Parse(dbInitData["data"][0]["License2W"]));
        PlayerPrefs.SetInt("4WheelerLicense",int.Parse(dbInitData["data"][0]["License4W"]));
        PlayerPrefs.SetFloat("Money",float.Parse(dbInitData["data"][0]["Money"]));
        PlayerPrefs.SetInt("Health",int.Parse(dbInitData["data"][0]["Health"]));
        PlayerPrefs.SetFloat("MoneyPerHealth",float.Parse(dbInitData["data"][0]["MoneyPerHealth"]));
        PlayerPrefs.SetFloat("TotalDistanceTraveled",float.Parse(dbInitData["data"][0]["TotalDistanceTraveled"]));
        //HN_Dio
        PlayerPrefs.SetString("HN_Dio_Unlocked",dbInitData["data"][0]["HN_Dio_Unlocked"]);
        PlayerPrefs.SetFloat("HN_Dio_Torque",float.Parse(dbInitData["data"][0]["HN_Dio_Torque"]));
        PlayerPrefs.SetFloat("HN_Dio_MaxSpeed",float.Parse(dbInitData["data"][0]["HN_Dio_MaxSpeed"]));
        PlayerPrefs.SetFloat("HN_Dio_TankCapacity",float.Parse(dbInitData["data"][0]["HN_Dio_TankCapacity"]));
        PlayerPrefs.SetFloat("HN_Dio_Mileage",float.Parse(dbInitData["data"][0]["HN_Dio_Mileage"]));
        PlayerPrefs.SetFloat("HN_Dio_FR",float.Parse(dbInitData["data"][0]["HN_Dio_FR"]));
        PlayerPrefs.SetFloat("HN_Dio_TotalDistance",float.Parse(dbInitData["data"][0]["HN_Dio_TotalDistance"]));
        //BJ_Chetak
        PlayerPrefs.SetString("BJ_Chetak_Unlocked",dbInitData["data"][0]["BJ_Chetak_Unlocked"]);
        PlayerPrefs.SetFloat("BJ_Chetak_Torque",float.Parse(dbInitData["data"][0]["BJ_Chetak_Torque"]));
        PlayerPrefs.SetFloat("BJ_Chetak_MaxSpeed",float.Parse(dbInitData["data"][0]["BJ_Chetak_MaxSpeed"]));
        PlayerPrefs.SetFloat("BJ_Chetak_TankCapacity",float.Parse(dbInitData["data"][0]["BJ_Chetak_TankCapacity"]));
        PlayerPrefs.SetFloat("BJ_Chetak_Mileage",float.Parse(dbInitData["data"][0]["BJ_Chetak_Mileage"]));
        PlayerPrefs.SetFloat("BJ_Chetak_FR",float.Parse(dbInitData["data"][0]["BJ_Chetak_FR"]));
        PlayerPrefs.SetFloat("BJ_Chetak_TotalDistance",float.Parse(dbInitData["data"][0]["BJ_Chetak_TotalDistance"]));
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
            (Objectlisthide[i]).SetActive(true);
        }
        for(int j =0;j<Objectlistshow.Length;j++){
            (Objectlistshow[j]).SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
