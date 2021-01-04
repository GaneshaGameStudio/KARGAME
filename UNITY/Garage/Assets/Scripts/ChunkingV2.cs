using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkingV2 : MonoBehaviour
{   
    private float xmin = 3697.1f;
    private float zmin = 3321.4f;
    private float xinterval = 205f;
    private float zinterval = 184.7f;
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
                GameObject roadobject = Resources.Load("Roads_prefabs/roads_4_"+zint[i]+"_"+xint[i]) as GameObject;
                GameObject mapobject = Resources.Load("Map_prefabs/map_4_"+zint[i]+"_"+xint[i]) as GameObject;
                Instantiate(roadobject, new Vector3(0, -990, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                Instantiate(mapobject, new Vector3(0, -90, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            
        }
    }
    void Calcarray(float x, float z){
        float yangledeg = transform.rotation.eulerAngles.y;
        float yanglerad = (yangledeg/57.3f);
        float xcomp = (Mathf.Sin(yanglerad))*FCP;
        float zcomp = (1-Mathf.Cos(yanglerad))*FCP; 
       
        xint[0] = Mathf.Floor(Mathf.Abs(x - xmin)/xinterval + 0f);
        zint[0] = Mathf.Floor(Mathf.Abs(z- zmin)/zinterval + 0f);
        /*
        xint[1] = Mathf.Floor(Mathf.Abs(x - xmin + xcomp)/xinterval + 0f);
        zint[1] = Mathf.Floor(Mathf.Abs(z+(0.5f*FCP) - zmin - 0.5f*zcomp)/zinterval + 0f);

        xint[2] = Mathf.Floor(Mathf.Abs(x - xmin + xcomp)/xinterval + 0f);
        zint[2] = Mathf.Floor(Mathf.Abs(z+(FCP) - zmin - zcomp)/zinterval + 0f);
        
        xint[3] = Mathf.Floor(Mathf.Abs(x-Mathf.Tan(FOV)*FCP - xmin + xcomp)/xinterval + 0f);
        zint[3] = Mathf.Floor(Mathf.Abs(z+(FCP) - zmin - zcomp)/zinterval + 0f);

        xint[4] = Mathf.Floor(Mathf.Abs(x-Mathf.Tan(FOV)*0.5f*FCP - xmin + xcomp)/xinterval + 0f);
        zint[4] = Mathf.Floor(Mathf.Abs(z+(FCP*0.5f) - zmin - zcomp)/zinterval + 0f);

        xint[5] = Mathf.Floor(Mathf.Abs(x+Mathf.Tan(FOV)*FCP - xmin + xcomp)/xinterval + 0f);
        zint[5] = Mathf.Floor(Mathf.Abs(z+(FCP) - zmin - zcomp)/zinterval + 0f);

        xint[6] = Mathf.Floor(Mathf.Abs(x+Mathf.Tan(FOV)*0.5f*FCP - xmin + xcomp)/xinterval + 0f);
        zint[6] = Mathf.Floor(Mathf.Abs(z+(FCP*0.5f) - zmin - zcomp)/zinterval + 0f);
        */
        xint[1] = xint[0] - 1;
        zint[1] = zint[0];

        xint[2] = xint[0] + 1;
        zint[2] = zint[0];

        xint[3] = xint[0] - 1;
        zint[3] = zint[0] + 1 ;

        xint[4] = xint[0] + 1;
        zint[4] = zint[0] + 1; 

        xint[5] = xint[0];
        zint[5] = zint[0] + 1; 

        xint[6] = xint[0];
        zint[6] =zint[0] -1;
        
    }

    void DetectChangeandUnload(float x, float z,int i){
        
        if((x - xdefault[i])!=0){
            
            Destroy(GameObject.Find("map_4_"+zdefault[i]+"_"+xdefault[i]+"(Clone)"));
            Destroy(GameObject.Find("roads_4_"+zdefault[i]+"_"+xdefault[i]+"(Clone)"));
            xdefault[i] = x;
        }
        else if((z - zdefault[i])!=0){
            
            Destroy(GameObject.Find("map_4_"+zdefault[i]+"_"+xdefault[i]+"(Clone)"));  
            Destroy(GameObject.Find("roads_4_"+zdefault[i]+"_"+xdefault[i]+"(Clone)")); 
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
                GameObject roadobject = Resources.Load("Roads_prefabs/roads_4_"+zint[i]+"_"+xint[i]) as GameObject;
                GameObject mapobject = Resources.Load("Map_prefabs/map_4_"+zint[i]+"_"+xint[i]) as GameObject;
                Instantiate(roadobject, new Vector3(0, -990, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                Instantiate(mapobject, new Vector3(0, -90, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                
            }
        }
    }
}