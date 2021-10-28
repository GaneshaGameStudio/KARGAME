using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class CreateAddressablesLoader

{   public static bool Instafin = false;
     public static async Task InitByNameOrLabel<T>(string assetNameOrLabel, List<T> createdObjs, Vector3 pos, Quaternion rot)
        where T : Object
    {   
        
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(assetNameOrLabel);
        var locations = await Addressables.LoadResourceLocationsAsync(assetNameOrLabel).Task;
        
        await CreateAssetsThenUpdateCollection(locations, createdObjs, pos, rot);
    }
    
    public static async Task IniByLoadedAddress<T>(IList<IResourceLocation> loadedLocations, List<T> createdObjs, Vector3 pos, Quaternion rot)
    where T : Object
    {
        await CreateAssetsThenUpdateCollection(loadedLocations, createdObjs, pos, rot);
    }

    private static async Task CreateAssetsThenUpdateCollection<T>(IList<IResourceLocation> locations, List<T> createdObjs, Vector3 pos, Quaternion rot)
        where T: Object
    {
        foreach (var location in locations){
            createdObjs.Add(await Addressables.InstantiateAsync(location, pos,rot).Task as T);
        }
         Instafin = true;
         
            
    }
   


   
}