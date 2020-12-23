using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System;

public class DisplayTime : MonoBehaviour
{   
    public TextMeshProUGUI TextPro;

    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine(GetTimeRequest());
    }
    
    IEnumerator GetTimeRequest()
    {   
        while (true){
            char[] delimiterChars = { ' ', ':'};
            string Time = DateTime.Now.ToString("mm/dd/yyyy HH:mm:ss");
            string[] wordstime = Time.Split(delimiterChars);
            //print(words[1]);
            //print(words[2]);
            TextPro.SetText(wordstime[1]+" : "+wordstime[2]);
            if(int.Parse(wordstime[1])<7 || int.Parse(wordstime[2])>=17){
                    Camera.main.GetComponent<Lights>().LightOn = true;
                 }
            else{
                    Camera.main.GetComponent<Lights>().LightOn = false;
                }
                int currenttime = (int.Parse(wordstime[1]))*60 + int.Parse(wordstime[2]);
                GameObject.Find("Directional Light").GetComponent<SunPosition>().Timecurrent = currenttime;
                yield return new WaitForSeconds(1f);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
