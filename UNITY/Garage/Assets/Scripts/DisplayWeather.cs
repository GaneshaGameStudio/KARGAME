using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class DisplayWeather : MonoBehaviour
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
        StartCoroutine(GetWeatherRequest(CurrentUrl));
    }
    IEnumerator GetWeatherRequest(string uri)
    {   
        while(true){
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
                {
                    // Request and wait for the desired page.
                    yield return webRequest.SendWebRequest();

                    string[] pages = uri.Split('/');
                    int page = pages.Length - 1;
                    
                    string Weather = webRequest.downloadHandler.text;
                    string TempDisplay = DispWeather(Weather);
                    TextPro.SetText(TempDisplay + "ö");
                    yield return new WaitForSeconds(1800f);
            
                }
            }
    }
    
    private string DispWeather(string xml)
    {
        // Load the response into an XML document.
        XmlDocument xml_doc = new XmlDocument();
        xml_doc.LoadXml(xml);
        string temperature = "";
        foreach (XmlNode time_node in xml_doc.SelectNodes("current"))
        {
            XmlNode temp_node = time_node.SelectSingleNode("temperature");
            temperature = temp_node.Attributes["value"].Value;
            
        }
        return temperature;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
