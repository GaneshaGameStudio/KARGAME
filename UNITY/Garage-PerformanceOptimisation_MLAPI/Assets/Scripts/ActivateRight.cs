using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRight : MonoBehaviour
{   

     private GameObject btnactright;
     public static bool btactright;
     private bool toggle;
    // Start is called before the first frame update
    void OnEnable(){
        btnactright = GameObject.Find("Activate-Right");
        btactright=false;
    }
    public void btn_act_right(){
         
        if(btactright==false){
            btnactright.transform.localPosition = new Vector3(btnactright.transform.localPosition.x, btnactright.transform.localPosition.y-60f, btnactright.transform.localPosition.z);
            btactright=true;
        }
        else if(btactright==true){
            btnactright.transform.localPosition = new Vector3(btnactright.transform.localPosition.x, btnactright.transform.localPosition.y+60f, btnactright.transform.localPosition.z);
            btactright=false;
        
        }
    }
    public void ToggleSound()
     {
         toggle = !toggle;
 
         if (toggle)
             AudioListener.volume = 1f;
 
         else
             AudioListener.volume = 0f;
     }
}
