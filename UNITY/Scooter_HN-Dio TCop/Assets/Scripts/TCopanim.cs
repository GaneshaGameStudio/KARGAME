using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCopanim : MonoBehaviour
{   
    Animator anim;
    public Transform other;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        float distance = Vector3.Distance(other.position, transform.position);
        float relativedir = transform.rotation.eulerAngles.y + other.transform.rotation.eulerAngles.y;
        
        if(distance<15 && relativedir < transform.rotation.eulerAngles.y + 90 && relativedir > transform.rotation.eulerAngles.y - 90){
            anim.SetBool("Kambi",true);
        }
        else{
            anim.SetBool("Kambi",false);
    }
}
}
