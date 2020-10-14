using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSignal : MonoBehaviour
{   public float signaltime;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine(signaltime));
    }
    IEnumerator ExampleCoroutine(float signaltime)
    {   
        while(Time.deltaTime>0){
            foreach(Transform child in transform)
            {   
                Collider col = child.GetComponent<Collider>();
                col.enabled = false;
                yield return new WaitForSeconds(signaltime);
                col.enabled = true;
                yield return new WaitForSeconds(signaltime*0.2f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
