using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using MLAPI.Connection;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class LoadVehicle : NetworkBehaviour
{   
    private List<GameObject> Assets { get; } = new List<GameObject>();
    private ulong id;
    private GameObject prefab;
    private string Vehicletype;
    // Start is called before the first frame update
    void Start()
    {   
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name=="Bangalore" || scene.name=="VehicleLicense"){
        id =  NetworkManager.Singleton.LocalClientId;
        Vehicletype = VehicleID.Vehicle;
        
        
        SpawnServerRpc(id, Vehicletype);
        if(IsLocalPlayer){
            Camera.main.GetComponent<CameraFollowController>().enabled = true;
            GameObject.Find("fuel").GetComponent<fuelreader>().enabled = true;
            GameObject.Find("rpm").GetComponent<rpmreader>().enabled = true;
            ChunkingV2.directionquadINIT = (int)((180f -45f)/90f);
            }
        }
    }
    private void Gamehandle_Completed(AsyncOperationHandle<GameObject> handle) {
    if (handle.Status == AsyncOperationStatus.Succeeded) {
        prefab = handle.Result;
        GameObject go = Instantiate(prefab, new Vector3(PlayerPrefs.GetFloat("SpawnLoc.x"), PlayerPrefs.GetFloat("SpawnLoc.y"), PlayerPrefs.GetFloat("SpawnLoc.z")), Quaternion.Euler(new Vector3(PlayerPrefs.GetFloat("SpawnRot.x"), PlayerPrefs.GetFloat("SpawnRot.y"), PlayerPrefs.GetFloat("SpawnRot.z"))));
        go.GetComponent<NetworkObject>().SpawnAsPlayerObject(id);
        go.GetComponent<NetworkObject>().ChangeOwnership(id);
        Debug.Log(go.name);
        
        
    }
        // The texture is ready for use.
    
}
  
    [ServerRpc]
    public void SpawnServerRpc(ulong id, string Vehicletype){
        var request = Addressables.LoadAssetAsync<GameObject>("Vehicles_prefabs/" + Vehicletype);
        request.Completed += Gamehandle_Completed;
        
        GetComponent<NetworkObject>().Despawn();
        Destroy(gameObject);
        //bool spawned = false;
        //GameObject prefab = Resources.Load("Vehicles_prefabs/" + Vehicletype) as GameObject; 
        //Addressables.InstantiateAsync("Vehicles_prefabs/" + Vehicletype, new Vector3(PlayerPrefs.GetFloat("SpawnLoc.x"), PlayerPrefs.GetFloat("SpawnLoc.y"), PlayerPrefs.GetFloat("SpawnLoc.z")), Quaternion.Euler(new Vector3(PlayerPrefs.GetFloat("SpawnRot.x"), PlayerPrefs.GetFloat("SpawnRot.y"), PlayerPrefs.GetFloat("SpawnRot.z"))));
        
            
            
            //CreateAddressablesLoader.InitByNameOrLabel("Vehicles_prefabs/" + Vehicletype, Assets, new Vector3(PlayerPrefs.GetFloat("SpawnLoc.x"), PlayerPrefs.GetFloat("SpawnLoc.y"), PlayerPrefs.GetFloat("SpawnLoc.z")), Quaternion.Euler(new Vector3(PlayerPrefs.GetFloat("SpawnRot.x"), PlayerPrefs.GetFloat("SpawnRot.y"), PlayerPrefs.GetFloat("SpawnRot.z"))));
        
            /*GameObject go = Assets[0].gameObject
            
                go.GetComponent<NetworkObject>().SpawnAsPlayerObject(id);
                go.GetComponent<NetworkObject>().ChangeOwnership(id);
                go.GetComponent<VehicleINIT>().enabled = true;
                CameraFollowController.objectToFollow = go.transform;
                fuelreader.GO = go;
                go.transform.GetChild(0).gameObject.GetComponent<Clamp>().enabled=true;*/
            
                
             
                
        //StartCoroutine(CreateAdrresableSpawner(id, Vehicletype));

        
    }
    

}
