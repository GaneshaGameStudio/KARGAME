using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;

public class Animate : NetworkBehaviour
{
    public Rigidbody rb;
    Animator anim;
    private PlayerActionControls playerActionControls;
    // Start is called before the first frame update
    private void Awake(){
        playerActionControls = new PlayerActionControls();
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
    {if(IsLocalPlayer){
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

        anim.SetBool("isWheelie",GameObject.Find("HN-Dio_stock(Clone)").GetComponent<Lean>().isWheelie);
    }
        
       
    }
}
