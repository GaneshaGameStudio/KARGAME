using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;

public class LoadVehicle : NetworkBehaviour
{   
    
    // Start is called before the first frame update
    void Start()
    {   
        ulong id =  NetworkManager.Singleton.LocalClientId;
        SpawnServerRpc(id);
        if(IsLocalPlayer){
            Camera.main.GetComponent<CameraFollowController>().enabled = true;
            GameObject.Find("fuel").GetComponent<fuelreader>().enabled = true;
            GameObject.Find("rpm").GetComponent<rpmreader>().enabled = true;
            ChunkingV2.directionquadINIT = (int)((180f -45f)/90f);
            }
    }
    [ServerRpc]
    void SpawnServerRpc(ulong id){

        GameObject prefab = Resources.Load("Vehicles_prefabs/" + VehicleID.Vehicle) as GameObject;
        GameObject go = Instantiate(prefab, new Vector3(PlayerPrefs.GetFloat("SpawnLoc.x"), PlayerPrefs.GetFloat("SpawnLoc.y"), PlayerPrefs.GetFloat("SpawnLoc.z")), Quaternion.Euler(new Vector3(PlayerPrefs.GetFloat("SpawnRot.x"), PlayerPrefs.GetFloat("SpawnRot.y"), PlayerPrefs.GetFloat("SpawnRot.z"))));
        go.GetComponent<NetworkObject>().SpawnAsPlayerObject(id);
        go.GetComponent<NetworkObject>().ChangeOwnership(id);
    }
    

}
