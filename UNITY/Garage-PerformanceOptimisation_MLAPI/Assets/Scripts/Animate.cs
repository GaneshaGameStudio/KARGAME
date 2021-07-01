using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.Prototyping;
using UnityEngine.Animations.Rigging;

public class Animate : NetworkBehaviour
{
    public Rigidbody rb;
    public GameObject Vehicle;
    Animator anim;
    private PlayerActionControls playerActionControls;
    // Start is called before the first frame update
    private void Awake(){
        playerActionControls = new PlayerActionControls();
        // Do these things if rider and not walker
        CheckRider();
    }
    void CheckRider(){
        if(gameObject.transform.root.tag!="Manushya"){
            Animator anim = gameObject.GetComponent<Animator>();
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Rider");
            gameObject.GetComponent<SimpleBodyController>().enabled = false;
            gameObject.GetComponent<CharacterController>().enabled = false;
            Destroy(gameObject.GetComponent<NetworkObject>());
            gameObject.GetComponent<NetworkTransform>().enabled = false;
            gameObject.GetComponent<Chat>().enabled = false;
            Component[] capsulecolliders;

            capsulecolliders = GetComponents(typeof(CapsuleCollider));

            foreach (CapsuleCollider cc in capsulecolliders)
                cc.enabled = false;
            
            gameObject.transform.Find("Sphere").gameObject.SetActive(false);
            Rigidbody[] allRBs = GetComponentsInChildren<Rigidbody>();
            for (int r=0;r<allRBs.Length;r++) {
                allRBs[r].isKinematic = false;
                allRBs[r].useGravity = false;
            }
            Collider[] allCCs = GetComponentsInChildren<Collider>();
            for (int r=0;r<allCCs.Length;r++) {
                      allCCs[r].enabled = false;
                }
            gameObject.GetComponent<RigBuilder>().enabled=true;
        }
        
    }
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    private void OnEnable(){
        playerActionControls.Enable();
    }
    private void OnDisable(){
        playerActionControls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
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
}
