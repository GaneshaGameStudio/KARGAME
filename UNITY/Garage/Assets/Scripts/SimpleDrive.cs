﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDrive : MonoBehaviour
{   
    public WheelCollider WC;
    public float torque = 75;
    public float maxSteerAngle = 30;
    public GameObject Wheel;
    public GameObject Vehicle;
    public Rigidbody rb;
    private PlayerActionControls playerActionControls;
    private float a = 0;
    public float maxSpeed = 30f;
    public float tankcap;
    public float mileage;
    public float FR = 1f;
    static public float remainingfuel;
    // Start is called before the first frame update
    private void Awake(){
        playerActionControls = new PlayerActionControls();
        
    }
    private void OnEnable(){
        playerActionControls.Enable();
    }
    private void OnDisable(){
        playerActionControls.Disable();
    }
    void Start()
    {
        WC = this.GetComponent<WheelCollider>();
        remainingfuel = FR;
    }

    void Go(float accel, float steer)
    {   
        float vel = Mathf.Max((float)rb.velocity.magnitude,1);
        accel = Mathf.Clamp(accel,-1, 1);
        steer = Mathf.Clamp(steer,-1, 1) * maxSteerAngle / vel;
        float thrustTorque = accel * torque;
        if(vel<maxSpeed/3.6){
            WC.motorTorque = thrustTorque;
        }
        else{
            WC.motorTorque = 0f;
        }
        WC.steerAngle = steer*Time.deltaTime*30f;
        

        Quaternion quat;
        Vector3 position;
        WC.GetWorldPose(out position, out quat);
        Wheel.transform.position = position;
        Wheel.transform.rotation = quat;

        var velDir = transform.InverseTransformDirection(rb.velocity);
        if (velDir.z < -0.1)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 5f*0.2776f);
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {   
        Vector2 movementInput = playerActionControls.Vehicle.Move.ReadValue<Vector2>();
        float w = movementInput[1];
        
        a = Mathf.MoveTowards(movementInput[0], a, 0.7f * Time.deltaTime);
        Go(w,a);

    }
}
