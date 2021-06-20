using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public void SetInt(string KeyName)
    {   
        // set 2 wheeler license example
        PlayerPrefs.SetInt(KeyName, 1);
        PlayerPrefs.DeleteAll();
    }
    public int Getint(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }
    void Start(){
        print(PlayerPrefs.GetInt("2WheelerLicense"));
    }
}
