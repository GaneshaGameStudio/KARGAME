using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerHandle : MonoBehaviour
{   
    public float maxSteerAngleFork;
    public Rigidbody rb;
    public GameObject gaadi;

    void Start()
    {
        
    }
    void Go(float steer)
    {   
        float vel = Mathf.Max((float)rb.velocity.magnitude*0.1f,1);
        var currentZEuler = transform.rotation.eulerAngles.z;
        var deltaZEuler = (maxSteerAngleFork*steer/(vel*vel)) - currentZEuler;
        transform.Rotate(0, 0, deltaZEuler, Space.Self);
    }

    void Update()
    {   
        float s = Input.GetAxis("Horizontal");
        if(gaadi.GetComponent<Lean>().isWheelie == true)
        {
            s=0;
        }
        else
        {
            Go(s);
        }
    }
}

