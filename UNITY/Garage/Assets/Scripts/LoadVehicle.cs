using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVehicle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Resources.Load(VehicleID.Vehicle), new Vector3(1050.6f, 0f, 2130.6f), Quaternion.Euler(new Vector3(0f, 205f, 0f)));
        ChunkingV2.directionquadINIT = (int)((180f -45f)/90f);;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
