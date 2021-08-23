﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarEngine : MonoBehaviour
{   
    public Transform path;
    private List<Transform> nodes;
    public int currentNode = 0;
    public float maxSteerAngle = 50;
    public WheelCollider wheelFL, wheelFR;
	public WheelCollider wheelRL, wheelRR;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
    public float maxMotorTorque = -500f;
    public float currentSpeed;
    public float maxSpeed = 10f;
    public Vector3 centerOfMass;

    [Header("Sensors")]
    public float sensorLength = 5;
    public Vector3 frontsensorpos;
    public float sidesensorpos = 1f;
    public float sensorangle = 30f;
    // Start is called before the first frame update
    void Start()
    {   
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for(int i = 0; i < pathTransforms.Length; i++){
            if(pathTransforms[i] != path.transform){
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Sensors();
        ApplySteer();
        Drive();
        UpdateWheelPoses();
        CheckWaypointDistance();
        Destroy();
        
    }
    private void Sensors(){
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos -= transform.forward * frontsensorpos.z;
        sensorStartPos -= transform.up * frontsensorpos.y;
        bool avoiding = false;
        //frontcenter
        if(Physics.Raycast(sensorStartPos, -1*transform.forward, out hit, sensorLength)){
            if(hit.collider.CompareTag("Signal") || hit.collider.CompareTag("Traffic") || hit.collider.CompareTag("Kit")){
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
            }
        }
        
        //frontright
        sensorStartPos += transform.right*sidesensorpos;
        if(Physics.Raycast(sensorStartPos, -1*transform.forward, out hit, sensorLength)){
            if(hit.collider.CompareTag("Signal") || hit.collider.CompareTag("Traffic")){
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
            }
        }
        
        //frontrightangle
        if(Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-sensorangle, transform.up)*transform.forward*-1, out hit, sensorLength)){
            if(hit.collider.CompareTag("Signal") || hit.collider.CompareTag("Traffic")|| hit.collider.CompareTag("Kit")){
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
            }
        }
        
        //frontleft
        sensorStartPos -= transform.right*sidesensorpos*2;
        if(Physics.Raycast(sensorStartPos, -1*transform.forward, out hit, sensorLength)){
            if(hit.collider.CompareTag("Signal") || hit.collider.CompareTag("Traffic")|| hit.collider.CompareTag("Kit")){
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
            }
        }
        
        //frontleftangle
        if(Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(sensorangle, transform.up)*transform.forward*-1, out hit, sensorLength)){
            if(hit.collider.CompareTag("Signal") || hit.collider.CompareTag("Traffic")|| hit.collider.CompareTag("Kit")){
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
            }
        }
        if(avoiding){
            wheelRL.brakeTorque=-2*maxMotorTorque;
            wheelRR.brakeTorque=-2*maxMotorTorque;
        }
        else{
            wheelRL.brakeTorque=0;
            wheelRR.brakeTorque=0;
        }
        
    }
    private void ApplySteer(){
        try{
            Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
            relativeVector = relativeVector / relativeVector.magnitude;
            float newSteer = (relativeVector.x / relativeVector.magnitude)*-maxSteerAngle;
            wheelFL.steerAngle = newSteer*1.2f;
            wheelFR.steerAngle = newSteer*1.2f;
        }
        catch (Exception e)
        {
            Destroy(this.gameObject);
        }
        
    }
    private void Drive(){
        currentSpeed = - 2 * Mathf.PI * wheelRL.radius * wheelRL.rpm * 60/1000;
        if(currentSpeed<maxSpeed){
            wheelFL.motorTorque=maxMotorTorque;
            wheelFR.motorTorque=maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque=0;
            wheelFR.motorTorque=0;
        }

    }
    private void CheckWaypointDistance(){
        print(currentNode);
        //print(nodes.Count);
        try{
            if(Vector3.Distance(transform.position, nodes[currentNode].position) < 20f){
                if(currentNode == nodes.Count - 1){
                    currentNode = 0;
                }
            else
            {
                currentNode++;
            } 
        }
        }
        catch (Exception e)
        {
            Destroy(this.gameObject);
        }
        
    }
    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}
    private void UpdateWheelPoses()
	{
		UpdateWheelPose(wheelFL, frontDriverT);
		UpdateWheelPose(wheelFR, frontPassengerT);
		UpdateWheelPose(wheelRL, rearDriverT);
		UpdateWheelPose(wheelRR, rearPassengerT);
	}
    private void Destroy(){
        
        Camera maincam = GameObject.Find("Camera").GetComponent<Camera>();
        double checkdist = Mathf.Pow(Mathf.Pow((transform.position.x - maincam.transform.position.x),2f) + Mathf.Pow((transform.position.z - maincam.transform.position.z),2f) + Mathf.Pow((transform.position.y - maincam.transform.position.y),2f),0.5f);
        if(checkdist>GameObject.Find("Camera").GetComponent<TrafficPool>().range1){
                
                Destroy(this.gameObject);
                return;
        }
    }
    
    void OnTriggerEnter(Collider other){

        if(other.gameObject.tag == "Traffic"){
            Destroy(this.gameObject);
            return;
        }
        
    }
}
