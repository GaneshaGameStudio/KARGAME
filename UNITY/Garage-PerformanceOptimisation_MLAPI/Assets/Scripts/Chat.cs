using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI.Messaging;
using MLAPI;
using TMPro;

public class Chat : NetworkBehaviour
{   
    public static bool isCrash = false;
    public bool isKhalicheck = false;
    public bool isLicensetwoWheelercheck = false;
    public bool isDisplayMessage = false;
    public static int Life;
    // Start is called before the first frame update
    void Start()
    {
        Life = 2;
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Traffic" ){
            if(other.impulse.magnitude / Time.fixedDeltaTime > 6000){
                if(Life <= 0){
                    GameObject.Find("Life"+Life.ToString()).SetActive(false);
                    Life=Life-1;
                    isCrash = true;
                    transform.Find("LogoAPPchatanim").gameObject.SetActive(true);
                    if(transform.root.gameObject.tag!="Manushaya"){
                        if(transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer){
                        GameObject.Find("Money-number").GetComponent<TextMeshProUGUI>().SetText((0).ToString());
                        PlayerPrefs.SetInt("MoneyPocket",0);
                        ShowCoffinServerRpc();
                        ShowCoffinClientRpc();
                        }
                       
                    }
                }
                else{
                    GameObject.Find("Life"+Life.ToString()).SetActive(false);
                    Life=Life-1;
                    
                }
                
            
            }
        }
    }
    private IEnumerator CoffinVehicle(){
        yield return new WaitForSeconds(1.5f);
        if(!IsLocalPlayer){
            for(int i =0;i<transform.childCount-1;i++){
                //Destroy(gameObject.transform.root.GetChild(i).gameObject);
                gameObject.transform.root.GetChild(i).gameObject.SetActive(false);
            }
            Destroy(gameObject.transform.root.GetComponent<Rigidbody>());
            gameObject.transform.root.GetChild(transform.root.childCount-1).gameObject.SetActive(true);
            
        }
    }
    [ServerRpc]
    void ShowCoffinServerRpc(){
       StartCoroutine("CoffinVehicle");

    }
    [ClientRpc]
    void ShowCoffinClientRpc(){
        StartCoroutine("CoffinVehicle");
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "License" )
        {
            isLicensetwoWheelercheck = true;
            transform.Find("LogoAPPchatanim").gameObject.SetActive(true);
        }
        else if(other.tag == "Finish"){
            isDisplayMessage = true;
            transform.Find("LogoAPPchatanim").gameObject.SetActive(true);
        }
    }
}
