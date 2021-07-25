using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class SimpleCarController : NetworkBehaviour {

	private PlayerActionControls playerActionControls;
	public Rigidbody rb;
	private float maxSpeed;
    public float tankcap;
    private float mileage;
	public float FR = 1f;
	static public float remainingfuel;
	private float a = 0;
	void Start()
    {
		remainingfuel = FR;
		if(IsLocalPlayer){
            CameraFollowController.objectToFollow = gameObject.transform;
            fuelreader.GO = gameObject.transform.parent.gameObject;
			string s1 = PlayerPrefs.GetString(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_KIT");
        	motorForce = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_Accl") * 1000f * float.Parse("1." + s1.Substring(s1.Length - 1));
        	maxSteerAngle = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_Steer") * 1000f * float.Parse("1." + s1.Substring(s1.Length - 1));
        	maxSpeed = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_MaxSpeed") * 1000f * float.Parse("1." + s1.Substring(s1.Length - 1));
        	mileage = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_Mileage") * 1000f / float.Parse("1." + s1.Substring(s1.Length - 1));
        	tankcap = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_TankCapacity");
        	FR = PlayerPrefs.GetFloat(gameObject.transform.root.name.Replace("(Clone)","").Trim() + "_FR");
			print(motorForce);
       
        }
		 
		fuelreader.TC = tankcap;
        fuelreader.RF = FR;
        fuelreader.M = mileage;
        fuelreader.isKhali = false;
    }
	private void Awake(){
        playerActionControls = new PlayerActionControls();
    }
	private void OnEnable(){
        playerActionControls.Enable();
    }
    private void OnDisable(){
        playerActionControls.Disable();
    }
	public void GetInput()
	{	
		Vector2 movementInput = playerActionControls.Vehicle.Move.ReadValue<Vector2>();
		m_horizontalInput = movementInput[0];
		m_verticalInput = movementInput[1];
	}

	private void Steer(float steer)
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput * steerFactor;
		steer = Mathf.Clamp(steer,-1, 1) * maxSteerAngle;
		WheelFL.steerAngle = Mathf.Lerp(WheelFL.steerAngle, steer, Time.deltaTime*15f);
		WheelFR.steerAngle = Mathf.Lerp(WheelFR.steerAngle, steer, Time.deltaTime*15f);
	}
	private void Accelerate()
	{	
		float vel = Mathf.Max((float)rb.velocity.magnitude,1);
		if(vel<maxSpeed/3.6){
            WheelFL.motorTorque = m_verticalInput * motorForce;
			WheelFR.motorTorque = m_verticalInput * motorForce;
        }
        else{
            WheelFL.motorTorque = 0f;
			WheelFR.motorTorque = 0f;
        }
		

	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(WheelFL, frontDriverT);
		UpdateWheelPose(WheelFR, frontPassengerT);
		UpdateWheelPose(WheelRL, rearDriverT);
		UpdateWheelPose(WheelRR, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	private void FixedUpdate()
	{	
		GetInput();
		
		Accelerate();
		UpdateWheelPoses();
	}
	void Update(){
		Vector2 movementInput = playerActionControls.Vehicle.Move.ReadValue<Vector2>();
		a = movementInput[0];
		Steer(a);
	}

	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	public WheelCollider WheelFL, WheelFR;
	public WheelCollider WheelRL, WheelRR;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	private float maxSteerAngle;
	public float motorForce;
	public float steerFactor = 2;
}