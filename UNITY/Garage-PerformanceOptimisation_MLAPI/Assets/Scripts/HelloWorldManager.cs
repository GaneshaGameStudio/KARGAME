using MLAPI;
using UnityEngine;


public class HelloWorldManager : MonoBehaviour
    {
        void Start(){
            Camera.main.GetComponent<CameraFollowController>().enabled = false;
            Camera.main.GetComponent<ChunkingV2>().enabled = false;
            Camera.main.GetComponent<TrafficPool>().enabled = false;
            Camera.main.GetComponent<LoadVehicle>().enabled = false;
            GameObject.Find("fuel").GetComponent<fuelreader>().enabled = false;
            GameObject.Find("rpm").GetComponent<rpmreader>().enabled = false;
        }
        public void SH(){
            GameObject.Find("SelectNet").SetActive(false);
            Camera.main.GetComponent<CameraFollowController>().enabled = true;
            Camera.main.GetComponent<LoadVehicle>().enabled = true;
            Camera.main.GetComponent<ChunkingV2>().enabled = true;
            Camera.main.GetComponent<TrafficPool>().enabled = true;
            GameObject.Find("fuel").GetComponent<fuelreader>().enabled = true;
            GameObject.Find("rpm").GetComponent<rpmreader>().enabled = true;
            NetworkManager.Singleton.StartHost();
        }
        public void SC(){
            GameObject.Find("SelectNet").SetActive(false);
            Camera.main.GetComponent<CameraFollowController>().enabled = true;
            NetworkManager.Singleton.StartClient();
        }
        public void SS(){
            Camera.main.GetComponent<CameraFollowController>().enabled = false;
            NetworkManager.Singleton.StartServer();
        }

       

       
    }