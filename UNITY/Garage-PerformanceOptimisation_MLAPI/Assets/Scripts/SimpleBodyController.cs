using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using UnityEngine.Animations.Rigging;
public class SimpleBodyController: NetworkBehaviour
{   
    public float rotationRate = 360;
    
    private PlayerActionControls playerActionControls;
    Animator anim;
    public float actualhealth;
    public float tankcap;
    public float mileage;
	public static float remainingfuel;
    public float FR = 1.0f;
    public float maxhealth = 100f;
    NetworkVariableFloat health = new NetworkVariableFloat(100f);
    
    
    // Start is called before the first frame update
    
    private void Awake(){
        playerActionControls = new PlayerActionControls();
        health.Value = maxhealth;
    }
    
    
    private void OnEnable(){
        playerActionControls.Enable();
        
        
    }
    private void OnDisable(){
        playerActionControls.Disable();
    }
    void Start()
    {if(IsLocalPlayer){
            remainingfuel = FR;
            CameraFollowController.objectToFollow = gameObject.transform;
            
        }
        anim = gameObject.GetComponent<Animator>();
        
    }
    private void ApplyInput(float moveInput, float turnInput, float wheelieInput){
        Move(moveInput, wheelieInput);
        Turn(turnInput);
    }
    
    private void Move(float input, float wheelieInput){

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
            if(!IsLocalPlayer){

                //turn off character control
                //gameObject.GetComponent<CharacterController>().enabled = false;
                //turn on root motion
                anim.applyRootMotion = true;
                //turn off animator
                anim.enabled = false;
                gameObject.transform.Find("Weapon").GetComponent<BoxCollider>().isTrigger = false;
                gameObject.transform.Find("Weapon").GetComponent<Rigidbody>().isKinematic = false;
                gameObject.transform.Find("Weapon").GetComponent<Rigidbody>().useGravity = true;

                Rigidbody[] allRBs = GetComponentsInChildren<Rigidbody>();
            for (int r=0;r<allRBs.Length;r++) {
                //allRBs[r].isKinematic = true;
                allRBs[r].useGravity = true;
            }
            Collider[] allCCs = GetComponentsInChildren<Collider>();
            for (int r=0;r<allCCs.Length;r++) {
                      allCCs[r].enabled = true;
                }
            }
            else{
            Chat.Life = 0;
            transform.GetChild(3).gameObject.SetActive(true);
            Chat.isCrash = true;
            GameObject.Find("Life"+Chat.Life.ToString()).SetActive(false);
            }
            

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
                    if(Chat.Life <= 0){
                        //GameObject.Find("Life"+Chat.Life.ToString()).SetActive(false);
                        //Chat.Life=Chat.Life-1;
                        Chat.isCrash = true;
                        transform.GetChild(3).gameObject.SetActive(true);
                    }
                    else{
                        GameObject.Find("Life"+Chat.Life.ToString()).SetActive(false);
                    }
                    
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
        Chat.Life=(int)Mathf.Min(2f,(maxhealth/(Mathf.Max(maxhealth-health.Value,1f))));
        
    }
}
