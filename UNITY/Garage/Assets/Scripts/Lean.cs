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
    private PlayerActionControls playerActionControls;
    private float l = 0;
    private float b = 0;
    public GameObject Vehicle;
    private float Torque = 0f;


    private void Awake(){
        playerActionControls = new PlayerActionControls();
    }

    void Start()
    {
        
    }
    private void OnEnable(){
        playerActionControls.Enable();
    }
    private void OnDisable(){
        playerActionControls.Disable();
    }
    void Go(float lean)
    {   
        float vel = (float)rb.velocity.magnitude;
        var currentZEuler = transform.rotation.eulerAngles.z;
        if(currentZEuler>270){
            currentZEuler = currentZEuler - 360;
        }
        var deltaZEuler = (maxLeanAngle*-lean*vel*0.1f - currentZEuler)*3f*Time.deltaTime;
        transform.Rotate(0, 0, deltaZEuler, Space.Self);
        
    }
    void Wheelie(float liftoff)
    {   
        vel = (float)rb.velocity.magnitude;
        float currentXEuler = transform.rotation.eulerAngles.x -360;
        float currentZEuler = transform.rotation.eulerAngles.z;
        if(Vehicle.tag == "2Wheeler"){
                if(liftoff!=0)
                {   
                    
                    if((currentXEuler+maxWheelieAngle<15 && currentXEuler+maxWheelieAngle>5) || vel <1 ){
                        isWheelie = false;
                        Torque = 200f;
                        rb.AddTorque(transform.right * Torque * liftoff*40f*Time.deltaTime);
                    }
                    else if(b>0 && vel >3)
                    {   
                        isWheelie = true;
                        Torque = (-3000f/Mathf.Max(1,vel*vel))*vel;
                        rb.AddTorque(transform.right * Torque * liftoff*40f*Time.deltaTime);
                    }
                }   
                else{
                        isWheelie = false;
                    }
        
        }
        else if(Vehicle.tag == "3Wheeler"){
            if(currentXEuler<50 && currentXEuler>10)
                {
                    
                    isWheelie = true;
                }
                else
                {
                    isWheelie = false;
                }
                if(liftoff!=0)
                {
                    var deltaZEuler = (maxWheelieAngle*liftoff - currentZEuler)*10f*Time.deltaTime;
                    transform.Rotate(0, 0, deltaZEuler, Space.Self);
                }
        }
        
        
    }
    
    void Update()
    {   
        Vector2 movementInput = playerActionControls.Vehicle.Move.ReadValue<Vector2>();
        float wheelieInput = playerActionControls.Vehicle.Wheelie.ReadValue<float>();
        
        l = movementInput[0];
        b = movementInput[1];
        float j = wheelieInput;
        Go(l);
        Wheelie(j);
        
    }
}

