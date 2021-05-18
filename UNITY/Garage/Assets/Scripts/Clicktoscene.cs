using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.EventSystems;

public class Clicktoscene : MonoBehaviour, IPointerClickHandler
{   
    public string scene_name;
   
    public void OnPointerClick (PointerEventData eventData)
     {  
        MoveCamera.mCameraIndex  = 100;
        PlayerPrefs.Save();
        VehicleID.Scene = scene_name;
     }
}
