using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class VehicleAudio : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        AudioSource m_MyAudioSource;
        m_MyAudioSource = GetComponent<AudioSource>();
        if(!IsLocalPlayer){
            
            m_MyAudioSource.spatialBlend = 0.5f;
        }
        if(SceneManager.GetActiveScene().name=="ModShop"){
            m_MyAudioSource.enabled = false;
        }
        else{
            Camera.main.gameObject.GetComponent<AudioController>().enabled = false;
        Camera.main.gameObject.GetComponent<AudioSource>().enabled = false;
        }
    }


}
