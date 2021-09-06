using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using MLAPI;

public class Fillfuel : MonoBehaviour
{   
    private float tofill;
    public string id;
    private Init petrolData;
    private JSONNode dbPetData;
    


    // Start is called before the first frame update
    void Start()
    {
        // petrolData = new Init();
        // petrolData.apiParam = "bunks/"+id;
        // StartCoroutine(petrolData.Download(petrolData.apiParam, result => {
        //     dbPetData = result;
        // }));
        
        
    }
    private void OnTriggerEnter(Collider vahana)
    {   
        
        if(vahana.gameObject.tag=="Kit" | vahana.gameObject.tag=="Manushya"){
            Debug.Log(vahana.gameObject.tag);
            petrolData = new Init();

            if(vahana.gameObject.transform.root.tag == "4Wheeler"){
                if(vahana.gameObject.transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer){
                    // Debug.Log("Child count "+vahana.gameObject.transform.root.GetChild(2).GetChild(0).GetChild(1).gameObject.transform.childCount);
                    // TODO: fix this
                    for(int i=0; i<vahana.gameObject.transform.root.childCount; i++){
                        if(vahana.gameObject.transform.root.GetChild(i).name == PlayerPrefs.GetString(vahana.gameObject.transform.root.gameObject.name.Replace("(Clone)","").Trim()+"_KIT")){
                            

                            float tankcapacity = vahana.gameObject.transform.root.GetChild(i).gameObject.GetComponent<SimpleCarController>().tankcap;
                            tofill = vahana.gameObject.transform.root.GetChild(i).gameObject.GetComponent<SimpleCarController>().tankcap*(1-fuelreader.RF);
                            // Debug.Log(tofill + "litres");

                            
                            int fuelMoneyCheck = 0;
                            float fuelPercentageFilled = 0;
                            if(PlayerPrefs.GetInt("MoneyBank")<(Convert.ToInt32(tofill * PlayerPrefs.GetInt("PetrolPrice")))){
                                Debug.Log("Money check "+(Convert.ToInt32(tofill * PlayerPrefs.GetInt("PetrolPrice"))));
                                tofill = (float)(PlayerPrefs.GetInt("MoneyBank"))/(float)PlayerPrefs.GetInt("PetrolPrice");
                                Debug.Log("Rem fuel "+SimpleCarController.remainingfuel);
                                fuelPercentageFilled = tankcapacity/tofill;
                                // fuelPercentageFilled = (tofill*100)/tankcapacity;
                                Debug.Log(tofill + "litrea");
                                Debug.Log(PlayerPrefs.GetInt("MoneyBank"));
                                Debug.Log(PlayerPrefs.GetInt("PetrolPrice"));
                                fuelMoneyCheck = 1;
                                Debug.Log(fuelPercentageFilled);
                            }
                            // Debug.Log(tofill + "litres");

                            
                            Debug.Log(tofill+"---"+(0.3f*tankcapacity));
                            if(tofill>(0.3f*tankcapacity) | fuelMoneyCheck==1){
                                Debug.Log(tofill);
                                PlayerPrefs.SetInt("MoneyBank",((PlayerPrefs.GetInt("MoneyBank"))-(Convert.ToInt32((tofill * PlayerPrefs.GetInt("PetrolPrice"))))));
                                Debug.Log("Set PP to this value "+Convert.ToString((PlayerPrefs.GetInt("MoneyBank"))-Convert.ToInt32((tofill * PlayerPrefs.GetInt("PetrolPrice")))));
                                StartCoroutine(petrolData.Download("fuelCheck/"+id+"/"+tofill, result => {
                                dbPetData = result;
                                // Debug.Log("Fuel "+petData.data[0].RemainingFuel);

                                if(dbPetData != null){
                                    if(dbPetData["data"][0]["RemainingFuel"] != 0){
                                        
                                        if(fuelMoneyCheck==1){
                                            SimpleCarController.remainingfuel = fuelreader.RF + (1/fuelPercentageFilled);
                                            // SimpleDrive.remainingfuel = SimpleDrive.remainingfuel + SimpleDrive.remainingfuel * fuelPercentageFilled;
                                            Debug.Log("Rem fuel "+SimpleCarController.remainingfuel);
                                        }else{
                                            SimpleCarController.remainingfuel = 1f;
                                        }
                                    }else{
                                        Debug.Log("No petrol in the petrol bunk");
                                        //Add implementation to display the above message in the game
                                    }
                                }else{
                                    Debug.Log("Couldn't connect to DB");
                                    //Need to add implementation to display the above message in the game
                                }
                                
                                }));
                            }

                            
                            }
                        }
                }
                
            

            }
            else if(vahana.gameObject.transform.root.tag == "6Wheeler"){
                if(vahana.gameObject.transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer){
                    // Debug.Log("Child count "+vahana.gameObject.transform.root.GetChild(2).GetChild(0).GetChild(1).gameObject.transform.childCount);
                    // TODO: fix this
                    for(int i=0; i<vahana.gameObject.transform.root.childCount; i++){
                        if(vahana.gameObject.transform.root.GetChild(i).name == PlayerPrefs.GetString(vahana.gameObject.transform.root.gameObject.name.Replace("(Clone)","").Trim()+"_KIT")){
                            

                            float tankcapacity = vahana.gameObject.transform.root.gameObject.GetComponent<SimpleCarController>().tankcap;
                            tofill = vahana.gameObject.transform.root.gameObject.GetComponent<SimpleCarController>().tankcap*(1-fuelreader.RF);
                            // Debug.Log(tofill + "litres");

                            
                            int fuelMoneyCheck = 0;
                            float fuelPercentageFilled = 0;
                            if(PlayerPrefs.GetInt("MoneyBank")<(Convert.ToInt32(tofill * PlayerPrefs.GetInt("PetrolPrice")))){
                                Debug.Log("Money check "+(Convert.ToInt32(tofill * PlayerPrefs.GetInt("PetrolPrice"))));
                                tofill = (float)(PlayerPrefs.GetInt("MoneyBank"))/(float)PlayerPrefs.GetInt("PetrolPrice");
                                Debug.Log("Rem fuel "+SimpleCarController.remainingfuel);
                                fuelPercentageFilled = tankcapacity/tofill;
                                // fuelPercentageFilled = (tofill*100)/tankcapacity;
                                Debug.Log(tofill + "litrea");
                                Debug.Log(PlayerPrefs.GetInt("MoneyBank"));
                                Debug.Log(PlayerPrefs.GetInt("PetrolPrice"));
                                fuelMoneyCheck = 1;
                                Debug.Log(fuelPercentageFilled);
                            }
                            // Debug.Log(tofill + "litres");

                            
                            Debug.Log(tofill+"---"+(0.3f*tankcapacity));
                            if(tofill>(0.3f*tankcapacity) | fuelMoneyCheck==1){
                                Debug.Log(tofill);
                                PlayerPrefs.SetInt("MoneyBank",((PlayerPrefs.GetInt("MoneyBank"))-(Convert.ToInt32((tofill * PlayerPrefs.GetInt("PetrolPrice"))))));
                                Debug.Log("Set PP to this value "+Convert.ToString((PlayerPrefs.GetInt("MoneyBank"))-Convert.ToInt32((tofill * PlayerPrefs.GetInt("PetrolPrice")))));
                                StartCoroutine(petrolData.Download("fuelCheck/"+id+"/"+tofill, result => {
                                dbPetData = result;
                                // Debug.Log("Fuel "+petData.data[0].RemainingFuel);

                                if(dbPetData != null){
                                    if(dbPetData["data"][0]["RemainingFuel"] != 0){
                                        
                                        if(fuelMoneyCheck==1){
                                            SimpleCarController.remainingfuel = fuelreader.RF + (1/fuelPercentageFilled);
                                            // SimpleDrive.remainingfuel = SimpleDrive.remainingfuel + SimpleDrive.remainingfuel * fuelPercentageFilled;
                                            Debug.Log("Rem fuel "+SimpleCarController.remainingfuel);
                                        }else{
                                            SimpleCarController.remainingfuel = 1f;
                                        }
                                    }else{
                                        Debug.Log("No petrol in the petrol bunk");
                                        //Add implementation to display the above message in the game
                                    }
                                }else{
                                    Debug.Log("Couldn't connect to DB");
                                    //Need to add implementation to display the above message in the game
                                }
                                
                                }));
                            }

                            
                            }
                        }
                }
                
            

            
                
            }
            else if(vahana.gameObject.transform.root.tag == "2Wheeler"){
                if(vahana.gameObject.transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer){
                    // Debug.Log("Child count "+vahana.gameObject.transform.root.GetChild(2).GetChild(0).GetChild(1).gameObject.transform.childCount);
                    // TODO: fix this
                    for(int i=0; i<vahana.gameObject.transform.root.childCount; i++){
                        if(vahana.gameObject.transform.root.GetChild(i).name == PlayerPrefs.GetString(vahana.gameObject.transform.root.gameObject.name.Replace("(Clone)","").Trim()+"_KIT")){
                            

                            float tankcapacity = vahana.gameObject.transform.root.GetChild(i).GetChild(0).GetChild(1).gameObject.GetComponent<SimpleDrive>().tankcap;
                            tofill = vahana.gameObject.transform.root.GetChild(i).GetChild(0).GetChild(1).gameObject.GetComponent<SimpleDrive>().tankcap*(1-fuelreader.RF);
                            // Debug.Log(tofill + "litres");

                            
                            int fuelMoneyCheck = 0;
                            float fuelPercentageFilled = 0;
                            if(PlayerPrefs.GetInt("MoneyBank")<(Convert.ToInt32(tofill * PlayerPrefs.GetInt("PetrolPrice")))){
                                Debug.Log("Money check "+(Convert.ToInt32(tofill * PlayerPrefs.GetInt("PetrolPrice"))));
                                tofill = (float)(PlayerPrefs.GetInt("MoneyBank"))/(float)PlayerPrefs.GetInt("PetrolPrice");
                                Debug.Log("Rem fuel "+SimpleDrive.remainingfuel);
                                fuelPercentageFilled = tankcapacity/tofill;
                                // fuelPercentageFilled = (tofill*100)/tankcapacity;
                                Debug.Log(tofill + "litrea");
                                Debug.Log(PlayerPrefs.GetInt("MoneyBank"));
                                Debug.Log(PlayerPrefs.GetInt("PetrolPrice"));
                                fuelMoneyCheck = 1;
                                Debug.Log(fuelPercentageFilled);
                            }
                            // Debug.Log(tofill + "litres");

                            
                            Debug.Log(tofill+"---"+(0.3f*tankcapacity));
                            if(tofill>(0.3f*tankcapacity) | fuelMoneyCheck==1){
                                Debug.Log(tofill);
                                PlayerPrefs.SetInt("MoneyBank",((PlayerPrefs.GetInt("MoneyBank"))-(Convert.ToInt32((tofill * PlayerPrefs.GetInt("PetrolPrice"))))));
                                Debug.Log("Set PP to this value "+Convert.ToString((PlayerPrefs.GetInt("MoneyBank"))-Convert.ToInt32((tofill * PlayerPrefs.GetInt("PetrolPrice")))));
                                StartCoroutine(petrolData.Download("fuelCheck/"+id+"/"+tofill, result => {
                                dbPetData = result;
                                // Debug.Log("Fuel "+petData.data[0].RemainingFuel);

                                if(dbPetData != null){
                                    if(dbPetData["data"][0]["RemainingFuel"] != 0){
                                        
                                        if(fuelMoneyCheck==1){
                                            SimpleDrive.remainingfuel = fuelreader.RF + (1/fuelPercentageFilled);
                                            // SimpleDrive.remainingfuel = SimpleDrive.remainingfuel + SimpleDrive.remainingfuel * fuelPercentageFilled;
                                            Debug.Log("Rem fuel "+SimpleDrive.remainingfuel);
                                        }else{
                                            SimpleDrive.remainingfuel = 1f;
                                        }
                                    }else{
                                        Debug.Log("No petrol in the petrol bunk");
                                        //Add implementation to display the above message in the game
                                    }
                                }else{
                                    Debug.Log("Couldn't connect to DB");
                                    //Need to add implementation to display the above message in the game
                                }
                                
                                }));
                            }

                            
                            }
                        }
                }
                
            }
            else if(vahana.gameObject.transform.root.tag == "3Wheeler"){
                if(vahana.gameObject.transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer){
                    // Debug.Log("Child count "+vahana.gameObject.transform.root.GetChild(2).GetChild(0).GetChild(1).gameObject.transform.childCount);
                    // TODO: fix this
                    for(int i=0; i<vahana.gameObject.transform.root.childCount; i++){
                        if(vahana.gameObject.transform.root.GetChild(i).name == PlayerPrefs.GetString(vahana.gameObject.transform.root.gameObject.name.Replace("(Clone)","").Trim()+"_KIT")){
                            

                            float tankcapacity = vahana.gameObject.transform.root.GetChild(i).GetChild(0).GetChild(1).gameObject.GetComponent<SimpleDrive>().tankcap;
                            tofill = vahana.gameObject.transform.root.GetChild(i).GetChild(0).GetChild(1).gameObject.GetComponent<SimpleDrive>().tankcap*(1-fuelreader.RF);
                            // Debug.Log(tofill + "litres");

                            
                            int fuelMoneyCheck = 0;
                            float fuelPercentageFilled = 0;
                            if(PlayerPrefs.GetInt("MoneyBank")<(Convert.ToInt32(tofill * PlayerPrefs.GetInt("PetrolPrice")))){
                                Debug.Log("Money check "+(Convert.ToInt32(tofill * PlayerPrefs.GetInt("PetrolPrice"))));
                                tofill = (float)(PlayerPrefs.GetInt("MoneyBank"))/(float)PlayerPrefs.GetInt("PetrolPrice");
                                Debug.Log("Rem fuel "+SimpleDrive.remainingfuel);
                                fuelPercentageFilled = tankcapacity/tofill;
                                // fuelPercentageFilled = (tofill*100)/tankcapacity;
                                Debug.Log(tofill + "litrea");
                                Debug.Log(PlayerPrefs.GetInt("MoneyBank"));
                                Debug.Log(PlayerPrefs.GetInt("PetrolPrice"));
                                fuelMoneyCheck = 1;
                                Debug.Log(fuelPercentageFilled);
                            }
                            // Debug.Log(tofill + "litres");

                            
                            Debug.Log(tofill+"---"+(0.3f*tankcapacity));
                            if(tofill>(0.3f*tankcapacity) | fuelMoneyCheck==1){
                                Debug.Log(tofill);
                                PlayerPrefs.SetInt("MoneyBank",((PlayerPrefs.GetInt("MoneyBank"))-(Convert.ToInt32((tofill * PlayerPrefs.GetInt("PetrolPrice"))))));
                                Debug.Log("Set PP to this value "+Convert.ToString((PlayerPrefs.GetInt("MoneyBank"))-Convert.ToInt32((tofill * PlayerPrefs.GetInt("PetrolPrice")))));
                                StartCoroutine(petrolData.Download("fuelCheck/"+id+"/"+tofill, result => {
                                dbPetData = result;
                                // Debug.Log("Fuel "+petData.data[0].RemainingFuel);

                                if(dbPetData != null){
                                    if(dbPetData["data"][0]["RemainingFuel"] != 0){
                                        
                                        if(fuelMoneyCheck==1){
                                            SimpleDrive.remainingfuel = fuelreader.RF + (1/fuelPercentageFilled);
                                            // SimpleDrive.remainingfuel = SimpleDrive.remainingfuel + SimpleDrive.remainingfuel * fuelPercentageFilled;
                                            Debug.Log("Rem fuel "+SimpleDrive.remainingfuel);
                                        }else{
                                            SimpleDrive.remainingfuel = 1f;
                                        }
                                    }else{
                                        Debug.Log("No petrol in the petrol bunk");
                                        //Add implementation to display the above message in the game
                                    }
                                }else{
                                    Debug.Log("Couldn't connect to DB");
                                    //Need to add implementation to display the above message in the game
                                }
                                
                                }));
                            }

                            
                            }
                        }
                }
                
            
                
            }
            else if(vahana.gameObject.transform.root.tag == "Manushya"){
                
            }
        }

        // Debug.Log(vahana.gameObject.transform.root.gameObject.name.Replace("(Clone)","").Trim());
            





            // if(vahana.name=="Body"){
            //     if(GameObject.Find(VehicleID.Vehicle+"(Clone)").tag == "4Wheeler")
            //     {
            //         tofill = GameObject.FindWithTag("4Wheeler").GetComponent<SimpleCarController>().tankcap*(1-fuelreader.RF); 
            //         print(tofill + "litres");

            //         StartCoroutine(petrolData.Download("fuelCheck/"+id+"/"+tofill, result => {
            //         dbPetData = result;
            //         // Debug.Log("Fuel "+petData.data[0].RemainingFuel);

            //         if(dbPetData != null){
            //             if(dbPetData["data"][0]["RemainingFuel"] != 0){
            //                 SimpleCarController.remainingfuel = 1f;
            //             }else{
            //                 Debug.Log("No petrol in the petrol bunk");
            //                 //Add implementation to display the above message in the game
            //             }
            //         }else{
            //             Debug.Log("Couldn't connect to DB");
            //             //Need to add implementation to display the above message in the game
            //         }
                    
            //         }));
                    

            //     }
            //     else if (GameObject.Find(VehicleID.Vehicle + "(Clone)").tag == "6Wheeler")
            //         {
            //             //GameObject.FindWithTag("6Wheeler").GetComponent<SimpleCarController>().tankcap;
            //             //GameObject.FindWithTag("6Wheeler").GetComponent<SimpleCarController>().remainingfuel = 1;
            //         }
            //     else
            //         {
            //             tofill = GameObject.FindWithTag("WheelFC").GetComponent<SimpleDrive>().tankcap*(1-fuelreader.RF); 
            //             print(tofill + "litres");
            //             //SimpleDrive.remainingfuel = 1f;

                        
            //                 StartCoroutine(petrolData.Download("fuelCheck/"+id+"/"+tofill, result => {
            //                 dbPetData = result;
            //                 // Debug.Log("Fuel "+petData.data[0].RemainingFuel);

            //                 if(dbPetData != null){
            //                     if(dbPetData["data"][0]["RemainingFuel"] != 0){
            //                         SimpleDrive.remainingfuel = 1f;
            //                     }else{
            //                         Debug.Log("No petrol in the petrol bunk");
            //                         //Add implementation to display the above message in the game
            //                     }
            //                 }else{
            //                     Debug.Log("Couldn't connect to DB");
            //                     //Need to add implementation to display the above message in the game
            //                 }
                            
            //                 }));
                        

            //         }
            //     }
            //     if(vahana.name==VehicleID.Vehicle + "(Clone)"){
            //         if (GameObject.Find(VehicleID.Vehicle + "(Clone)").tag == "Manushya")
            //             {
            //                 tofill = GameObject.FindWithTag("Manushya").GetComponent<SimpleBodyController>().tankcap*(1-fuelreader.RF); 
            //                 print(tofill + "litres");
            //                 SimpleBodyController.remainingfuel = 1f;

            //             }
            //     }
            
            
            //remquant = GameObject.FindWithTag("fuel").GetComponent<Fuelreader>().Remful;



        
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
