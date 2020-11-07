using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{   
    public bool LightOn;
    public Material[] material;
    private GameObject[] HeadLights;
    // Start is called before the first frame update
    void Start()
    {
        HeadLights = GameObject.FindGameObjectsWithTag("Lights");
    }

    // Update is called once per frame
    void Update()
    {
        if(LightOn){
            for (int i = 0; i < material.Length; i++){
                material[i].EnableKeyword("_EMISSION");
            }
            foreach (GameObject headlight in HeadLights)
                {
                    Light myLight = headlight.GetComponent<Light>();
                    myLight.enabled = true;
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
    }
}
