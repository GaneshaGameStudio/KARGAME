using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using MLAPI.Connection;
public class Building : NetworkBehaviour
{   
    private string Vehicletemp;
    private GameObject GOO;
    private Vector3 buildingcoord;
    private Vector3 buildingrot;
    private float deg2rad = 0.01745311f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider building)
    {   
        if(building.transform.parent){
             if(building.transform.parent.tag=="2Wheeler"){
            print("buildingentered");
            GOO = GameObject.Find(VehicleID.Vehicle + "(Clone)");
            buildingcoord = GOO.transform.position;
            buildingrot = GOO.transform.localRotation.eulerAngles;
            Vehicletemp = VehicleID.Vehicle;
            Destroy(GOO);
            VehicleID.Vehicle = "Vibe2009rig-redoCSY";
            //Instantiate(Resources.Load("Vehicles_prefabs/Vibe2009rig-redoCSY"), new Vector3(buildingcoord.x + 1.0f*Mathf.Sin(deg2rad*buildingrot.y),buildingcoord.y ,buildingcoord.z+ 1.0f*Mathf.Cos(deg2rad*buildingrot.y)), Quaternion.Euler(new Vector3(0f,buildingrot.y,0f)));
            ulong id = NetworkManager.Singleton.LocalClientId;
            SpawnServerRpc(id, VehicleID.Vehicle,buildingcoord.x + 1.0f*Mathf.Sin(deg2rad*buildingrot.y), buildingcoord.y ,buildingcoord.z+ 1.0f*Mathf.Cos(deg2rad*buildingrot.y), buildingrot.y);
            //GOO = GameObject.Find(VehicleID.Vehicle + "(Clone)");
            //CameraFollowController.objectToFollow = GOO.transform;
            }
        }
       
        
    }
    private void OnTriggerExit(Collider building)
    {  
        if(building.transform.parent){
            if(building.transform.parent.tag=="Manushya"){
            print("buildingexit");
            GOO = GameObject.Find(VehicleID.Vehicle + "(Clone)");
            buildingcoord = GOO.transform.position;
            buildingrot = GOO.transform.localRotation.eulerAngles;
            VehicleID.Vehicle = Vehicletemp;
            Destroy(GOO);
            Instantiate(Resources.Load("Vehicles_prefabs/"+VehicleID.Vehicle), new Vector3(buildingcoord.x + 1.0f*Mathf.Sin(deg2rad*buildingrot.y),buildingcoord.y ,buildingcoord.z+ 1.0f*Mathf.Cos(deg2rad*buildingrot.y)), Quaternion.Euler(new Vector3(0f,buildingrot.y,0f)));
            GOO = GameObject.Find(VehicleID.Vehicle + "(Clone)");
            CameraFollowController.objectToFollow = GOO.transform;
            }
        }
        

    }
    [ServerRpc]
    public void SpawnServerRpc(ulong id, string Vehicletype,float x, float y, float z, float yrot){
        GameObject prefab = Resources.Load("Vehicles_prefabs/" + Vehicletype) as GameObject; 
        GameObject go = Instantiate(prefab, new Vector3(x, y, z), Quaternion.Euler(new Vector3(0f, yrot, 0f)));
        

        go.GetComponent<NetworkObject>().SpawnAsPlayerObject(id);
        go.GetComponent<NetworkObject>().ChangeOwnership(id);
        //GetComponent<NetworkObject>().Despawn();
        //Destroy(gameObject);

        
    }

}
