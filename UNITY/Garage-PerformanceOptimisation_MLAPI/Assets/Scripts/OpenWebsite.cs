using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class OpenWebsite : MonoBehaviour
{   
    private bool show;
    public GameObject showobj;
    void Start(){
      //showobj = GameObject.Find("Helper");
      show=true;
    }
    public void btn_open_site (string websitename)
     {  
      //   Debug.Log(websitename);
        Application.OpenURL("http://www."+websitename);
     }
     public void btn_open_helper(){
       if(show==true){
         showobj.SetActive(true);
         show=false;
        
       }
       else if(show==false){
         showobj.SetActive(false);
         show=true;
         
       }
       
     }
}
