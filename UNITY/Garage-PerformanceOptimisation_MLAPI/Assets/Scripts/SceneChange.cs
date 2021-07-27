using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
public class SceneChange: MonoBehaviour {  
    public void btn_change_scene(string scene_name){
        
        MoveCamera.mCameraIndex  = 100;
        MoveCameraModShop.mCameraIndex  = 100;
        Chat.isCrash = false;
        PlayerPrefs.Save();
        if(GameObject.Find(VehicleID.VehicleTag+"Gate")){
            
            VehicleID.Scene = "VehicleLicense";

        }
        else if(PlayerPrefs.GetString(VehicleID.Vehicle + "_Unlocked") == "0"){
            MoveCamera.mCameraIndex  = 10;
            MoveCameraModShop.mCameraIndex  = 10;
        }
        else{
            VehicleID.Scene = scene_name;
        }
        
        
    }
    
}   