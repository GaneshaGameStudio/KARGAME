using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVehicle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Resources.Load(VehicleID.Vehicle), new Vector3(PlayerPrefs.GetFloat("SpawnLoc.x"), PlayerPrefs.GetFloat("SpawnLoc.y"), PlayerPrefs.GetFloat("SpawnLoc.z")), Quaternion.Euler(new Vector3(PlayerPrefs.GetFloat("SpawnRot.x"), PlayerPrefs.GetFloat("SpawnRot.y"), PlayerPrefs.GetFloat("SpawnRot.z"))));
        ChunkingV2.directionquadINIT = (int)((180f -45f)/90f);;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
