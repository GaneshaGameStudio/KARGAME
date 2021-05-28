
using MLAPI;
using UnityEngine;


public class HelloWorldManager : MonoBehaviour
    {
        
        public void SH(){
            NetworkManager.Singleton.StartHost();
        }
        public void SC(){
            NetworkManager.Singleton.StartClient();
        }
        public void SS(){
            NetworkManager.Singleton.StartServer();
        }

       

       
    }
