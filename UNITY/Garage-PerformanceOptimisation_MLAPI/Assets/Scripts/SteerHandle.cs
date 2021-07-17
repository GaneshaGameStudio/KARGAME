using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerHandle : MonoBehaviour
{   
    public float maxSteerAngleFork = 30;
    public Rigidbody rb;
    private PlayerActionControls playerActionControls;
    private float s = 0f;
    public GameObject Vehicle;
    public WheelCollider WC;
    public GameObject Wheel;

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
    void Go(float steer)
    {   
        float vel = Mathf.Max((float)rb.velocity.magnitude*0.1f,1);
        var currentZEuler = transform.rotation.eulerAngles.z;
        if(currentZEuler>270){
            currentZEuler = currentZEuler - 360;
        }
        Quaternion quat;
        Vector3 position;
        WC.GetWorldPose(out position, out quat);
        var deltaZEuler = (((maxSteerAngleFork)*steer)/(vel*vel) - currentZEuler)*3f*Time.deltaTime;
        transform.Rotate(0, 0, deltaZEuler, Space.Self);
        Wheel.transform.Rotate(quat.eulerAngles.x - Wheel.transform.rotation.eulerAngles.x,0,0);

    }
    
    void Update()
    {   
        Vector2 movementInput = playerActionControls.Vehicle.Move.ReadValue<Vector2>();
        s = movementInput[0];
        
        if(Vehicle.GetComponent<Lean>().isWheelie == true)
        {
            s=0;
        }
        else
        {
            Go(s);
            
        }
    }
}

