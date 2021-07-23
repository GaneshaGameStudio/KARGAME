using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class Garagerig : MonoBehaviour
{   
    public RigBuilder rb;
    // Start is called before the first frame update
    void OnDisable()

    {
        rb.enabled = false;
    }
    void OnEnable(){
        rb.enabled = false;
        StartCoroutine("EnableRig");
    }
    // Update is called once per frame
    private IEnumerator EnableRig(){
        yield return new WaitForSeconds(0.05f);
        rb.enabled = true;
    }
}
