using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
public class Sceneexit: MonoBehaviour {  

    public void btn_change_scene(string scene_name){

        MoveCamera.mCurrentIndex  = 100;
        SceneManager.LoadScene(scene_name);
    }   
}
