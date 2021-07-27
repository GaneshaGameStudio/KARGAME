using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using UnityEngine.SceneManagement;
using MLAPI.Messaging;
using MLAPI.Connection;

public class VehicleINIT : NetworkBehaviour
{   
    public string kit;
    public string mat;
    public NetworkVariableString teamKit = new NetworkVariableString("Stock");
    public NetworkVariableString teamMat = new NetworkVariableString();
  
    // Start is called before the first frame update
   
    void Awake()
    {   
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name!="Bangalore"){
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {   
                
                if(gameObject.transform.GetChild(i).name == PlayerPrefs.GetString(gameObject.name.Replace("(Clone)","").Trim() + "_KIT")){
                    gameObject.transform.GetChild(i).gameObject.SetActive(true);
                    Texture2D[] Tex = gameObject.transform.GetChild(i).gameObject.GetComponent<TexturesCollect>().TexturesCollection;
                    for(int j=0;j<Tex.Length;j++){
                            if(Tex[j].name == PlayerPrefs.GetString(gameObject.name + "_MAT")){
                                    gameObject.transform.GetChild(i).gameObject.GetComponent<Renderer>().sharedMaterial.SetTexture("_BaseMap",Tex[j]);
                     }
                }
            }
                else{
                    if(gameObject.transform.GetChild(i).tag =="Kit"){
                         gameObject.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    }
            }
        }
    }
    
    void Start(){
        

        kit = PlayerPrefs.GetString(gameObject.name.Replace("(Clone)","").Trim() + "_KIT");
        mat = PlayerPrefs.GetString(gameObject.name.Replace("(Clone)","").Trim()  + "_MAT");
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name =="Bangalore"){
        ulong id=NetworkManager.Singleton.LocalClientId;
        SelectKitServerRpc(id,kit,mat);
        }
        if(IsLocalPlayer){
            KitChange(kit,mat);
        }
        else{
            KitChange(teamKit.Value,teamMat.Value);
        }
        
    }
    private void KitChange(string Kit, string Mat){
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name!="Garage")
        for(int i = 0; i < gameObject.transform.childCount; i++)
            {   
                if(gameObject.transform.GetChild(i).name == Kit){
                    gameObject.transform.GetChild(i).gameObject.SetActive(true);
                    Texture2D[] Tex = gameObject.transform.GetChild(i).gameObject.GetComponent<TexturesCollect>().TexturesCollection;
                    for(int j=0;j<Tex.Length;j++){
                            if(Tex[j].name == Mat){
                                    gameObject.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap",Tex[j]);
                                    gameObject.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap",Tex[j]);
                                    gameObject.transform.GetChild(i).GetChild(1).gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap",Tex[j]);
                                    if(gameObject.tag=="2Wheeler"){
                                        gameObject.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap",Tex[j]);
                                        gameObject.transform.GetChild(i).GetChild(1).GetChild(0).gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap",Tex[j]);
                                    }
                                    else if(gameObject.tag=="4Wheeler" || gameObject.tag=="6Wheeler"){
                                        gameObject.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap",Tex[j]);
                                        gameObject.transform.GetChild(i).GetChild(1).GetChild(0).gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap",Tex[j]);
                                        gameObject.transform.GetChild(i).GetChild(1).GetChild(1).gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap",Tex[j]);
                                        gameObject.transform.GetChild(i).GetChild(1).GetChild(2).gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap",Tex[j]);
                                        gameObject.transform.GetChild(i).GetChild(1).GetChild(3).gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap",Tex[j]);
                                    }
                                    
                                    
                     }
                }
            }
                else{
                    if(gameObject.transform.GetChild(i).tag =="Kit"){
                         gameObject.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    }
            }

    }
    [ServerRpc]
    public void SelectKitServerRpc(ulong id, string kit, string mat){
        teamKit.Value = kit;
        teamMat.Value = mat;
        KitChange(teamKit.Value,teamMat.Value);
    }    
        
}
