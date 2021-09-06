using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using TMPro;

public class Amount : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        if(IsLocalPlayer){
            int MoneyP = PlayerPrefs.GetInt("MoneyPocket");
            int MoneyB = PlayerPrefs.GetInt("MoneyBank");
            MoneyP = 2000-MoneyP;
            MoneyB = MoneyB - MoneyP;
            PlayerPrefs.SetInt("MoneyPocket", 2000);
            PlayerPrefs.SetInt("MoneyBank", MoneyB);
            GameObject.Find("Money-number").GetComponent<TextMeshProUGUI>().SetText((PlayerPrefs.GetInt("MoneyPocket")).ToString());
 
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
