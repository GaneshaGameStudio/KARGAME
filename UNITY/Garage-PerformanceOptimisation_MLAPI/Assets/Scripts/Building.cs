using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;
public class Building : MonoBehaviour
{   
    private string Vehicletemp;
    private Vector3 buildingcoord;
    private Vector3 buildingrot;
    private float deg2rad = 0.01745311f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider building)
    {   
        
        if(building.transform.parent){
             if(building.transform.parent.tag=="2Wheeler"){
            
            Vehicletemp = VehicleID.Vehicle;
            PlayerPrefs.SetString("Vehicletemp", Vehicletemp);
            VehicleID.Vehicle = "Vibe2009rig-redoCSY";
            buildingcoord = building.gameObject.transform.parent.position;
            buildingrot = building.gameObject.transform.parent.localRotation.eulerAngles;
            PlayerPrefs.SetFloat("SpawnLoc.x",buildingcoord.x + 1.0f*Mathf.Sin(deg2rad*buildingrot.y));
            PlayerPrefs.SetFloat("SpawnLoc.y",buildingcoord.y + 0.2f);
            PlayerPrefs.SetFloat("SpawnLoc.z",buildingcoord.z + 1.0f*Mathf.Cos(deg2rad*buildingrot.y));

            PlayerPrefs.SetFloat("SpawnRot.x",0f);
            PlayerPrefs.SetFloat("SpawnRot.y",buildingrot.y);
            PlayerPrefs.SetFloat("SpawnRot.z",0f);
            PlayerPrefs.Save();
            NetworkManager.Singleton.StopHost();
            NetworkManager.Singleton.Shutdown();
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            
            
            }
        }
       
        
    }
    private void OnTriggerExit(Collider building)
    {  
        if(building.transform.parent){
            if(building.transform.parent.tag=="Manushya"){
            
            buildingcoord = building.transform.parent.position;
            buildingrot = building.gameObject.transform.parent.localRotation.eulerAngles;
            PlayerPrefs.SetFloat("SpawnLoc.x",buildingcoord.x + 1.0f*Mathf.Sin(deg2rad*buildingrot.y));
            PlayerPrefs.SetFloat("SpawnLoc.y",buildingcoord.y);
            PlayerPrefs.SetFloat("SpawnLoc.z",buildingcoord.z + 1.0f*Mathf.Cos(deg2rad*buildingrot.y));

            PlayerPrefs.SetFloat("SpawnRot.x",0f);
            PlayerPrefs.SetFloat("SpawnRot.y",buildingrot.y);
            PlayerPrefs.SetFloat("SpawnRot.z",0f);
            VehicleID.Vehicle = PlayerPrefs.GetString("Vehicletemp");
            PlayerPrefs.Save();
            NetworkManager.Singleton.StopHost();
            NetworkManager.Singleton.Shutdown();
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            
            
            }
        }
        

    }

}
