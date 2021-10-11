using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.SceneManagement;

public class TexturesCollect : NetworkBehaviour
{   
    [SerializeField] public Texture2D[] TexturesCollection;
    // Start is called before the first frame update
    void Start(){
        if(SceneManager.GetActiveScene().name == "Bangalore")
        if(!IsLocalPlayer){
            gameObject.layer = LayerMask.NameToLayer("Player");
            List<Transform> transformList = new List<Transform>();
            gameObject.GetComponentsInChildren<Transform>(transformList);
            for(int i=0;i<=transformList.Count-1;i++){
                transformList[i].gameObject.layer = LayerMask.NameToLayer("Player");
            }
            
            
        }
    }
}
