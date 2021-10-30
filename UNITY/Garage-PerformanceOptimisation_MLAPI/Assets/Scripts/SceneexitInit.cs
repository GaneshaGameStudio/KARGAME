using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
using MLAPI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class SceneexitInit: MonoBehaviour {  


    public void btn_scene_change(string scene_name){
        StartCoroutine(btn_change_scene(scene_name));
        Debug.Log("Entered change scene int");
    }

    public IEnumerator btn_change_scene(string scene_name){
        Debug.Log("Entered ienum change scene int");
        string key = "PreLoad";

        Scene scene = SceneManager.GetActiveScene();
        MoveCamera.mCameraIndex  = 0;
        PlayerPrefs.Save();
        if(scene.name=="Bangalore"){
            NetworkManager.Singleton.StopHost();
            NetworkManager.Singleton.Shutdown();
        }
        
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(key);
        yield return getDownloadSize;

        //If the download size is greater than 0, download all the dependencies.
        if (getDownloadSize.Result > 0)
        {
            Debug.Log("Download size "+getDownloadSize.Result);
            Addressables.LoadSceneAsync(scene_name);
        }else{
            Debug.Log("Download size "+getDownloadSize.Result);
            Addressables.LoadSceneAsync("Classroom");
        }
        
    }   
    public void btn_exit_scene(){
        Application.Quit();
    }
}
