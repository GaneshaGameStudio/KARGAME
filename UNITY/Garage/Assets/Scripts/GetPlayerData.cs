using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerData : MonoBehaviour
{   
    public GameObject[] ListDisabledObjects;
    public string[] Listvarnames;
    // Start is called before the first frame update
    void Start()
    {   
        for(int i = 0; i < ListDisabledObjects.Length; i++){
            if(PlayerPrefs.GetInt(Listvarnames[i]) == 0){
                ListDisabledObjects[i].SetActive(true);
            }
            else if(PlayerPrefs.GetInt(Listvarnames[i]) == 1){
                ListDisabledObjects[i].SetActive(false);
            }
            
        }
    }

}
