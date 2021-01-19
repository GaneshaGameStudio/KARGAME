using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rpmreader : MonoBehaviour
{   
    private float rpmmax;
    private float rectwidth;
    private float wheelradius;
    private GameObject GO;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {   
        rpmmax = 200f;
        rectwidth = 110f;
    }

    // Update is called once per frame
    void Update()
    {   
        GO = GameObject.Find(VehicleID.Vehicle + "(Clone)");
        rb = GO.GetComponent<Rigidbody>();
        wheelradius = GameObject.FindWithTag("WheelFC").GetComponent<WheelCollider>().radius;
        float currentrpm = rb.velocity.magnitude*3.6f/(wheelradius*0.10472f);
        float normrpm = Mathf.Pow((currentrpm*0.015f),2)/rpmmax;
        GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Max(10f,Mathf.Min(Mathf.Log(normrpm)*rectwidth,rectwidth)), 15f);
    }
}
