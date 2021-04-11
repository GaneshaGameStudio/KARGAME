using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicenseCollider : MonoBehaviour
{   
    public GameObject FC1;
    public GameObject FC2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(other.tag=="2Wheeler"){
            FC1.tag ="Untagged";
            FC2.tag ="License";
        
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
