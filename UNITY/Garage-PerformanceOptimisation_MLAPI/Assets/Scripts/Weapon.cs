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
    public WheelCollider rearweapon;
    
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
        anim.SetBool("WeaponDraw",false);
        var emission = ps1.emission;
        emission.enabled = false;
        emission = ps2.emission;
        emission.enabled = false;
        gameObject.transform.localPosition = Weapondeflocation;
        gameObject.transform.localEulerAngles = Weapondefrotation;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if(transform.root.gameObject.tag =="Manushya"){
            Holder.transform.GetChild(1).GetComponent<Collider>().enabled = true;
        }
        else{
            
            //Holder.transform.GetChild(1).GetComponent<Collider>().enabled = false;
        }
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
                //if(transform.root.gameObject.tag !="Manushya")
                //   Holder.transform.GetChild(1).GetComponent<Collider>().enabled = false;
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
        //if(transform.root.gameObject.tag !="Manushya")
        //Holder.transform.GetChild(1).GetComponent<Collider>().enabled = status;

        
    }
    [ClientRpc]
    void ReleaseClientRpc(bool status){
        release.SetActive(status);
        Rigger.layers[1].active = false;
        //release.GetComponent<Collider>().enabled = status;
        //if(transform.root.gameObject.tag !="Manushya")
        //Holder.transform.GetChild(1).GetComponent<Collider>().enabled = status;
       
        
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
           if(other.gameObject.name == "Equip"&& anim.GetBool("WeaponDraw")==true){
                gameObject.transform.parent=Holder.transform;
                gameObject.transform.localPosition = Weaponlocation;
                gameObject.transform.localEulerAngles = Weaponrotation;
                
            }
            if(other.gameObject.name == "Release" && anim.GetBool("WeaponDraw")==false){
                
                gameObject.transform.parent=Def.transform;
                gameObject.transform.localPosition = Weapondeflocation;
                gameObject.transform.localEulerAngles = Weapondefrotation;
                Rigger.layers[1].active = true;
                
                
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
