using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDrive : MonoBehaviour
{   
    public WheelCollider WC;
    public float torque = 75;
    public float maxSteerAngle = 30;
    public GameObject Wheel;
    public Rigidbody rb;
    public GameObject gaadi;
    // Start is called before the first frame update
    void Start()
    {
        WC = this.GetComponent<WheelCollider>();
    }

    void Go(float accel, float steer)
    {   
        float vel = Mathf.Max((float)rb.velocity.magnitude*0.5f,1);
        accel = Mathf.Clamp(accel,-1, 1);
        steer = Mathf.Clamp(steer,-1, 1) * maxSteerAngle / vel;
        float thrustTorque = accel * torque;
        WC.motorTorque = thrustTorque;
        WC.steerAngle = steer;

        Quaternion quat;
        Vector3 position;
        WC.GetWorldPose(out position, out quat);
        Wheel.transform.position = position;
        Wheel.transform.rotation = quat;

        var velDir = transform.InverseTransformDirection(rb.velocity);
        if (velDir.z < -0.1)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 5*0.2776f);
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        float w = Input.GetAxis("Vertical");
        float a = Input.GetAxis("Horizontal");
        if(gaadi.GetComponent<Lean>().vel > 80/3.6f)
        {
            Debug .Log("Speed limit!!");
            w=0;
        }
        else
        {
            Go(w,a);
        }
    }
}
