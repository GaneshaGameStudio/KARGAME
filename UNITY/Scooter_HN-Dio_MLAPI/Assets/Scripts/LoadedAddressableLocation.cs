using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

public class LoadedAddressableLocation : MonoBehaviour
{
    [SerializeField] private string _label;
    
    public IList<IResourceLocation> AssetLocations { get; } = new List<IResourceLocation>();

    private void Start()
    {
        
    }
    public void LoadAssets(string _label){
        InitAndWaitUntilLoaded(_label);
    }
    public async Task InitAndWaitUntilLoaded(string label)
    {
        await AddressableLocationLoader.GetAll(label, AssetLocations);

        foreach (var location in AssetLocations)
        {
            //ASSETS ARE FULLY LOADED
            //PERFORM ADDITIONAL OPERATIONS HERE
            AsyncOperationHandle<GameObject> loadOp = Addressables.LoadAssetAsync<GameObject>(location.PrimaryKey);
            Debug.Log(location.PrimaryKey); 
        }
    }
}
