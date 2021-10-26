using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void btn_change_scene(string scene_name){
        //SceneManager.LoadScene(scene_name);
        Addressables.LoadSceneAsync("Assets/Scenes/" + scene_name + ".unity");
    }
}