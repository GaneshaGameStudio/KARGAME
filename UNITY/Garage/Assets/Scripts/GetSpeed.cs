using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSpeed : MonoBehaviour
{   

    public Rigidbody rb;
    public float vel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vel = rb.velocity.magnitude;

    }
}
