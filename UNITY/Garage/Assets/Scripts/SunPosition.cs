using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunPosition : MonoBehaviour
{   
    public float Xanglerate;
    public float Timeintercept;
    public float Timecurrent;
    [Range(0f,360f)]
    public float Xanglecurrent;
    // Start is called before the first frame update
    void Start()
    {   
        
    }
    // Update is called once per frame
    void Update()
    {   
        Xanglecurrent = Xanglerate*Timecurrent + -Xanglerate*Timeintercept;
        transform.rotation = Quaternion.Euler(Xanglecurrent + Time.deltaTime*(Xanglerate/60),0,0);
    }
}
