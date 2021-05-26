using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerData : MonoBehaviour
{   
    public string varname;
    public string mainname;
    public string DispMessage;
    public static string DisplayMessage;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        if(other.tag==mainname){
            PlayerPrefs.SetInt(varname, 1);
            DisplayMessage = DispMessage;
        }
    }
}
