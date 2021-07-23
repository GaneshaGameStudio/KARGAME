using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using UnityEngine.Animations.Rigging;
using TMPro;
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
    public float maxhealth = 2f;
    public float damage;
    public GameObject weapon;
    public int Kit;
    NetworkVariableFloat health = new NetworkVariableFloat(2f);
    public LayerMask groundmask;
    bool isGrounded;
    public Transform groundcheck;
    public float grounddistance = 0.4f;
    private CharacterController charac;
    Vector3 velocity;
    public float gravity = -9.81f;
    
    
    // Start is called before the first frame update
    
    private void Awake(){
        playerActionControls = new PlayerActionControls();
        health.Value = maxhealth;
        actualhealth = health.Value;
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
            int MoneyP = PlayerPrefs.GetInt("MoneyPocket");
            int MoneyB = PlayerPrefs.GetInt("MoneyBank");
            MoneyP = 2000-MoneyP;
            MoneyB = MoneyB - MoneyP;
            PlayerPrefs.SetInt("MoneyPocket", 2000);
            PlayerPrefs.SetInt("MoneyBank", MoneyB);
            GameObject.Find("Money-number").GetComponent<TextMeshProUGUI>().SetText((PlayerPrefs.GetInt("MoneyPocket")).ToString());
            charac = GetComponent<CharacterController>();
            fuelreader.GO = gameObject;
            fuelreader.TC = tankcap;
            fuelreader.RF = FR;
            fuelreader.M = mileage;
            fuelreader.isKhali = false;
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
            //transform.position += transform.forward * Time.deltaTime * animspeed * (input + (wheelieInput*0.5f));
            Vector3 move = transform.forward * (input + (wheelieInput*0.5f));
            charac.Move(move * animspeed * Time.deltaTime);
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
        if(actualhealth<=0f){
             //turn off character control
                gameObject.GetComponent<CharacterController>().enabled = false;
                //turn on root motion
                anim.applyRootMotion = true;
                //turn off animator
                anim.enabled = false;
                weapon.GetComponent<BoxCollider>().isTrigger = false;
                weapon.GetComponent<Rigidbody>().isKinematic = false;
                weapon.GetComponent<Rigidbody>().useGravity = true;

                Rigidbody[] allRBs = GetComponentsInChildren<Rigidbody>();
            for (int r=0;r<allRBs.Length;r++) {
                //allRBs[r].isKinematic = true;
                allRBs[r].useGravity = true;
            }
            Collider[] allCCs = GetComponentsInChildren<Collider>();
            for (int r=0;r<allCCs.Length;r++) {
                      allCCs[r].enabled = true;
                }
                if(IsLocalPlayer){
                    health.Value = 0;
                }
            
    }
    }
    private IEnumerator Coffin(){
        
        yield return new WaitForSeconds(1.5f);
        
        if(!IsLocalPlayer){
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            for (int i=5;i<=5+Kit;i++){
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }

    }
    [ServerRpc]
    void ShowCoffinServerRpc(){
        
       StartCoroutine("Coffin");

    }
    [ClientRpc]
    void ShowCoffinClientRpc(){
       
        StartCoroutine("Coffin");
    }
    [ServerRpc]
    public void TakeDamageServerRpc(float damage){
       
            health.Value -=damage;  
            
    }
    private void OnTriggerEnter(Collider collision){

            if(IsLocalPlayer){
                if(collision.gameObject.tag=="Weapon" && collision.transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer==false){
                    
                    TakeDamageServerRpc(damage);
                    if(health.Value <= 0f){
                        ///gameObject.transform.GetChild(4).gameObject.SetActive(true);
                        GameObject.Find("Money-number").GetComponent<TextMeshProUGUI>().SetText((0).ToString());
                        PlayerPrefs.SetInt("MoneyPocket",0);
                        Chat.isCrash = true;
                        transform.GetChild(3).gameObject.SetActive(true);
                        ShowCoffinServerRpc();
                        ShowCoffinClientRpc();
                    }
                    else{
                        Chat.Life=Mathf.CeilToInt((Mathf.Max(0f,actualhealth))); 
                        try{
                             GameObject.Find("Life"+Chat.Life.ToString()).SetActive(false);
                        }
                        catch{

                        }
                        
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
            isGrounded = Physics.CheckSphere(groundcheck.position,grounddistance,groundmask);
            if(isGrounded && velocity.y < 0f){
            velocity.y = -2f;
            }
            velocity.y += gravity * Time.deltaTime;
            charac.Move(velocity * Time.deltaTime);
        }
        actualhealth = health.Value;
    }
}
