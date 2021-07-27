using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicenseINIT : MonoBehaviour
{   
    private GameObject WC;
    // Start is called before the first frame update
    
    void Start()
    {   
        StartCoroutine("LicenseColliders");
        
    }

    // Update is called once per frame
    private IEnumerator LicenseColliders(){
        yield return new WaitForSeconds(1f);
        if(GameObject.FindWithTag("2Wheeler")){
            WC = GameObject.Find("2WheelerColliders");
        }
        else if(GameObject.FindWithTag("3Wheeler")){
            WC = GameObject.Find("3WheelerColliders");
        }
        else if(GameObject.FindWithTag("4Wheeler")){
            WC = GameObject.Find("4WheelerColliders");
        }
        else if(GameObject.FindWithTag("6Wheeler")){
            WC = GameObject.Find("6WheelerColliders");
        }
        GameObject.Find("2WheelerColliders").SetActive(false);
        GameObject.Find("3WheelerColliders").SetActive(false);
        GameObject.Find("4WheelerColliders").SetActive(false);
        GameObject.Find("6WheelerColliders").SetActive(false);
        WC.SetActive(true);
        
    }
}
