using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat : MonoBehaviour
{   
    public bool isCrash = false;
    public bool isKhalicheck = false;
    public bool isLicensetwoWheelercheck = false;
    public static int Life;
    //public GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        Life = 2;
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Traffic" ){
            if(other.impulse.magnitude / Time.fixedDeltaTime > 6000){
                if(Life <= 0){
                    GameObject.Find("Life"+Life.ToString()).SetActive(false);
                    Life=Life-1;
                    isCrash = true;
                    transform.Find("LogoAPPchatanim").gameObject.SetActive(true);
                }
                else{
                    GameObject.Find("Life"+Life.ToString()).SetActive(false);
                    Life=Life-1;
                    
                }
                
            
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "License" )
        {
            isLicensetwoWheelercheck = true;
            transform.Find("LogoAPPchatanim").gameObject.SetActive(true);
        }
    }
        
    // Update is called once per frame
    void Update()
    {   
        
        if(GameObject.FindWithTag("fuel").GetComponent<fuelreader>().isKhali == true){
        isKhalicheck = true;
        transform.Find("LogoAPPchatanim").gameObject.SetActive(true);
        
        }
        
    }
}
