using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class AudioController : MonoBehaviour
{   
    public string url;
    private float timestamp;
    // Start is called before the first frame update
    void Start()
    {   
        timestamp = 0;
        StartCoroutine(AudioPlayer());
        //StartCoroutine(PlayAudio());
    }
    private IEnumerator AudioPlayer(){
        int i = 1;
        while(true){
        WWW music = new WWW(url);
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url+"mixedstream" + i + ".ogg", AudioType.OGGVORBIS))
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
                        if (!GetComponent<AudioSource>().isPlaying){
                            GetComponent<AudioSource>().clip = myClip;
                            GetComponent<AudioSource>().Play();
                            i=i+1; 
                        }
                        
                        
                    }
                    yield return new WaitForSeconds(0f);
            }
        }
        
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
}
