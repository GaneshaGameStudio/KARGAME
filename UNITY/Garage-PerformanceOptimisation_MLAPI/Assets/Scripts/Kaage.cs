using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaage : MonoBehaviour
{   
    
    // Start is called before the first frame update
    void OnEnable()
    {       
        if(int.Parse(DisplayTime.wordstime[1])<7 || int.Parse(DisplayTime.wordstime[1])>=17){
            gameObject.GetComponent<ParticleSystem>().enableEmission = false;
        }
        InvokeRepeating("Updateposition", 0, 10.0f);
    }
    void Updateposition(){
        
        transform.position = Camera.main.transform.position + new Vector3 (0f,2.5f,0f);
        
    }
    // Update is called once per frame
    void Update()
    {   
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.maxParticles = (int)(rpmreader.normrpm * 100f);
    }
}
