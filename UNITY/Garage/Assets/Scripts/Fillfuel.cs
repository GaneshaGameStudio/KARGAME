using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fillfuel : MonoBehaviour
{   
    private float tofill;
    public string id;
    private Init petrolData;

    private JsonList petData  = null;
    


    // Start is called before the first frame update
    void Start()
    {
        petrolData = new Init();
        petrolData.apiParam = "bunks/"+id;
        StartCoroutine(petrolData.Download(petrolData.apiParam, result => {
            // Debug.Log(result);
            petData = result;
            // Debug.Log("Loc "+petData.data[0].RemainingFuel);
            // Debug.Log("Name "+petData.data[0].Location);
        }));
        
        
    }
    private void OnTriggerEnter(Collider vahana)
    {   
        if(vahana.name=="Body"){
            if(GameObject.Find(VehicleID.Vehicle+"(Clone)").tag == "4Wheeler")
            {
                tofill = GameObject.FindWithTag("4Wheeler").GetComponent<SimpleCarController>().tankcap*(1-fuelreader.RF); 
                print(tofill + "litres");
                // SimpleCarController.remainingfuel = 1f;

                StartCoroutine(petrolData.Download("fuelCheck/"+id+"/"+tofill, result => {
                petData = result;
                // Debug.Log("Fuel "+petData.data[0].RemainingFuel);

                if(petData != null){
                    if(petData.data[0].RemainingFuel != 0){
                        SimpleCarController.remainingfuel = 1f;
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
            else if (GameObject.Find(VehicleID.Vehicle + "(Clone)").tag == "6Wheeler")
                {
                    //GameObject.FindWithTag("6Wheeler").GetComponent<SimpleCarController>().tankcap;
                    //GameObject.FindWithTag("6Wheeler").GetComponent<SimpleCarController>().remainingfuel = 1;
                }
            else
                {
                    tofill = GameObject.FindWithTag("WheelFC").GetComponent<SimpleDrive>().tankcap*(1-fuelreader.RF); 
                    print(tofill + "litres");
                    //SimpleDrive.remainingfuel = 1f;

                    
                        StartCoroutine(petrolData.Download("fuelCheck/"+id+"/"+tofill, result => {
                        petData = result;
                        // Debug.Log("Fuel "+petData.data[0].RemainingFuel);

                        if(petData != null){
                            if(petData.data[0].RemainingFuel != 0){
                                SimpleDrive.remainingfuel = 1f;
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
            if(vahana.name==VehicleID.Vehicle + "(Clone)"){
                if (GameObject.Find(VehicleID.Vehicle + "(Clone)").tag == "Manushya")
                    {
                        tofill = GameObject.FindWithTag("Manushya").GetComponent<SimpleBodyController>().tankcap*(1-fuelreader.RF); 
                        print(tofill + "litres");
                        SimpleBodyController.remainingfuel = 1f;

                    }
            }
        
        
        //remquant = GameObject.FindWithTag("fuel").GetComponent<Fuelreader>().Remful;
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
