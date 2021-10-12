using MLAPI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using MLAPI.Transports.UNET;

public class HelloWorldManager : MonoBehaviour
    {
        void Start(){
            Camera.main.GetComponent<CameraFollowController>().enabled = false;
            Camera.main.GetComponent<ChunkingV2>().enabled = false;
            Camera.main.GetComponent<TrafficPool>().enabled = false;
            GameObject.Find("fuel").GetComponent<fuelreader>().enabled = false;
            GameObject.Find("rpm").GetComponent<rpmreader>().enabled = false;
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name=="VehicleLicense"){
                NetworkManager.Singleton.StartHost();
            }
            //SS();
        }
        public void SH(){
            GameObject.Find("SelectNet").SetActive(false);
            Camera.main.GetComponent<ChunkingV2>().enabled = true;
            Camera.main.GetComponent<TrafficPool>().enabled = true;
            NetworkManager.Singleton.StartHost();
        }
        public void SC(){
            string IPText = GameObject.Find("TextIP").GetComponent<TextMeshProUGUI>().text;
            // Debug.Log(IPText);
            if(IPText!=""){
                //Set this IP to Network Manager IP
                string mTransport = MLAPI.NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress;
                if (mTransport != null)
                {
                    Debug.Log(mTransport);
                    MLAPI.NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = IPText;
                    Debug.Log(MLAPI.NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress);
                }
            }

            GameObject.Find("SelectNet").SetActive(false);
            Camera.main.GetComponent<ChunkingV2>().enabled = true;
            Camera.main.GetComponent<TrafficPool>().enabled = true;
            NetworkManager.Singleton.StartClient();
        }
        public void SS(){
            // Camera.main.GetComponent<CameraFollowController>().enabled = false;
            NetworkManager.Singleton.StartServer();
        }

       

       
    }