using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;

public class Lean : NetworkBehaviour
{   
    public float maxLeanAngle;
    public float maxWheelieAngle;
    public Rigidbody rb;
    public Rigidbody rbt;
    Animator anim;
    public bool isWheelie;
    public float vel;
    private PlayerActionControls playerActionControls;
    private float l = 0;
    private float b = 0;
    public GameObject target;
    private float Torque = 0f;

    private void Awake(){
        playerActionControls = new PlayerActionControls();
    }

    void Start()
    {   if(IsLocalPlayer){
        CameraFollowController.objectToFollow = gameObject.transform;
    }
        
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
        float currentXEuler = transform.rotation.eulerAngles.x - 360;
        
        
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
            if(currentXEuler+maxWheelieAngle<15 && currentXEuler+maxWheelieAngle>5){
                Torque = 500f;
            }
            else if(b>0 && vel >3)
            {
                Torque = (-3000f/Mathf.Max(1,vel*vel))*vel;
                rbt.AddTorque(transform.right * Torque * liftoff*40f*Time.deltaTime);
            }
            
            
        }
        
         
        
    }
    
    void Update()
    {   if(IsLocalPlayer){
        Vector2 movementInput = playerActionControls.Vehicle.Move.ReadValue<Vector2>();
        float wheelieInput = playerActionControls.Vehicle.Wheelie.ReadValue<float>();
        
        l = movementInput[0];
        b = movementInput[1];
        float j = wheelieInput;
        Go(l);
        Wheelie(j);
    }
        
        
    }
}

