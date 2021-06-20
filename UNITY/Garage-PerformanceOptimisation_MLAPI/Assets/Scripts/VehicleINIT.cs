using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleINIT : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {   
        
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
