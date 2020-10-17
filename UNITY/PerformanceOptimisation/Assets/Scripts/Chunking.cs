using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunking : MonoBehaviour
{   
    private float xmin = 1441.5f;
    private float ymin = 2028.9f;
    private float xinterval = 205f;
    private float yinterval = 185f;
    private float xint;
    private float yint;
    private float xdefault,ydefault;
    // Start is called before the first frame update
    void Start()
    {     
        xint = Mathf.Floor(Mathf.Abs(transform.position.x - xmin)/xinterval + 11f);
        yint = Mathf.Floor(Mathf.Abs(transform.position.z - ymin)/yinterval + 7f);
        xdefault = xint;
        ydefault = yint;
        GameObject mapobject = Resources.Load("map_4_"+yint+"_"+xint) as GameObject;
        Instantiate(mapobject, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(-90, 0, 0)));
    }
    void DetectChangeandUnload(float x, float y){
        if((x - xdefault )!=0){
            print("xchnged");
            Destroy(GameObject.Find("map_4_"+ydefault+"_"+xdefault+"(Clone)"));
            xdefault = xint;
        }
        else if((y - ydefault)!=0){
            print("ychanged");
            Destroy(GameObject.Find("map_4_"+ydefault+"_"+xdefault+"(Clone)"));
            ydefault = yint;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        DetectChangeandUnload(xint,yint);
        xint = Mathf.Floor(Mathf.Abs(transform.position.x - xmin)/xinterval + 11f);
        yint = Mathf.Floor(Mathf.Abs(transform.position.z - ymin)/yinterval + 7f);
        if(GameObject.Find("map_4_"+yint+"_"+xint+"(Clone)")){
            
        }
        else{
            GameObject mapobject = Resources.Load("map_4_"+yint+"_"+xint) as GameObject;
            Instantiate(mapobject, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
        Debug.Log(xint);
        Debug.Log(yint);
    }
}
