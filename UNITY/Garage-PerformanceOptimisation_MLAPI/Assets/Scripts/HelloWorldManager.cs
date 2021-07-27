using MLAPI;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        }
        public void SH(){
            GameObject.Find("SelectNet").SetActive(false);
            Camera.main.GetComponent<ChunkingV2>().enabled = true;
            Camera.main.GetComponent<TrafficPool>().enabled = true;
            NetworkManager.Singleton.StartHost();
        }
        public void SC(){
            GameObject.Find("SelectNet").SetActive(false);
            Camera.main.GetComponent<ChunkingV2>().enabled = true;
            Camera.main.GetComponent<TrafficPool>().enabled = true;
            NetworkManager.Singleton.StartClient();
        }
        public void SS(){
            Camera.main.GetComponent<CameraFollowController>().enabled = false;
            NetworkManager.Singleton.StartServer();
        }

       

       
    }