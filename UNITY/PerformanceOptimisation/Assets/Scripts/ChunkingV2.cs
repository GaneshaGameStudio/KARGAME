using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkingV2 : MonoBehaviour
{   
    private float xmin = 1441.5f;
    private float zmin = 2028.9f;
    private float xinterval = 205f;
    private float zinterval = 185f;
    private float[] xint;
    private float[] zint;
    private float[] xdefault,zdefault;
    private float FOV;
    private float FCP;
    // Start is called before the first frame update
    void Start()
    {   
        xint = new float[7];
        zint = new float[7];
        xdefault = new float[7];
        zdefault = new float[7];
        FOV = Camera.main.fieldOfView;
        FCP = Camera.main.farClipPlane;
        Calcarray(transform.position.x, transform.position.z);
        for(int i  =0;i<=xint.Length-1;i++){
            xdefault[i] = xint[i];
            zdefault[i] = zint[i];
        }
        
        for(int i=0;i<=xint.Length-1;i++){
            if(GameObject.Find("map_4_"+zint[i]+"_"+xint[i]+"(Clone)")){
            
            }
            else{
                GameObject mapobject = Resources.Load("map_4_"+zint[i]+"_"+xint[i]) as GameObject;
                Instantiate(mapobject, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(-90, 0, 0)));
            }
            
        }
    }
    void Calcarray(float x, float z){
        float yangledeg = transform.rotation.eulerAngles.y;
        float yanglerad = (yangledeg/57.3f);
        float xcomp = (Mathf.Sin(yanglerad))*FCP;
        float zcomp = (1-Mathf.Cos(yanglerad))*FCP; 
       
        xint[0] = Mathf.Floor(Mathf.Abs(x - xmin)/xinterval + 11f);
        zint[0] = Mathf.Floor(Mathf.Abs(z- zmin)/zinterval + 7f);

        xint[1] = Mathf.Floor(Mathf.Abs(x - xmin + xcomp)/xinterval + 11f);
        zint[1] = Mathf.Floor(Mathf.Abs(z+(0.5f*FCP) - zmin - 0.5f*zcomp)/zinterval + 7f);

        xint[2] = Mathf.Floor(Mathf.Abs(x - xmin + xcomp)/xinterval + 11f);
        zint[2] = Mathf.Floor(Mathf.Abs(z+(FCP) - zmin - zcomp)/zinterval + 7f);

        xint[3] = Mathf.Floor(Mathf.Abs(x-Mathf.Tan(FOV)*FCP - xmin + xcomp)/xinterval + 11f);
        zint[3] = Mathf.Floor(Mathf.Abs(z+(FCP) - zmin - zcomp)/zinterval + 7f);

        xint[4] = Mathf.Floor(Mathf.Abs(x-Mathf.Tan(FOV)*0.5f*FCP - xmin + xcomp)/xinterval + 11f);
        zint[4] = Mathf.Floor(Mathf.Abs(z+(FCP*0.5f) - zmin - zcomp)/zinterval + 7f);

        xint[5] = Mathf.Floor(Mathf.Abs(x+Mathf.Tan(FOV)*FCP - xmin + xcomp)/xinterval + 11f);
        zint[5] = Mathf.Floor(Mathf.Abs(z+(FCP) - zmin - zcomp)/zinterval + 7f);

        xint[6] = Mathf.Floor(Mathf.Abs(x+Mathf.Tan(FOV)*0.5f*FCP - xmin + xcomp)/xinterval + 11f);
        zint[6] = Mathf.Floor(Mathf.Abs(z+(FCP*0.5f) - zmin - zcomp)/zinterval + 7f);
    }

    void DetectChangeandUnload(float x, float z,int i){
        
        if((x - xdefault[i])!=0){
            print("xchanged");
            Destroy(GameObject.Find("map_4_"+zdefault[i]+"_"+xdefault[i]+"(Clone)"));
            xdefault[i] = x;
        }
        else if((z - zdefault[i])!=0){
            print("zchanged");
            Destroy(GameObject.Find("map_4_"+zdefault[i]+"_"+xdefault[i]+"(Clone)"));  
            zdefault[i] = z;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        Calcarray(transform.position.x, transform.position.z);
        for(int i=0;i<=xint.Length-1;i++){
            DetectChangeandUnload(xint[i],zint[i],i);
            if(GameObject.Find("map_4_"+zint[i]+"_"+xint[i]+"(Clone)")){
                
            }
            else{
                GameObject mapobject = Resources.Load("map_4_"+zint[i]+"_"+xint[i]) as GameObject;
                Instantiate(mapobject, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(-90, 0, 0)));
            }
        }
    }
}