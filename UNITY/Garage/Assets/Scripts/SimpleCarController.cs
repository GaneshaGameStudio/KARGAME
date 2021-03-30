﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour {

	private PlayerActionControls playerActionControls;
	public Rigidbody rb;
	public float maxSpeed = 30f;
    public float tankcap;
    public float mileage;
	public float FR = 1f;
	static public float remainingfuel;
	void Start()
    {
        remainingfuel = FR;
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

	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput * steerFactor;
		WheelFL.steerAngle = m_steeringAngle;
		WheelFR.steerAngle = m_steeringAngle;
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
		Steer();
		Accelerate();
		UpdateWheelPoses();
	}

	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	public WheelCollider WheelFL, WheelFR;
	public WheelCollider WheelRL, WheelRR;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 30;
	public float motorForce = 50;
	public float steerFactor = 2;
}