using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRight : MonoBehaviour
{   

     private GameObject btnactright;
     public static bool btactright;
    // Start is called before the first frame update
    public void btn_act_right(){
         btnactright = GameObject.Find("Activate-Right");
        if(btactright==false){
            btnactright.transform.localPosition = new Vector3(btnactright.transform.localPosition.x, btnactright.transform.localPosition.y-60f, btnactright.transform.localPosition.z);
            btactright=true;
        }
        else if(btactright==true){
            btnactright.transform.localPosition = new Vector3(btnactright.transform.localPosition.x, btnactright.transform.localPosition.y+60f, btnactright.transform.localPosition.z);
            btactright=false;
        
        }
    }
}
