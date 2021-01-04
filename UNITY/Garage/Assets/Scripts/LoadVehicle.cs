using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVehicle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Resources.Load(VehicleID.Vehicle), new Vector3(1045.6f, 0f, 2115.6f), Quaternion.Euler(new Vector3(0f, 180f, 0f)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
