using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
using MLAPI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
public class Sceneexit: MonoBehaviour {  

    public void btn_change_scene(string scene_name){
        Scene scene = SceneManager.GetActiveScene();
        MoveCamera.mCameraIndex  = 0;
        PlayerPrefs.Save();
        if(scene.name=="Bangalore"){
            NetworkManager.Singleton.StopHost();
            NetworkManager.Singleton.Shutdown();
        }
        
         Addressables.LoadSceneAsync(scene_name);
    }   
    public void btn_exit_scene(){
        Application.Quit();
    }
}
