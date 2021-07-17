using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRot : MonoBehaviour
{   
    public WheelCollider WC;
    public GameObject Wheel;
    public GameObject Holder;
    void Start()
    {
        WC = this.GetComponent<WheelCollider>();
    }
    
    void Rot()
    {   
        Quaternion quat;
        Vector3 position;
        WC.GetWorldPose(out position, out quat);
        //Wheel.transform.position = position;
        Wheel.transform.rotation = quat;
    }
    
        
            
    
    // Update is called once per frame
    void Update()
    
    {   
        if(WC.name!="WheelFC"){
            if(WC.GetGroundHit(out WheelHit hit)){
            Holder.transform.GetChild(1).GetComponent<Collider>().enabled = true;
        }
        else{
            Holder.transform.GetChild(1).GetComponent<Collider>().enabled = false;
        }
        }
        
        
        
        Rot();
    }
}
