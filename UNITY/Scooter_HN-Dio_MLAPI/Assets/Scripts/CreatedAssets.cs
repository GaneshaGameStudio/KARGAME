using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = System.Object;
using MLAPI;
public class CreatedAssets : MonoBehaviour
{
    [SerializeField] private string _label;
    public string prefabname;
    private List<GameObject> Assets { get; } = new List<GameObject>();

    private void Start()
    {
        CreateAndWaitUntilComplete();
        camon();
    }

    private async Task CreateAndWaitUntilComplete()
    {   
        
        
        await CreateAddressablesLoader.InitByNameOrLabel(prefabname, Assets);

        foreach (var asset in Assets)
        {
            //Asset is now fully loaded
            Debug.Log("Loaded asset: " + asset.name);
        }
        
        //Task.Delay(TimeSpan.FromSeconds(5));

        //CleanUpFinishedAssets(Assets[0]);
        
        CameraFollowController.objectToFollow = Assets[0].transform;
        NetworkManager.Singleton.StartHost();
    }
    private void camon(){
        Camera.main.GetComponent<CameraFollowController>().enabled = true;
    }
    private async Task CleanUpFinishedAssets(Object obj)
    {
        //Addressables.Release(obj);
    }
    
    
}