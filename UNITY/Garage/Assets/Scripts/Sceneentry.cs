using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
public class Sceneentry: MonoBehaviour {  
    
    public void btn_change_scene(string scene_name){
        scene_name = VehicleID.Vehicle;
        SceneManager.LoadScene(scene_name);
    }
}   