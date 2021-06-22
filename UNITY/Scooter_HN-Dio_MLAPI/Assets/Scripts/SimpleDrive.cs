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
    private PlayerActionControls playerActionControls;
    private float a = 0;
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
        
    }

    void Go(float accel, float steer)
    {   
        float vel = Mathf.Max((float)rb.velocity.magnitude*0.5f,1);
        accel = Mathf.Clamp(accel,-1, 1);
        steer = Mathf.Clamp(steer,-1, 1) * maxSteerAngle / vel;
        float thrustTorque = accel * torque;
        WC.motorTorque = thrustTorque;
        WC.steerAngle = steer*Time.deltaTime*30f;

        Quaternion quat;
        Vector3 position;
        WC.GetWorldPose(out position, out quat);
        Wheel.transform.position = position;
        Wheel.transform.rotation = quat;

        var velDir = transform.InverseTransformDirection(rb.velocity);
        if (velDir.z < -0.1)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 0.2776f);
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {   
        Vector2 movementInput = playerActionControls.Vehicle.Move.ReadValue<Vector2>();
        float w = movementInput[1];
        a = Mathf.MoveTowards(movementInput[0], a, 0.7f * Time.deltaTime);
        if(GameObject.Find("HN-Dio_stock(Clone)").GetComponent<Lean>().vel > 80/3.6f)
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
