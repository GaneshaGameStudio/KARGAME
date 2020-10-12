﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animate : MonoBehaviour
{
    public Rigidbody rb;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float vel = (float)rb.velocity.magnitude*3.6f;
        anim.SetFloat("MagVel",vel);
        float a = Input.GetAxis("Horizontal");
        float w = Input.GetAxis("Vertical");
        anim.SetFloat("MagDir",a);
        if(a!=0)
        {
            anim.SetBool("keynotPressed",true);
        }
        else
        {
            anim.SetBool("keynotPressed",false);
        }
        var velDir = transform.InverseTransformDirection(rb.velocity);
        if(w<0 && velDir.z < -0.01)
        {
            anim.SetBool("notgoingReverse",false);
        }
        else
        {
            anim.SetBool("notgoingReverse",true);
        }

        anim.SetBool("isWheelie",GameObject.Find("HN-Dio_stock").GetComponent<Lean>().isWheelie);
       
    }
}
