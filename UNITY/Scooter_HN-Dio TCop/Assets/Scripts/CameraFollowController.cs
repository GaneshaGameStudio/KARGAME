using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour {

	private bool isWheelieE;
	void Start()
	{
		
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
		if(GameObject.Find("HN-Dio_stock").GetComponent<Lean>().isWheelie == true)
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
			offset.z = -3.8f;
			followSpeed = 13;
			lookSpeed = 10;
		}
		LookAtTarget();
		MoveToTarget();
		
	}

	public Transform objectToFollow;
	public Vector3 offset;
	public float followSpeed = 10;
	public float lookSpeed = 10;
}