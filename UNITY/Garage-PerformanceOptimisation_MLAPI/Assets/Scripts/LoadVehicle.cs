using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class LoadVehicle : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        Spawn();
    }
    
    void Spawn(){

        GameObject prefab = Resources.Load("Vehicles_prefabs/" + VehicleID.Vehicle) as GameObject;
        GameObject go = Instantiate(prefab, new Vector3(PlayerPrefs.GetFloat("SpawnLoc.x"), PlayerPrefs.GetFloat("SpawnLoc.y"), PlayerPrefs.GetFloat("SpawnLoc.z")), Quaternion.Euler(new Vector3(PlayerPrefs.GetFloat("SpawnRot.x"), PlayerPrefs.GetFloat("SpawnRot.y"), PlayerPrefs.GetFloat("SpawnRot.z"))));
        go.GetComponent<NetworkObject>().SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);
        
        ChunkingV2.directionquadINIT = (int)((180f -45f)/90f);
        CameraFollowController.objectToFollow = GameObject.Find(VehicleID.Vehicle+"(Clone)").transform;
    }

}
