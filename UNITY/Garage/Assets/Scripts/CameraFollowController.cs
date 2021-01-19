using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour {

	private bool isWheelieE;
	void Start()
	{
		objectToFollow = GameObject.Find(VehicleID.Vehicle+"(Clone)").transform;
	}
	public void LookAtTarget()
	{
		Vector3 _lookDirection = objectToFollow.position - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
	}

	public void MoveToTarget()
	{	
		
		Vector3 _targetPos = objectToFollow.position + 
							 objectToFollow.forward * offset.z + 
							 objectToFollow.right * offset.x + 
							 objectToFollow.up * offset.y;
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
	}
	

	private void FixedUpdate()
	{	
		if(GameObject.Find(VehicleID.Vehicle+"(Clone)").tag == "2Wheeler"){
			if(GameObject.Find(VehicleID.Vehicle+"(Clone)").GetComponent<Lean>().isWheelie == true)
				{	
					followSpeed = 1;
					lookSpeed = 1;
					offset.x = 0f;
					offset.y = 7f;
					offset.z = 0f;
					
				}
				else
				{	
					
					offset.x = 0f;
					offset.y = 2.0f;
					offset.z = -3.0f;
					followSpeed = 13;
					lookSpeed = 10;
				}
		}
		else if(GameObject.Find(VehicleID.Vehicle+"(Clone)").tag == "6Wheeler"){
			offset.x = 0f;
			offset.y = 4.58f;
			offset.z = -9.8f;
			followSpeed = 20;
			lookSpeed = 10;
		}
		else
		{	
			offset.x = 0f;
			offset.y = 3.5f;
			offset.z = -4.8f;
			followSpeed = 13;
			lookSpeed = 10;
		}
		LookAtTarget();
		MoveToTarget();
		
	}

	private Transform objectToFollow;
	public Vector3 offset;
	public float followSpeed = 10;
	public float lookSpeed = 10;
}