﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{   
    public static bool LightOn;
    public Material[] material;
    private GameObject[] HeadLights;
    // Start is called before the first frame update
    void OnEnable()
    {
        
        if(gameObject.tag=="Lightsprojector" && Lights.LightOn==true){
            gameObject.SetActive(false);
        }
        else{
            StartCoroutine("CheckLights");
        }
    }
    private IEnumerator CheckLights(){
        
        while(true){
                yield return new WaitForSeconds(1f);
                HeadLights = GameObject.FindGameObjectsWithTag("Lights");
                if(LightOn){
            for (int i = 0; i < material.Length; i++){
                material[i].EnableKeyword("_EMISSION");
            }
            foreach (GameObject headlight in HeadLights)
                {
                    Light myLight = headlight.GetComponent<Light>();
                    myLight.enabled = true;
                    print("lights are ON");
                }
        }
        else{
            for (int i = 0; i < material.Length; i++){
                material[i].DisableKeyword("_EMISSION");
            }
            foreach (GameObject headlight in HeadLights)
            {
                Light myLight = headlight.GetComponent<Light>();
                myLight.enabled = false;
            }
        }
        yield return new WaitForSeconds(10f);
        }
        
    }
}
