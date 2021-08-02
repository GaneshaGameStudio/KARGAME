using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Garbage");
        StartCoroutine("Firehazard");
        StartCoroutine("Drainage");
    }

    private IEnumerator Garbage(){
        yield return new WaitForSeconds(5f);
        //spawn at a radius and angle
        while(true){
            int degree = Random.Range(0,360);
            int rad = Random.Range(0,25);
            if(GameObject.Find("Garbage(Clone)")){
                GameObject.Find("Garbage(Clone)").transform.position = GameObject.Find("Garbage(Clone)").transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad*degree)*rad,0f,Mathf.Sin(Mathf.Deg2Rad*degree)*rad);
            
            }
            else
            {
                GameObject.Instantiate(Resources.Load("Landmarks_Prefabs/Garbage"), transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad*degree)*rad,0f,Mathf.Sin(Mathf.Deg2Rad*degree)*rad), transform.rotation);
            }
            yield return new WaitForSeconds(120f);
        }
        
        
    }

    private IEnumerator Firehazard(){
         yield return new WaitForSeconds(5f);
        //spawn at a radius and angle
        while(true){
            int degree = Random.Range(0,360);
            int rad = Random.Range(0,25);
            if(GameObject.Find("Firehazard(Clone)")){
                GameObject.Find("Firehazard(Clone)").transform.position = GameObject.Find("Firehazard(Clone)").transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad*degree)*rad,0f,Mathf.Sin(Mathf.Deg2Rad*degree)*rad);
            
            }
            else
            {
                GameObject.Instantiate(Resources.Load("Landmarks_Prefabs/Firehazard"), transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad*degree)*rad,0f,Mathf.Sin(Mathf.Deg2Rad*degree)*rad), transform.rotation);
            }
            yield return new WaitForSeconds(5000f);
        }
             
        
        
    }

    private IEnumerator Drainage(){
        yield return new WaitForSeconds(7f);
        while(true){
            int degree = Random.Range(0,360);
            int rad = Random.Range(0,25);
            if(GameObject.Find("Drainage(Clone)")){
                GameObject.Find("Drainage(Clone)").transform.position = GameObject.Find("Drainage(Clone)").transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad*degree)*rad,0f,Mathf.Sin(Mathf.Deg2Rad*degree)*rad);
            
            }
            else
            {
                GameObject.Instantiate(Resources.Load("Landmarks_Prefabs/Drainage"), transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad*degree)*rad,0f,Mathf.Sin(Mathf.Deg2Rad*degree)*rad), transform.rotation);
            }
            yield return new WaitForSeconds(120f);
        }
       

        
    }
}
