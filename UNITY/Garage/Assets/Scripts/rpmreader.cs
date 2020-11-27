using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rpmreader : MonoBehaviour
{   
    private float rpmmax;
    private float rectwidth;
    // Start is called before the first frame update
    void Start()
    {
        rpmmax = 200f;
        rectwidth = 110f;
    }

    // Update is called once per frame
    void Update()
    {   
        
        float currentrpm = GameObject.FindWithTag("WheelFC").GetComponent<WheelCollider>().rpm;
        float normrpm = (currentrpm+210f)/rpmmax;
        GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Min(Mathf.Log(normrpm)*rectwidth,rectwidth), 15f);
    }
}
