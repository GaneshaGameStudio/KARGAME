﻿using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
public class Sceneexit: MonoBehaviour {  

    public void btn_change_scene(string scene_name){

        MoveCamera.mCameraIndex  = 0;
        PlayerPrefs.Save();
        SceneManager.LoadScene(scene_name);
    }   
    public void btn_exit_scene(){
        Application.Quit();
    }
}