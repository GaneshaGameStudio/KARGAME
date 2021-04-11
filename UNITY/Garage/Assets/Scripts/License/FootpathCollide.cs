using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootpathCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision gaadi)
    {   
        Debug.Log("fail");
        if(gaadi.collider.tag == "2Wheeler"){
            Debug.Log("fail");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
