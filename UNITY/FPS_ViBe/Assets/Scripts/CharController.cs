using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

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
        anim.SetFloat("Mag",input + (wheelieInput*0.5f));
        
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
    public void TakeDamage(float damage){
       
            health.Value -=damage;
        
    }
    private void OnTriggerEnter(Collider collision){
        
            if(IsLocalPlayer){
            if(collision.gameObject.tag=="Weapon" && collision.transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer==false){
                    
                    print("damaged");
                    TakeDamage(10f);
                    
                }
            }  
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
