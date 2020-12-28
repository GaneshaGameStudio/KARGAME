using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class AudioController : MonoBehaviour
{   
    public string url;
    private float timestamp;
    private string currentTS;
    private int i;
    // Start is called before the first frame update
    void Start()
    {   
        timestamp = 0;
        //StartCoroutine(GetCurrentTimestamp());
        StartCoroutine(AudioPlayer());
        //StartCoroutine(PlayAudio());
    }
    private IEnumerator AudioPlayer(){
        //i = currentTS;
        StartCoroutine(GetCurrentTimestamp());
        yield return new WaitForSeconds(0.75f);
        i = int.Parse(currentTS);
        print(i);
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
                        AudioPlaying:
                            if (!GetComponent<AudioSource>().isPlaying)
                            {
                                GetComponent<AudioSource>().clip = myClip;
                                GetComponent<AudioSource>().Play();
                                i=i+1; 
                            }
                            else{
                                goto AudioPlaying;
                            }
                        
                        
                    }
                    yield return new WaitForSeconds(0f);
            }
        }
        
        }
    }
    private IEnumerator GetCurrentTimestamp(){
        UnityWebRequest www = UnityWebRequest.Get(url + "currenttimestamp.txt");
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            currentTS = (www.downloadHandler.text);
            yield return currentTS ;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
}
