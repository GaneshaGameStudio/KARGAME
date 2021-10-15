using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using TMPro;

public class LoadedAddressableLocation : MonoBehaviour
{
    [SerializeField] private string _label;
    public TextMeshProUGUI progresstext;
    
    public IList<IResourceLocation> AssetLocations { get; } = new List<IResourceLocation>();

    private void Start()
    {
        Caching.ClearCache();
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
            AsyncOperationHandle downloadDependencies = Addressables.DownloadDependenciesAsync(location.PrimaryKey);
            StartCoroutine(OnLoad(Addressables.DownloadDependenciesAsync(location.PrimaryKey)));
            //AsyncOperationHandle<GameObject> loadOp = Addressables.LoadAssetAsync<GameObject>(location.PrimaryKey);
            Debug.Log(location.PrimaryKey); 
        }
    }

    IEnumerator OnLoad(AsyncOperationHandle obj)
      {
          while (obj.IsDone == false)
          {
              yield return new WaitForSeconds(.01f);
              //progressImg.fillAmount = obj.GetDownloadStatus().Percent;
              progresstext.text = obj.GetDownloadStatus().Percent * 100f + "%".ToString();
              //print(obj.GetDownloadStatus().Percent * 100f + "%".ToString());
          }
          
      }
}
