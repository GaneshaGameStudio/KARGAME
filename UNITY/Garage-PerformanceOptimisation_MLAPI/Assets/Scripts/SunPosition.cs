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
        string sunrise = "2021-10-12T00:39:21";
        string sunset = "2021-10-12T12:32:31";
                        
                        
        string[] sunrisewords = sunrise.Split('T');
        string[] sunrisetimewords = sunrisewords[1].Split(':');
        int hours = int.Parse(sunrisetimewords[0]) + 5;
        int minutes = int.Parse(sunrisetimewords[1])+ 30;
        int sunriseminuteday = hours*60 + minutes;
                        
        string[] sunsetwords = sunset.Split('T');
        string[] sunsettimewords = sunsetwords[1].Split(':');
        hours = int.Parse(sunsettimewords[0]) + 5;
        minutes = int.Parse(sunsettimewords[1])+ 30;
        int sunsetminuteday = hours*60 + minutes;
                        
        float sunrate = 180f/(sunsetminuteday - sunriseminuteday);
        Xanglerate = sunrate;
        Xanglecurrent = Xanglerate*Timecurrent + -Xanglerate*Timeintercept;
        Timeintercept = sunriseminuteday;
        transform.rotation = Quaternion.Euler(Xanglecurrent + Time.deltaTime*(Xanglerate/60),East,0);
        StartCoroutine("SunPositionUpdate");
        //xtransform.rotation = Quaternion.Euler(0,0,0);
    }
    private IEnumerator SunPositionUpdate(){
        while(true){
            //yield return new WaitForSeconds(4.0f);
            Xanglecurrent = Xanglerate*Timecurrent + -Xanglerate*Timeintercept;
            if(Xanglecurrent!=0){
                //float time = 0;
                //float scalingTime = 1;
                transform.rotation = Quaternion.Euler(Xanglecurrent + Time.deltaTime*(Xanglerate/60),East,0);
            
                //while (time < 1){
                    //time += Time.deltaTime*0.01f / scalingTime;
                    //print("smooth");
                    //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Xanglecurrent + Time.deltaTime*(Xanglerate/60f),East,0f),time);
                    yield return new WaitForSeconds(0.05f);
                    
                //}
    
        
            }
            DynamicGI.UpdateEnvironment();
            yield return new WaitForSeconds(10f);
            }
        
    }
    
}
