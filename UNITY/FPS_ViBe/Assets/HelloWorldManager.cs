
using MLAPI;
using UnityEngine;


public class HelloWorldManager : MonoBehaviour
    {
        void Start(){
            Camera.main.GetComponent<CameraFollowController>().enabled = false;
        }
        public void SH(){
            Camera.main.GetComponent<CameraFollowController>().enabled = true;
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
