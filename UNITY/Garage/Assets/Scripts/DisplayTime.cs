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
                char[] delimiterCharstime = { ':'};
                using (StringReader reader = new StringReader(Time))
                {   
                    int iter =0;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {   
                        if(iter==2){
                            string[] words = Time.Split(delimiterChars);
                            string TimeDisplay = words[5].Substring(0,5);
                            string[] wordstime = TimeDisplay.Split(delimiterCharstime);
                            TextPro.SetText(wordstime[0]+" : "+wordstime[1]);
                            if(int.Parse(wordstime[0])<7 || int.Parse(wordstime[0])>18){
                                Camera.main.GetComponent<Lights>().LightOn = true;
                                print("Lights are ON");
                            }
                            else{
                                Camera.main.GetComponent<Lights>().LightOn = false;
                            }
                            int currenttime = (int.Parse(wordstime[0]))*60 + int.Parse(wordstime[1]);
                            GameObject.Find("Directional Light").GetComponent<SunPosition>().Timecurrent = currenttime;
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
