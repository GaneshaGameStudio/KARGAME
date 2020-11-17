using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
public class SceneChange: MonoBehaviour {  
    
    public void btn_change_scene(string scene_name){
        MoveCamera.mCurrentIndex  = 100;
        VehicleID.Scene = scene_name;
    }
}   