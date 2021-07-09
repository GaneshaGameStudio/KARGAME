using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using UnityEngine.Animations.Rigging;
using UnityEngine.Networking;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class Animate : NetworkBehaviour
{
    public Rigidbody rb;
    public GameObject Vehicle;
    Animator anim;
    private PlayerActionControls playerActionControls;
    public float maxhealth = 2f;
    public float damage;
    NetworkVariableFloat health = new NetworkVariableFloat(2f);
    public float actualhealth;
    public GameObject weapon;
    // Start is called before the first frame update
    private void Awake(){
        playerActionControls = new PlayerActionControls();
        health.Value = maxhealth;
        actualhealth = health.Value;
        // Do these things if rider and not walker
    }
    
    void Start()
    {   
        if(IsLocalPlayer){
            anim = gameObject.GetComponent<Animator>();
        }
        
        
    }
    private void OnEnable(){
        playerActionControls.Enable();
    }
    private void OnDisable(){
        playerActionControls.Disable();
    }
    public void Fall(){
        if(health.Value<0){
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
            
            if(!IsLocalPlayer){
                

               
            }
            else{
            PlayerPrefs.SetFloat("SpawnLoc.x",gameObject.transform.position.x);
            PlayerPrefs.SetFloat("SpawnLoc.y",gameObject.transform.position.y);
            PlayerPrefs.SetFloat("SpawnLoc.z",gameObject.transform.position.z);
            
            IEnumerator coroutine;
            coroutine = SpawnCoffin(PlayerPrefs.GetFloat("SpawnLoc.x"),PlayerPrefs.GetFloat("SpawnLoc.y"),PlayerPrefs.GetFloat("SpawnLoc.z"));
            StartCoroutine(coroutine);
            health.Value = 0;
            Chat.isCrash = true;
            transform.GetChild(3).gameObject.SetActive(true);
            GameObject.Find("Life").SetActive(false);
            }

    }
    }
    
    private IEnumerator SpawnCoffin(float x, float y, float z){
        yield return new WaitForSeconds(2f);
        SpawnCoffinServerRpc(x,y,z);

    }
    
    [ServerRpc]
    public void SpawnCoffinServerRpc(float x, float y, float z){
       
        GameObject prefab = Resources.Load("Landmarks_prefabs/Coffin") as GameObject; 
        GameObject go = Instantiate(prefab, new Vector3(x, y, z), Quaternion.Euler(0,0,0));
        go.GetComponent<NetworkObject>().Spawn();
        print("spawned");
        Destroy(gameObject);

    }
    [ServerRpc]
    public void TakeDamageServerRpc(float damage){
       
            health.Value -=damage;  
            
    }
    private void OnTriggerEnter(Collider collision){

            if(IsLocalPlayer){
                if(collision.gameObject.tag=="Weapon" && collision.transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer==false){
                    
                    TakeDamageServerRpc(damage);
                    if(Chat.Life <= 0f){
                        Chat.isCrash = true;
                        transform.GetChild(3).gameObject.SetActive(true);
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
        if(gameObject.transform.root.gameObject.GetComponent<NetworkObject>().IsLocalPlayer){
            float vel = (float)rb.velocity.magnitude*3.6f;
            anim.SetFloat("MagVel",vel);
            Vector2 movementInput = playerActionControls.Vehicle.Move.ReadValue<Vector2>();
            float a = movementInput[0];
            float w = movementInput[1];
            anim.SetFloat("MagDir",a);
            if(a!=0)
            {
                anim.SetBool("keynotPressed",true);
            }
            else
            {
                anim.SetBool("keynotPressed",false);
            }
            var velDir = transform.InverseTransformDirection(rb.velocity);
            if(w<0 && velDir.z < -0.01)
            {
                anim.SetBool("notgoingReverse",false);
            }
            else
            {
                anim.SetBool("notgoingReverse",true);
            }

            anim.SetBool("isWheelie",Vehicle.GetComponent<Lean>().isWheelie);
        
            }
            actualhealth = health.Value;
          
    }
}
