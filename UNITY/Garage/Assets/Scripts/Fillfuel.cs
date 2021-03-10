using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fillfuel : MonoBehaviour
{   
    private float tofill;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider vahana)
    {   
        if(vahana.name=="Body"){
            if(GameObject.Find(VehicleID.Vehicle+"(Clone)").tag == "4Wheeler")
            {
                //GameObject.FindWithTag("4Wheeler").GetComponent<SimpleCarController>().tankcap;
                //GameObject.FindWithTag("4Wheeler").GetComponent<SimpleCarController>().remainingfuel = 1;
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
                SimpleDrive.remainingfuel = 1f;

            }
        }
        //remquant = GameObject.FindWithTag("fuel").GetComponent<Fuelreader>().Remful;
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
