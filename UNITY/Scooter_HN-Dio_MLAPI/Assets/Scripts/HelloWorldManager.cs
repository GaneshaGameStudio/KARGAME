
using MLAPI;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class HelloWorldManager : MonoBehaviour
    {
        void Start(){
            Camera.main.GetComponent<CameraFollowController>().enabled = false;
        }
        public void SH(){
            Camera.main.GetComponent<CameraFollowController>().enabled = true;
             
            //AssetReference m_LogPrefab = Addressables.LoadAssetAsync<GameObject>("HN-Dio_stock.prefab");;
            //Addressables.InstantiateAsync("HN-Dio_stock.prefab");
            //CameraFollowController.objectToFollow = spawn.transform;
            NetworkManager.Singleton.StartHost();
        }
        public void SC(){
            Camera.main.GetComponent<CameraFollowController>().enabled = true;
            NetworkManager.Singleton.StartClient();
        }
        public void SS(){
            Camera.main.GetComponent<CameraFollowController>().enabled = false;
            NetworkManager.Singleton.StartServer();
        }

       

       
    }
