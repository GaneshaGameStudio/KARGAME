using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class JsonList
{
    public List<Init> data;

    public static JsonList Parse(string json)
    {
        return JsonUtility.FromJson<JsonList>(json);
    }
}



[System.Serializable]
public class Init
{
    private string tofill;
    public string id;
    public float TotalFuel;
    public float RemainingFuel;
    public string Name;
    public string Location;
    public string Coordinates;

    public string apiParam;
    



    // static string data = null;
    private string apiUrl = "http://127.0.0.1:8000/";  //For now, hardcode the url with portnumber. Once kargame site is setup, fetch this url from there and update automatically

    


    public string Stringify() {
        return JsonUtility.ToJson(this);
    }

    public static Init Parse(string json)
    {
        return JsonUtility.FromJson<Init>(json);
    }


    public IEnumerator Download(string id, System.Action<JsonList> callback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl + id))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                // Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(null);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(JsonList.Parse(request.downloadHandler.text));
                    // var jsondata = Init.Parse(request.downloadHandler.text);
                    // Debug.Log("Response: "+request.downloadHandler.text);
                    // Debug.Log(jsondata.TotalFuel);

                    // JsonList loadedScoreData = JsonList.Parse(request.downloadHandler.text);
                    // Debug.Log(loadedScoreData.data[0].RemainingFuel);
                    // Debug.Log(loadedScoreData.data[0].Location);
                    
                }
            }
        }
    }

    
}
