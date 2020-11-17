﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{   
    public float rotationRate = 360;
    
    private PlayerActionControls playerActionControls;
    Animator anim;
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
    {
        anim = gameObject.GetComponent<Animator>();
    }
    private void ApplyInput(float moveInput, float turnInput){
        Move(moveInput);
        Turn(turnInput);
    }
    private void Move(float input){
        anim.SetFloat("Mag",input);
    }
    private void Turn(float input){
        transform.Rotate(0,input*rotationRate*Time.deltaTime,0);
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 movementInput = playerActionControls.Vehicle.Move.ReadValue<Vector2>();
        float l = movementInput[1];
        float n = movementInput[0];
        ApplyInput(l,n);
    }
}