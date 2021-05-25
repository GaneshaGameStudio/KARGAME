using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
public class AudioController : MonoBehaviour
{   
    public string url;
    //private float timestamp;
    private string currentTS;
    private int i;
    private AudioClip myClip;
    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine(AudioPlayer());
    }
    private IEnumerator AudioPlayer(){
        //i = currentTS;
        StartCoroutine(GetCurrentTimestamp());
        yield return new WaitForSeconds(2.0f);
        
        UnityWebRequest www;
        while(true){
            //yield return new WaitForSeconds(1f);
            //UnityWebRequest music = UnityWebRequest.Get(url);

            www = UnityWebRequestMultimedia.GetAudioClip(url+"mixedstream" + i + ".ogg", AudioType.OGGVORBIS);
            
            yield return www.SendWebRequest();
            try{
                myClip = DownloadHandlerAudioClip.GetContent(www);
                ((DownloadHandlerAudioClip)www.downloadHandler).streamAudio = false;
                ((DownloadHandlerAudioClip)www.downloadHandler).compressed = false;
                }
            catch (Exception e){
                StartCoroutine(GetCurrentTimestamp());
                }
            yield return new WaitWhile (()=> GetComponent<AudioSource>().isPlaying);      
                            
                GetComponent<AudioSource>().clip = myClip;
                GetComponent<AudioSource>().Play();
                i=i+1; 
                
            }
    }
        
    private IEnumerator GetCurrentTimestamp(){
        UnityWebRequest wwe;
        while(true){
        try{
            wwe = UnityWebRequest.Get(url + "currenttimestamp.txt");
            break;
        }
        catch (Exception e){
                continue;
            } 
        }
        
        yield return wwe.SendWebRequest();
            // Show results as text
            try{
                currentTS = (wwe.downloadHandler.text);
            i = int.Parse(currentTS);
            }catch(FormatException e){
                // Debug.Log("Not connected to DB");
            }
            
            yield return currentTS ;
    }
    
}
