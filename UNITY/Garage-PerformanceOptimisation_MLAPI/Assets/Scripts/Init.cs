using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text;




[System.Serializable]
public class Init
{
    // resulting JSON from an API request
    public JSONNode jsonResult;

    public string apiParam;
    
    // static string data = null;
    private string apiUrl = "http://127.0.0.1:8000/";

    


    public string Stringify() {
        return JsonUtility.ToJson(this);
    }

    private static Init Parse(string json)
    {
        return JsonUtility.FromJson<Init>(json);
    }


    public IEnumerator Download(string id, System.Action<JSONNode> callback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl + id))
        {
            yield return request.SendWebRequest();

            if ((request.result == UnityWebRequest.Result.ConnectionError) || (request.result == UnityWebRequest.Result.ProtocolError))
            {
                Debug.Log(apiUrl + id);
                Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(null);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(Processjson(request.downloadHandler.text));
                }
            }
        }
    }


    public IEnumerator NetCheck(string id, System.Action<string> callback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl + id))
        {
            yield return request.SendWebRequest();

            if ((request.result == UnityWebRequest.Result.ConnectionError) || (request.result == UnityWebRequest.Result.ProtocolError))
            {
                Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(request.error);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(request.downloadHandler.text);
                }
            }
        }
    }



    public IEnumerator Post(JSONObject jobject,System.Action<string> callback = null)
    {
        string postData = jobject.ToString();
        var request = new UnityWebRequest(apiUrl+"playerprefspush", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(postData);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if ((request.result == UnityWebRequest.Result.ConnectionError) || (request.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.Log(request.error);
            if(callback != null) {
                callback.Invoke(request.error);
            }
        }
        else
        {
            // Debug.Log(request.downloadHandler.text);
            if(callback != null) {
                callback.Invoke(request.downloadHandler.text);
            }
            Debug.Log("Status code "+request.responseCode);
        }

    }

    private JSONNode Processjson(string jsonString)
     {
         // parse the raw string into a json result we can easily read
        jsonResult = JSON.Parse(jsonString);

        // display the results on screen
        // Debug.Log(jsonResult["data"][0]["RemainingFuel"]);

        return jsonResult;
     }

    
}
