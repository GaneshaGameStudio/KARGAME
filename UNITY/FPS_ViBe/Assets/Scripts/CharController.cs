using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
public class CharController : NetworkBehaviour
{   
    public float rotationRate = 360;
    NetworkVariableFloat health = new NetworkVariableFloat(100f);
    private PlayerActionControls playerActionControls;
    Animator anim;
    public float actualhealth;
    
    
    // Start is called before the first frame update
    
    private void Awake(){
        playerActionControls = new PlayerActionControls();
        
    }
    
    
    private void OnEnable(){
        playerActionControls.Enable();
        
    }
    private void OnDisable(){
        playerActionControls.Disable();
    }
    void Start()
    {if(IsLocalPlayer){
            Camera.main.GetComponent<CameraFollowController>().objectToFollow = gameObject.transform;
        }
        anim = gameObject.GetComponent<Animator>();
    }
    private void ApplyInput(float moveInput, float turnInput, float wheelieInput){
        Move(moveInput, wheelieInput);
        Turn(turnInput);
    }
    
    private void Move(float input, float wheelieInput){
        //print(transform.eulerAngles);
        float animspeed = 0f;
        if(input!=0){
            anim.SetBool("Walk",true);
            if(anim.GetBool("WeaponDraw")==true){
                animspeed = 0.2f;
            }
            else{
                animspeed=1.0f;
            }
            
            anim.SetFloat("Mag",input + (wheelieInput*0.5f));
            transform.position += transform.forward * Time.deltaTime * animspeed * (input + (wheelieInput*0.5f));
        }
        else{
            anim.SetBool("Walk",false);
        }
        
        
        
    }
    private void Turn(float input){
        transform.Rotate(0,input*rotationRate*Time.deltaTime,0);
    }
    private void MovePlayer(){
        Vector2 movementInput = playerActionControls.Vehicle.Move.ReadValue<Vector2>();
        float wheelieInput = playerActionControls.Vehicle.Wheelie.ReadValue<float>();

        float l = movementInput[1];
        float n = movementInput[0];
        ApplyInput(l,n,wheelieInput);
    }
    public void Fall(){
        if(health.Value<0){
                //turn off character control
                gameObject.GetComponent<CharacterController>().enabled = false;
                //turn on root motion
                anim.applyRootMotion = true;
                //turn off animator
                anim.enabled = false;
                gameObject.transform.Find("Weapon").GetComponent<BoxCollider>().isTrigger = false;
                gameObject.transform.Find("Weapon").GetComponent<Rigidbody>().isKinematic = false;
                gameObject.transform.Find("Weapon").GetComponent<Rigidbody>().useGravity = true;
                
            }
    }
    [ServerRpc]
    public void TakeDamageServerRpc(float damage){
       
            health.Value -=damage;
            
        
    }
    private void OnTriggerEnter(Collider collision){
        
            if(IsLocalPlayer){
            if(collision.gameObject.tag=="Weapon" && collision.transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer==false){
                    
                    TakeDamageServerRpc(5f);
                    
                }
            }  
            Fall();
    }
  
    // Update is called once per frame
    void Update()
    {   
        if(IsLocalPlayer){
            MovePlayer();
        }
        actualhealth = health.Value;
    }
}
