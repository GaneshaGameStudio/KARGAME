using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicenseCollider : MonoBehaviour
{   
    public GameObject FC1;
    public GameObject FC2;
    
    void OnTriggerEnter(Collider other){
        if(other.tag=="2Wheeler" || other.tag =="3Wheeler" || other.tag=="4Wheeler" || other.tag=="6Wheeler"){
            FC1.tag ="Untagged";
            FC2.tag ="License";
        }
    }
    
}
