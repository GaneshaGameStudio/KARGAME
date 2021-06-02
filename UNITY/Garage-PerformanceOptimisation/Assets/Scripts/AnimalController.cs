using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{   
    public Rigidbody rb;
    public float MaxVel;
    Animator animalanim;
    // Start is called before the first frame update
    void Start()
    {
        animalanim = gameObject.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float vel = (float)rb.velocity.magnitude*3.6f;
        animalanim.SetFloat("Gallop",vel/MaxVel);
    }
}
