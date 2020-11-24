using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class AudioController : MonoBehaviour
{   
    public string url;
    private float timestamp;
    private float waittime;
    // Start is called before the first frame update
    void Start()
    {   
        timestamp = 0;
        StartCoroutine(AudioPlayer());
        //StartCoroutine(PlayAudio());
    }
    private IEnumerator AudioPlayer(){
        WWW music = new WWW(url);
        while(true){
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS))
         
         
         {
             yield return www.SendWebRequest();
             

        
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {   
                    
                    AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                    if(myClip.length!=0){

                        GetComponent<AudioSource>().clip = myClip;
                        GetComponent<AudioSource>().time = timestamp;
                        GetComponent<AudioSource>().Play();
                        print(timestamp);
                        waittime = myClip.length - timestamp;
                        timestamp = myClip.length;
                        print(myClip.length);
                    }
                    yield return new WaitForSeconds(waittime-0.5);
            }
        }
        
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
}
