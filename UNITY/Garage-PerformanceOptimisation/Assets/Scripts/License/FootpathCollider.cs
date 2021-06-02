using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootpathCollider : MonoBehaviour
{
    
    
    void OnCollisionEnter(Collision gaadi)
    {   
        
        if(gaadi.collider.tag == "2Wheeler" || gaadi.collider.tag == "4Wheeler" || gaadi.collider.tag == "3Wheeler" || gaadi.collider.tag == "6Wheeler"){
            Debug.Log("fail");
        }
    }
    
}
