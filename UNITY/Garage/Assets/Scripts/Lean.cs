using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lean : MonoBehaviour
{   
    public float maxLeanAngle;
    public float maxWheelieAngle;
    public Rigidbody rb;
    Animator anim;
    public bool isWheelie;
    public float vel;

    void Start()
    {
        
    }
    void Go(float lean)
    {   
        float vel = (float)rb.velocity.magnitude;
        var currentZEuler = transform.rotation.eulerAngles.z;
        var deltaZEuler = maxLeanAngle*-lean*vel*0.1f - currentZEuler;
        transform.Rotate(0, 0, deltaZEuler, Space.Self);
        
    }
    void Wheelie(float liftoff)
    {   
        vel = (float)rb.velocity.magnitude;
        float currentXEuler = transform.rotation.eulerAngles.x;
        //Debug.Log(currentXEuler);
        if(currentXEuler<354 && currentXEuler>270)
        {
            //Debug.Log("Wheel has left the road");
            isWheelie = true;
        }
        else
        {
            //Debug.Log("Wheel touches the road");
            isWheelie = false;
        }
        if(liftoff!=0)
        {
            var deltaXEuler = maxWheelieAngle*-liftoff - currentXEuler;
            transform.Rotate(deltaXEuler, 0, 0, Space.Self);
        }
        
    }
    
    void Update()
    {  
        float l = Input.GetAxis("Horizontal");
        float j = Input.GetAxis("Jump");
        Go(l);
        Wheelie(j);
        
    }
}

