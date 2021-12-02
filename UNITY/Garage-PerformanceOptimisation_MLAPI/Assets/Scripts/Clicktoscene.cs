using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.EventSystems;
using MLAPI;
using UnityEngine.AddressableAssets;

public class Clicktoscene : MonoBehaviour, IPointerClickHandler
{   
    public string scene_name;
   
    public void OnPointerClick (PointerEventData eventData)
     {  
        
        PlayerPrefs.SetFloat("SpawnLoc.x",Camera.main.transform.position.x);
        PlayerPrefs.SetFloat("SpawnLoc.y",Camera.main.transform.position.y);
        PlayerPrefs.SetFloat("SpawnLoc.z",Camera.main.transform.position.z);
        PlayerPrefs.SetFloat("SpawnRot.x",Camera.main.transform.rotation.x);
        PlayerPrefs.SetFloat("SpawnRot.y",Camera.main.transform.rotation.y);
        PlayerPrefs.SetFloat("SpawnRot.z",Camera.main.transform.rotation.z);
        if(PlayerPrefs.GetString(VehicleID.Vehicle + "_Unlocked") != "0"){
            MoveCamera.mCameraIndex  = 100;
            PlayerPrefs.Save();
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name=="Bangalore"){
            NetworkManager.Singleton.StopHost();
            NetworkManager.Singleton.Shutdown();
            Addressables.LoadSceneAsync(scene_name);
            }
            else{
                VehicleID.Scene = scene_name;
            }
            
         }
        
     }
}
