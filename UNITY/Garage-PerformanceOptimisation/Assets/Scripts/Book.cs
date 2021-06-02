using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(other.tag=="MainCamera"){
            gameObject.GetComponent<Animator>().SetBool("bookanim",true);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
