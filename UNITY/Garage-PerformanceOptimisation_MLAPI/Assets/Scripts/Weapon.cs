using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine.Animations.Rigging;

public class Weapon : NetworkBehaviour
{   
    public Vector3 Weaponlocation;
    public Vector3 Weaponrotation;
    public Vector3 Weapondeflocation;
    public Vector3 Weapondefrotation;
    public RigBuilder Rigger;

    private PlayerActionControls playerActionControls;
    public Animator anim;
    public GameObject Holder;
    public GameObject Def;
    public GameObject release;
    public ParticleSystem ps1;
    public ParticleSystem ps2;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerActionControls = new PlayerActionControls();
        GameObject.Find("Release").GetComponent<Collider>().enabled=true;
        
    }
    private void OnEnable(){
        playerActionControls.Enable();
        release.SetActive(false);
        ps1 = GetComponent<ParticleSystem>();
        var emission = ps1.emission;
        emission.enabled = false;
        emission = ps2.emission;
        emission.enabled = false;
        gameObject.transform.localPosition = Weapondeflocation;
        gameObject.transform.localEulerAngles = Weapondefrotation;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Holder.transform.GetChild(1).GetComponent<Collider>().enabled = false;
        

    }
    void DrawWeapon(){
        float drawweapon = playerActionControls.Vehicle.Draw.ReadValue<float>();
        if(drawweapon>0f || ActivateRight.btactright == true){
            Rigger.layers[1].active = false;
            Holder.transform.GetChild(1).GetComponent<Collider>().enabled = true;
            var emission = ps1.emission;
            emission.enabled = true;
            anim.SetBool("WeaponDraw",true);
            release.SetActive(false);
            ReleaseServerRpc(false);
            ReleaseClientRpc(false);
        }
        else{
            //Rigger.layers[1].active = true;
            anim.SetBool("WeaponDraw",false);
            //release.GetComponent<Collider>().enabled = false;
            var emission = ps1.emission;
            Holder.transform.GetChild(1).GetComponent<Collider>().enabled = false;
            release.SetActive(true);
            emission.enabled = false;
            ReleaseServerRpc(true);
            ReleaseClientRpc(true);
        }
        
    }
    [ServerRpc]
    void ReleaseServerRpc(bool status){
        release.SetActive(status);
        Rigger.layers[1].active = false;
        //release.GetComponent<Collider>().enabled = status;
        Holder.transform.GetChild(1).GetComponent<Collider>().enabled = status;

        
    }
    [ClientRpc]
    void ReleaseClientRpc(bool status){
        release.SetActive(status);
        Rigger.layers[1].active = false;
        //release.GetComponent<Collider>().enabled = status;
        Holder.transform.GetChild(1).GetComponent<Collider>().enabled = status;
       
        
    }
    void Strike(){
        float strikeweapon = playerActionControls.Vehicle.Wheelie.ReadValue<float>();
         if(strikeweapon>0f){
            
            anim.SetBool("Strike",true);
            
          
        }
        else{
            anim.SetBool("Strike",false);
            var emission = ps2.emission;
            emission.enabled = false;
        }
         

    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other){
      
          if(other.gameObject.name == "Equip"){
                gameObject.transform.parent=Holder.transform;
                gameObject.transform.localPosition = Weaponlocation;
                gameObject.transform.localEulerAngles = Weaponrotation;
                if(IsLocalPlayer){
                     //Camera.main.GetComponent<CameraFollowController>().offset = new Vector3(0.83f,1.81f,-3.33f);
                }
               
                
            }
        if(other.gameObject.name == "Release" && anim.GetBool("WeaponDraw")==false){
                
                gameObject.transform.parent=Def.transform;
                gameObject.transform.localPosition = Weapondeflocation;
                gameObject.transform.localEulerAngles = Weapondefrotation;
                Rigger.layers[1].active = true;
                if(IsLocalPlayer){
                    //Camera.main.GetComponent<CameraFollowController>().offset = new Vector3(0.0f,2.66f,-3.8f);
                }
                
                
            }
            if(IsLocalPlayer){
                if(other.gameObject.tag=="Manushya" && other.transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer==false){
                    var emission = ps2.emission;
                    emission.enabled = true;

                 }
            }
            
    }
    void Update()
    {
        if(IsLocalPlayer){
            DrawWeapon();
            Strike();
        }
    }
}
