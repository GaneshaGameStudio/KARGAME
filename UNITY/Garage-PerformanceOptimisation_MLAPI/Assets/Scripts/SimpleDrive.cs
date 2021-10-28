using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.SceneManagement;
public class SimpleDrive : MonoBehaviour
{   
    public WheelCollider WC;
    public float torque;
    private float maxSteerAngle;
    private float maxSpeed;
    public float FR;
    private float mileage;
    public float tankcap;
    public GameObject Vehicle;
    public Rigidbody rb;
    private PlayerActionControls playerActionControls;
    private float a = 0;
    
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
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name =="Bangalore" || scene.name =="VehicleLicense"){
            if(gameObject.transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer){
        }
        
        string s1 = PlayerPrefs.GetString(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_KIT");
        torque = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_Accl") * 1000f * float.Parse("1." + s1.Substring(s1.Length - 1));
        maxSteerAngle = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_Steer") * 1000f * float.Parse("1." + s1.Substring(s1.Length - 1));
        maxSpeed = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_MaxSpeed") * 1000f * float.Parse("1." + s1.Substring(s1.Length - 1));
        mileage = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_Mileage") * 1000f / float.Parse("1." + s1.Substring(s1.Length - 1));
        tankcap = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_TankCapacity");
        FR = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_FR");
        fuelreader.TC = tankcap;
        fuelreader.RF = FR;
        fuelreader.M = mileage;
        fuelreader.isKhali = false;
        }
    }

    void Go(float accel, float steer)
    {   
        float vel = Mathf.Max((float)rb.velocity.magnitude,1);
        accel = Mathf.Clamp(accel,-1, 1);
        steer = Mathf.Clamp(steer,-1, 1) * maxSteerAngle / vel;
        float thrustTorque = accel * torque;
        if(vel<maxSpeed/3.6f){
            WC.motorTorque = thrustTorque;
        }
        else{
            WC.motorTorque = 0f;
        }

        WC.steerAngle = Mathf.Lerp(WC.steerAngle, steer, Time.deltaTime*15f);

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
