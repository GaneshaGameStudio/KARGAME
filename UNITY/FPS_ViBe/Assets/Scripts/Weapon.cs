using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
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
    // Start is called before the first frame update
    void Awake()
    {
        playerActionControls = new PlayerActionControls();
        //anim = gameObject.GetComponent<Animator>();
    }
    private void OnEnable(){
        playerActionControls.Enable();
        release.SetActive(false);
        
    }
    void DrawWeapon(){
        float drawweapon = playerActionControls.Vehicle.Draw.ReadValue<float>();
        if(drawweapon>0f){
            anim.SetBool("WeaponDraw",true);
            release.SetActive(false);
        }
        else{
            anim.SetBool("WeaponDraw",false);
            release.SetActive(true);
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other){
      if(IsLocalPlayer){
          if(other.gameObject.name == "Equip"){
              
                gameObject.transform.parent=Holder.transform;
                gameObject.transform.localPosition = Weaponlocation;
                gameObject.transform.localEulerAngles = Weaponrotation;
                Camera.main.GetComponent<CameraFollowController>().offset = new Vector3(0.83f,1.81f,-3.33f);
                
            }
        if(other.gameObject.name == "Release"){
              
                gameObject.transform.parent=Def.transform;
                gameObject.transform.localPosition = Weapondeflocation;
                gameObject.transform.localEulerAngles = Weapondefrotation;
                Camera.main.GetComponent<CameraFollowController>().offset = new Vector3(0.0f,2.66f,-3.8f);
                
            }
            

            
            
        }
    }
    void Update()
    {
        if(IsLocalPlayer){
            DrawWeapon();
        }
    }
}
