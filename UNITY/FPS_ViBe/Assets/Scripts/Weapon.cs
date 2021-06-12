using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
public class Weapon : NetworkBehaviour
{   
    public Vector3 Weaponlocation;
    public Vector3 Weaponrotation;
    public Vector3 Weapondeflocation;
    public Vector3 Weapondefrotation;
    
    private PlayerActionControls playerActionControls;
    public Animator anim;
    public GameObject Holder;
    public GameObject Def;
    public GameObject release;
    private ParticleSystem ps;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerActionControls = new PlayerActionControls();
        //anim = gameObject.GetComponent<Animator>();
    }
    private void OnEnable(){
        playerActionControls.Enable();
        release.SetActive(false);
        ps = GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.enabled = false;
      
        
    }
    void DrawWeapon(){
        float drawweapon = playerActionControls.Vehicle.Draw.ReadValue<float>();
        if(drawweapon>0f){
            var emission = ps.emission;
            emission.enabled = true;
            anim.SetBool("WeaponDraw",true);
            release.SetActive(false);
            ReleaseServerRpc(false);
            ReleaseClientRpc(false);
        }
        else{
            var emission = ps.emission;
            anim.SetBool("WeaponDraw",false);
            release.SetActive(true);
            emission.enabled = false;
            ReleaseServerRpc(true);
            ReleaseClientRpc(true);
        }
        
    }
    [ServerRpc]
    void ReleaseServerRpc(bool status){
        release.SetActive(status);
    }
    [ClientRpc]
    void ReleaseClientRpc(bool status){
        release.SetActive(status);
    }
    void Strike(){
        float strikeweapon = playerActionControls.Vehicle.Strike.ReadValue<float>();
         if(strikeweapon>0f){
            
            anim.SetBool("Strike",true);
          
        }
        else{
            anim.SetBool("Strike",false);
            
        }
         

    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other){
      
          if(other.gameObject.name == "Equip"){
              
                gameObject.transform.parent=Holder.transform;
                gameObject.transform.localPosition = Weaponlocation;
                gameObject.transform.localEulerAngles = Weaponrotation;
                if(IsLocalPlayer){
                     Camera.main.GetComponent<CameraFollowController>().offset = new Vector3(0.83f,1.81f,-3.33f);
                }
               
                
            }
        if(other.gameObject.name == "Release" && anim.GetBool("WeaponDraw")==false){
              
                gameObject.transform.parent=Def.transform;
                gameObject.transform.localPosition = Weapondeflocation;
                gameObject.transform.localEulerAngles = Weapondefrotation;
                if(IsLocalPlayer){
                    Camera.main.GetComponent<CameraFollowController>().offset = new Vector3(0.0f,2.66f,-3.8f);
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
