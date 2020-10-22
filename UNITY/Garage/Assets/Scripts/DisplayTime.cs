using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class DisplayTime : MonoBehaviour
{   
    private const string API_KEY = "42d4191ee9409cb50f90142a4630a078";
    private const string CurrentUrl =
        "http://api.openweathermap.org/data/2.5/weather?" +
        "q=Bengaluru,IN@&mode=xml&units=metric&APPID=" + API_KEY;
    private const string ForecastUrl =
        "http://api.openweathermap.org/data/2.5/forecast?" +
        "q=Bengaluru,IN&mode=xml&units=metric&APPID=" + API_KEY;
    public TextMeshProUGUI TextPro;

    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine(GetTimeRequest("http://worldtimeapi.org/api/timezone/Asia/Kolkata.txt"));
    }
    
    IEnumerator GetTimeRequest(string uri)
    {   
        while (true){
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {   
                
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                
                string Time = webRequest.downloadHandler.text;
                char[] delimiterChars = { 'T', '.'};
                using (StringReader reader = new StringReader(Time))
                {   
                    int iter =0;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {   
                        if(iter==2){
                            string[] words = Time.Split(delimiterChars);
                            string TimeDisplay = words[5].Substring(0,5);
                            TextPro.SetText(TimeDisplay);
                            break;
                        }
                        iter++;
                    }
                }
            yield return new WaitForSeconds(1f);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
