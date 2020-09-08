using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoorAnim : MonoBehaviour
{   
    [SerializeField] private Animator animcontroller1;
    [SerializeField] private Animator animcontroller2;

    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MainCamera"))
        {   
            Debug.Log("trigger enter");
            animcontroller1.SetBool("playspin2",true);
            animcontroller2.SetBool("playspin",true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("MainCamera"))
        {   
            Debug.Log("trigger exit");
            animcontroller1.SetBool("playspin2",false);
            animcontroller2.SetBool("playspin",false);
        }
    }
    // Update is called once per frame
}
