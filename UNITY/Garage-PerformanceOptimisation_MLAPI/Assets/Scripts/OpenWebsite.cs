using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class OpenWebsite : MonoBehaviour
{
    public void btn_open_site (string websitename)
     {  
      //   Debug.Log(websitename);
        Application.OpenURL("http://www."+websitename);
     }
}
