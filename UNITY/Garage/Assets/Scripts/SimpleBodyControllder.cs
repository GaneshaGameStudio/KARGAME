using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBodyController : MonoBehaviour
{   
    public float tankcap;
    public float mileage;
	static public float remainingfuel;
    public float FR = 1f;
    // Start is called before the first frame update
    void Start()
    {
        remainingfuel = FR;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
