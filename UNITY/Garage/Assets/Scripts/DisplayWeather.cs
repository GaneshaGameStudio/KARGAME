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
    public Texture[] m_Texture;

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
                    GetSunRise(Weather);
                    
                    char[] delimiterChars = {'.'};
                    string[] words = TempDisplay.Split(delimiterChars);
                    TextPro.SetText(words[0] + "ö");
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
        string description = "";
        
        foreach (XmlNode time_node in xml_doc.SelectNodes("current"))
        {
            XmlNode temp_node = time_node.SelectSingleNode("temperature");
            temperature = temp_node.Attributes["value"].Value;

            XmlNode weather_node = time_node.SelectSingleNode("weather");
            description = weather_node.Attributes["value"].Value;
            switch(description){
                case "clear sky":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[0];
                    break;
                case "few clouds":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[1];
                    break;
                case "scattered clouds":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[2];
                    break;
                case "broken clouds":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[3];
                    break;
                case "shower rain":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[4];
                    break;
                case "rain":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[5];
                    break;
                case "thunderstorm":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[6];
                    break;
                case "snow":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[7];
                    break;
                case "mist":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[8];
                    break;
            }
        }
        return temperature;
    }
    private string GetSunRise(string xml)
    {
        // Load the response into an XML document.
        XmlDocument xml_doc = new XmlDocument();
        xml_doc.LoadXml(xml);
        string sunrise = "";
        string sunset = "";
        foreach (XmlNode sunrise_node in xml_doc.SelectNodes("current/city"))
        {
            XmlNode sun_node = sunrise_node.SelectSingleNode("sun");
            sunrise = sun_node.Attributes["rise"].Value;
            string[] sunrisewords = sunrise.Split('T');
            string[] sunrisetimewords = sunrisewords[1].Split(':');
            int hours = int.Parse(sunrisetimewords[0]) + 5;
            int minutes = int.Parse(sunrisetimewords[1])+ 30;
            int sunriseminuteday = hours*60 + minutes;
            
            sunset = sun_node.Attributes["set"].Value;
            string[] sunsetwords = sunset.Split('T');
            string[] sunsettimewords = sunsetwords[1].Split(':');
            hours = int.Parse(sunsettimewords[0]) + 5;
            minutes = int.Parse(sunsettimewords[1])+ 30;
            int sunsetminuteday = hours*60 + minutes;
            
            float sunrate = 180f/(sunsetminuteday - sunriseminuteday);
            GameObject.Find("Directional Light").GetComponent<SunPosition>().Xanglerate = sunrate;
            GameObject.Find("Directional Light").GetComponent<SunPosition>().Timeintercept = sunriseminuteday;
        }
        return sunrise;
    }    
    // Update is called once per frame
    void Update()
    {
        
    }
}
