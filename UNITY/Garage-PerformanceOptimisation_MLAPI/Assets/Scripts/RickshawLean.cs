using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RickshawLean : MonoBehaviour
{   
    public float maxLeanAngle;
    public float maxWheelieAngle;
    public Rigidbody rb;
    Animator anim;
    public bool isWheelie;
    public float vel;
    private PlayerActionControls playerActionControls;
    private float l = 0;

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
        var deltaZEuler = maxLeanAngle*-lean*vel*0.1f - currentZEuler;
        
    }
    void Wheelie(float liftoff)
    {   
        vel = (float)rb.velocity.magnitude;
        float currentXEuler = transform.rotation.eulerAngles.z;
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
            var deltaXEuler = maxWheelieAngle*-liftoff - currentXEuler;
            transform.Rotate(0, 0, deltaXEuler, Space.Self);
        }
        
    }
    
    void Update()
    {  
        Vector2 movementInput = playerActionControls.Vehicle.Move.ReadValue<Vector2>();
        float wheelieInput = playerActionControls.Vehicle.Wheelie.ReadValue<float>();
        l = movementInput[0];
        float j = wheelieInput;
        Go(l);
        Wheelie(j);
        
    }
}

