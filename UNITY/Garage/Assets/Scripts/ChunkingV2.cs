using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ChunkingV2 : MonoBehaviour
{   
    private float xmin = 3697.1f;
    private float zmin = 3321.4f;
    private float xinterval = 205f;
    private float zinterval = 184.7f;
    private float[] xint;
    private float[] zint;
    private float[] xdefault,zdefault;
    private int directionquad;
    static public float directionquadINIT;
    private float FOV;
    private float FCP;
    private string[] busroute = new string[1296];
    // Start is called before the first frame update
    void Start()
    {   
        xint = new float[7];
        zint = new float[7];
        xdefault = new float[7];
        zdefault = new float[7];
        FOV = Camera.main.fieldOfView;
        FCP = Camera.main.farClipPlane;
        readcsv();
        StartCoroutine("deletenonexisting");
    }

    void readcsv()
    {   
        int bs = 0;
        using(var reader = new StreamReader("Assets/Resources/Map4_Busroute_tracker.csv"))
        {
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                busroute[bs] = values[2];
                bs=bs+1;
            }
        }
    }
    void Calcarray(float x, float z){
        float yangledeg = transform.rotation.eulerAngles.y;
        float yanglerad = (yangledeg/57.3f);
        float xcomp = (Mathf.Sin(yanglerad))*FCP;
        float zcomp = (1-Mathf.Cos(yanglerad))*FCP; 
        directionquad = (int)((transform.rotation.eulerAngles.y -45)/90);
        xint[0] = Mathf.Floor(Mathf.Abs(x - xmin)/xinterval + 0f);
        zint[0] = Mathf.Floor(Mathf.Abs(z- zmin)/zinterval + 0f);
        //which quadrant the camera is facing
        //facing +z
        if(directionquad == 1 )
          {
            xint[1] = xint[0];
            zint[1] = zint[0] + 1; 

            xint[2] = xint[0] - 1;
            zint[2] = zint[0] + 1 ;

            xint[3] = xint[0] + 1;
            zint[3] = zint[0] + 1; 

            xint[4] = xint[0];
            zint[4] =zint[0] -1;

            xint[5] = xint[0] - 1;
            zint[5] = zint[0];
            
            xint[6] = xint[0] + 1;
            zint[6] = zint[0];
            
          }  
          //facing +x
          else if(directionquad == 2 )
          {
            
            xint[1] = xint[0] + 1;
            zint[1] = zint[0];

            xint[2] = xint[0] + 1;
            zint[2] = zint[0] + 1;

            xint[3] = xint[0] + 1;
            zint[3] = zint[0] - 1 ;

            xint[4] = xint[0] -1 ;
            zint[4] =zint[0];

            xint[5] = xint[0];
            zint[5] = zint[0] + 1; 

            xint[6] = xint[0];
            zint[6] = zint[0] - 1; 
            
          }  
          //facing -z
          else if(directionquad == 3 )
          {
            
            xint[1] = xint[0];
            zint[1] = zint[0] - 1;

            xint[2] = xint[0] + 1;
            zint[2] = zint[0] - 1;

            xint[3] = xint[0] - 1;
            zint[3] = zint[0] - 1 ;

            xint[4] = xint[0] ;
            zint[4] =zint[0] + 1;

            xint[5] = xint[0] - 1;
            zint[5] = zint[0]; 
            
            xint[6] = xint[0] + 1;
            zint[6] = zint[0]; 

          }  
          //facing -x
          else if(directionquad == 0 )
          {
        
            xint[1] = xint[0] - 1;
            zint[1] = zint[0];

            xint[2] = xint[0] - 1;
            zint[2] = zint[0] - 1;

            xint[3] = xint[0] - 1;
            zint[3] = zint[0] + 1 ;

            xint[4] = xint[0] + 1 ;
            zint[4] =zint[0];

            xint[5] = xint[0];
            zint[5] = zint[0] + 1; 

            xint[6] = xint[0];
            zint[6] = zint[0] - 1;
          }  
    }


    void DetectChangeandUnload(float x, float z,int i){
        
        if((x - xdefault[i])!=0 || (z - zdefault[i])!=0){
            if(directionquad - directionquadINIT!=0){
                    Destroy(GameObject.Find("map_4_"+zdefault[2]+"_"+xdefault[2]+"(Clone)"));
                    Destroy(GameObject.Find("roads_4_"+zdefault[2]+"_"+xdefault[2]+"(Clone)"));
                    directionquadINIT = directionquad;
                    //print("dir changed");
                    xdefault[2] = x;
                    zdefault[2] = z;
                } 
            if(i!=0 && i!=1 && i!=2 && i!=3){
                Destroy(GameObject.Find("map_4_"+zdefault[i]+"_"+xdefault[i]+"(Clone)"));
                Destroy(GameObject.Find("roads_4_"+zdefault[i]+"_"+xdefault[i]+"(Clone)"));
                xdefault[i] = x;
                zdefault[i] = z;
            }
        }
    }
    private IEnumerator deletenonexisting(){
        bool fastWavesStarted = true;
        while(fastWavesStarted == true){
            yield return new WaitForSeconds(2f);
            
            GameObject[] obj =  GameObject.FindGameObjectsWithTag("Map");
            foreach (GameObject o in obj)
            {
                for(int l=0;l<=xint.Length-1;l++){
                    if(o.name!="map_4_"+zdefault[l]+"_"+xdefault[l]+"(Clone)"){
                        Destroy(o);
                    }
                }
            }
        GameObject[] obj2 =  GameObject.FindGameObjectsWithTag("Roads");
            foreach (GameObject o in obj2)
            {
                for(int l=0;l<=xint.Length-1;l++){
                    if(o.name!="roads_4_"+zdefault[l]+"_"+xdefault[l]+"(Clone)"){
                        Destroy(o);
                    }
                }
        }
        fastWavesStarted = false;
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
                Instantiate(roadobject, new Vector3(0, -89.8f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                Instantiate(mapobject, new Vector3(0, -90, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            if(GameObject.Find(busroute[((int)xint[0]*36 + (int)zint[0])-1]+"(Clone)")){
                
            }
            else{
                Destroy (GameObject.FindWithTag("path"));
                if(busroute[((int)xint[0]*36 + (int)zint[0])-1]!=null){
                    GameObject busrouteobject = Resources.Load("BusRoutes_prefabs/" + busroute[((int)xint[0]*36 + (int)zint[0])-1]) as GameObject;
                    Instantiate(busrouteobject, new Vector3(0, -9, 0), Quaternion.Euler(new Vector3(-90, 0, 0)));
                }
                
            }
            
        
        xdefault[i] = xint[i];
        zdefault[i] = zint[i];
        }
    }
}