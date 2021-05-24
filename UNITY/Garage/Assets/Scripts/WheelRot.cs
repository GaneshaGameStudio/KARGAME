using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRot : MonoBehaviour
{   
    public WheelCollider WC;
    public GameObject Wheel;
    void Start()
    {
        WC = this.GetComponent<WheelCollider>();
    }

    void Rot()
    {   
        Quaternion quat;
        Vector3 position;
        WC.GetWorldPose(out position, out quat);
        Wheel.transform.position = position;
        Wheel.transform.rotation = quat;
    }
    
    // Update is called once per frame
    void Update()
    {   
        Rot();
    }
}
