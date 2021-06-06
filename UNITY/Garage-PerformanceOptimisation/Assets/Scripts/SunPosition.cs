using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunPosition : MonoBehaviour
{   
    public float Xanglerate;
    public float Timeintercept;
    public float Timecurrent;
    public float East;
    [Range(0f,360f)]
    public float Xanglecurrent;
    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine("SunPositionUpdate");
        //xtransform.rotation = Quaternion.Euler(0,0,0);
    }
    private IEnumerator SunPositionUpdate(){
        while(true){
            Xanglecurrent = Xanglerate*Timecurrent + -Xanglerate*Timeintercept;
            transform.rotation = Quaternion.Euler(Xanglecurrent + Time.deltaTime*(Xanglerate/60),East,0);
            DynamicGI.UpdateEnvironment();
            yield return new WaitForSeconds(10f);
            }
        
    }
    
}
