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
    public Material Skybox;
    private string passdescription;
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {   
        //ps = GetComponent<ParticleSystem>();
        GameObject.Find("Rain").GetComponent<ParticleSystem>().Stop();
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
        var emission = ps.emission;
        foreach (XmlNode time_node in xml_doc.SelectNodes("current"))
        {
            XmlNode temp_node = time_node.SelectSingleNode("temperature");
            temperature = temp_node.Attributes["value"].Value;

            XmlNode weather_node = time_node.SelectSingleNode("weather");
            description = weather_node.Attributes["value"].Value;
            passdescription = description;
            
            RenderSettings.fogEndDistance = 100f;
            
            switch(description){
                case "clear sky":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[0];
                    Skybox.SetColor("Color_BE31CDF2",new Color(0.2549019f,0.5294118f,1f,1f));
                    Skybox.SetColor("Color_68FD0CD8",new Color(0.01321642f,0.7215686f,0.8661178f,1f));
                    Skybox.SetFloat("Vector1_B93A03DE",0.01f);
                    Skybox.SetFloat("Vector1_FE797C36",0.03f);
                    Skybox.SetFloat("Vector1_FA4E5253",0.03f);
                    Skybox.SetFloat("Vector1_249357C1",0.08f);
                    Skybox.SetFloat("Vector1_9E12CF26",0.14f);
                    Skybox.SetColor("Color_B54BEABE",new Color(0.764151f,0.6776432f,0.6776432f,1f));
                    Skybox.SetColor("Color_424F81E0",new Color(0.9056604f,0.883785f,0.6237095f,1f));
                    break;
                case "few clouds":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[1];
                    Skybox.SetColor("Color_BE31CDF2",new Color(0.2549019f,0.5294118f,1f,1f));
                    Skybox.SetColor("Color_68FD0CD8",new Color(0.01321642f,0.7215686f,0.8661178f,1f));
                    Skybox.SetFloat("Vector1_B93A03DE",0.01f);
                    Skybox.SetFloat("Vector1_FE797C36",0.03f);
                    Skybox.SetFloat("Vector1_FA4E5253",0.03f);
                    Skybox.SetFloat("Vector1_249357C1",0.153f);
                    Skybox.SetFloat("Vector1_9E12CF26",0.194f);
                    Skybox.SetColor("Color_B54BEABE",new Color(0.764151f,0.6776432f,0.6776432f,1f));
                    Skybox.SetColor("Color_424F81E0",new Color(0.9056604f,0.883785f,0.6237095f,1f));
                    break;
                case "scattered clouds":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[2];
                    Skybox.SetColor("Color_BE31CDF2",new Color(0.2549019f,0.5294118f,1f,1f));
                    Skybox.SetColor("Color_68FD0CD8",new Color(0.01321642f,0.7215686f,0.8661178f,1f));
                    Skybox.SetFloat("Vector1_B93A03DE",0.01f);
                    Skybox.SetFloat("Vector1_FE797C36",0.03f);
                    Skybox.SetFloat("Vector1_FA4E5253",0.09f);
                    Skybox.SetFloat("Vector1_249357C1",0.153f);
                    Skybox.SetFloat("Vector1_9E12CF26",0.194f);
                    Skybox.SetColor("Color_B54BEABE",new Color(0.764151f,0.6776432f,0.6776432f,1f));
                    Skybox.SetColor("Color_424F81E0",new Color(0.9056604f,0.883785f,0.6237095f,1f));
                    break;
                case "broken clouds":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[3];
                    Skybox.SetColor("Color_BE31CDF2",new Color(0.2549019f,0.5294118f,1f,1f));
                    Skybox.SetColor("Color_68FD0CD8",new Color(0.01321642f,0.7215686f,0.8661178f,1f));
                    Skybox.SetFloat("Vector1_B93A03DE",0.01f);
                    Skybox.SetFloat("Vector1_FE797C36",0.03f);
                    Skybox.SetFloat("Vector1_FA4E5253",0.09f);
                    Skybox.SetFloat("Vector1_249357C1",0.153f);
                    Skybox.SetFloat("Vector1_9E12CF26",0.042f);
                    Skybox.SetColor("Color_B54BEABE",new Color(0.764151f,0.6776432f,0.6776432f,1f));
                    Skybox.SetColor("Color_424F81E0",new Color(0.9056604f,0.883785f,0.6237095f,1f));
                    break;
                case "shower rain":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[4];
                    emission.rateOverTime = 4000;
                    GameObject.Find("Rain").GetComponent<ParticleSystem>().Play();
                    Skybox.SetColor("Color_BE31CDF2",new Color(0f,0.3481131f,0.3679245f,1f));
                    Skybox.SetColor("Color_68FD0CD8",new Color(0.3978284f,0.5377547f,0.5660378f,1f));
                    Skybox.SetFloat("Vector1_B93A03DE",0.001f);
                    Skybox.SetFloat("Vector1_FE797C36",0.03f);
                    Skybox.SetFloat("Vector1_FA4E5253",0.03f);
                    Skybox.SetFloat("Vector1_249357C1",0.302f);
                    Skybox.SetFloat("Vector1_9E12CF26",0.281f);
                    Skybox.SetColor("Color_B54BEABE",new Color(0.4622641f,0.4622641f,0.4622641f,1f));
                    Skybox.SetColor("Color_424F81E0",new Color(0.7735849f,0.7735849f,0.7735849f,1f));
                    break;
                case "moderate rain":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[4];
                    emission.rateOverTime = 4000;
                    GameObject.Find("Rain").GetComponent<ParticleSystem>().Play();
                    Skybox.SetColor("Color_BE31CDF2",new Color(0f,0.3481131f,0.3679245f,1f));
                    Skybox.SetColor("Color_68FD0CD8",new Color(0.3978284f,0.5377547f,0.5660378f,1f));
                    Skybox.SetFloat("Vector1_B93A03DE",0.001f);
                    Skybox.SetFloat("Vector1_FE797C36",0.03f);
                    Skybox.SetFloat("Vector1_FA4E5253",0.03f);
                    Skybox.SetFloat("Vector1_249357C1",0.302f);
                    Skybox.SetFloat("Vector1_9E12CF26",0.281f);
                    Skybox.SetColor("Color_B54BEABE",new Color(0.4622641f,0.4622641f,0.4622641f,1f));
                    Skybox.SetColor("Color_424F81E0",new Color(0.7735849f,0.7735849f,0.7735849f,1f));
                    break;
                case "rain":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[5];
                    emission.rateOverTime = 4000;
                    GameObject.Find("Rain").GetComponent<ParticleSystem>().Play();
                    Skybox.SetColor("Color_BE31CDF2",new Color(0.1647828f,0.2140812f,0.2169811f,1f));
                    Skybox.SetColor("Color_68FD0CD8",new Color(0.4716981f,0.4716981f,0.4716981f,1f));
                    Skybox.SetFloat("Vector1_B93A03DE",0.0005f);
                    Skybox.SetFloat("Vector1_FE797C36",0.03f);
                    Skybox.SetFloat("Vector1_FA4E5253",0.03f);
                    Skybox.SetFloat("Vector1_249357C1",0.249f);
                    Skybox.SetFloat("Vector1_9E12CF26",0.281f);
                    Skybox.SetColor("Color_B54BEABE",new Color(0.2641509f,0.2641509f,0.2641509f,1f));
                    Skybox.SetColor("Color_424F81E0",new Color(0.3584906f,0.3584906f,0.3584906f,1f));
                    break;
                case "heavy intensity rain":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[5];
                    emission.rateOverTime = 9000;
                    GameObject.Find("Rain").GetComponent<ParticleSystem>().Play();
                    Skybox.SetColor("Color_BE31CDF2",new Color(0.1647828f,0.2140812f,0.2169811f,1f));
                    Skybox.SetColor("Color_68FD0CD8",new Color(0.4716981f,0.4716981f,0.4716981f,1f));
                    Skybox.SetFloat("Vector1_B93A03DE",0.0005f);
                    Skybox.SetFloat("Vector1_FE797C36",0.03f);
                    Skybox.SetFloat("Vector1_FA4E5253",0.03f);
                    Skybox.SetFloat("Vector1_249357C1",0.249f);
                    Skybox.SetFloat("Vector1_9E12CF26",0.281f);
                    Skybox.SetColor("Color_B54BEABE",new Color(0.2641509f,0.2641509f,0.2641509f,1f));
                    Skybox.SetColor("Color_424F81E0",new Color(0.3584906f,0.3584906f,0.3584906f,1f));
                    break;
                case "thunderstorm":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[6];
                    emission.rateOverTime = 4000;
                    GameObject.Find("Rain").GetComponent<ParticleSystem>().Play();
                    Skybox.SetColor("Color_BE31CDF2",new Color(0.1647828f,0.2140812f,0.2169811f,1f));
                    Skybox.SetColor("Color_68FD0CD8",new Color(0.4716981f,0.4716981f,0.4716981f,1f));
                    Skybox.SetFloat("Vector1_B93A03DE",0.0f);
                    Skybox.SetFloat("Vector1_FE797C36",0.03f);
                    Skybox.SetFloat("Vector1_FA4E5253",0.03f);
                    Skybox.SetFloat("Vector1_249357C1",0.249f);
                    Skybox.SetFloat("Vector1_9E12CF26",0.281f);
                    Skybox.SetColor("Color_B54BEABE",new Color(0.2641509f,0.2641509f,0.2641509f,1f));
                    Skybox.SetColor("Color_424F81E0",new Color(0.3584906f,0.3584906f,0.3584906f,1f));
                    StartCoroutine(Minakaminaka(3f));
                    break;
                case "snow":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[7];
                    break;
                case "mist":
                    GameObject.Find("Garage-Canvas/Forecast").GetComponent<RawImage>().texture = m_Texture[8];
                    Skybox.SetColor("Color_BE31CDF2",new Color(0.2549019f,0.5294118f,1f,1f));
                    Skybox.SetColor("Color_68FD0CD8",new Color(0.01321642f,0.7215686f,0.8661178f,1f));
                    Skybox.SetFloat("Vector1_B93A03DE",0.01f);
                    Skybox.SetFloat("Vector1_FE797C36",0.03f);
                    Skybox.SetFloat("Vector1_FA4E5253",0.03f);
                    Skybox.SetFloat("Vector1_249357C1",0.08f);
                    Skybox.SetColor("Color_B54BEABE",new Color(0.764151f,0.6776432f,0.6776432f,1f));
                    Skybox.SetColor("Color_424F81E0",new Color(0.9056604f,0.883785f,0.6237095f,1f));
                    RenderSettings.fogEndDistance = 20f;
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
    IEnumerator Minakaminaka(float delay){
        if(passdescription == "thunderstorm"){
            
            while(true){
                int times = Random.Range(2,6);
                for(int m=0;m<=times;m++){
                    GameObject.Find("Directional Light").GetComponent<Light>().intensity = 10f;
                    yield return new WaitForSeconds(.05f);
                    GameObject.Find("Directional Light").GetComponent<Light>().intensity = 2.54f;
                    yield return new WaitForSeconds(.05f);
                }
            yield return new WaitForSeconds(delay + Random.value*delay);
            }
        }
    }

    void Update()
    {   
        
    }
}
