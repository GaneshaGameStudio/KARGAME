using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSignal : MonoBehaviour
{   
    public float signaltime;
    public Texture2D Rtexture;
    public Texture2D Gtexture;
    public Renderer[] Signal;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine(signaltime));
    }
    IEnumerator ExampleCoroutine(float signaltime)
    {   
        while(Time.deltaTime>0){
            int i = 0;
            foreach(Transform child in transform)
            {   
                Collider col = child.GetComponent<Collider>();
                col.enabled = false;
                Signal[i].material.mainTexture = Gtexture;
                yield return new WaitForSeconds(signaltime);
                col.enabled = true;
                Signal[i].material.mainTexture = Rtexture;
                yield return new WaitForSeconds(signaltime*0.2f);
                i++;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
