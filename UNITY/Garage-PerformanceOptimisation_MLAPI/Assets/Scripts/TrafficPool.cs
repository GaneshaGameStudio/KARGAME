using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TrafficPool : MonoBehaviour
{   
    private int nodenumber;
    private GameObject[] respawnobjects;
    private GameObject[] respawnpaths;
    public float InstantiateTimer = 10f;
    private float InstantiationTimer;
    public GameObject goobject;
    public GameObject gopath;
    private List<Transform> makalu;
    public float range1;
    public float range2;
    // Start is called before the first frame update
    void Start()
    {   
        
    }
    private void OnTriggerEnter(Collider other){
        if(other.tag == "path"){
            //print("bmtc");
            gopath = other.gameObject;
        }
    }
    void spawntraffic(){
        goobject = Resources.Load("Vehicles_prefabs/" + "BMTC_1_Traffic") as GameObject;
        for (int j=0;j<respawnpaths.Length;j++){
            for(int i=0; i<respawnpaths[j].transform.childCount;i++)
            {   
                double dist = Mathf.Pow(Mathf.Pow((transform.position.x - respawnpaths[j].transform.GetChild(i).position.x),2f) + Mathf.Pow((transform.position.z - respawnpaths[j].transform.GetChild(i).position.z),2f),0.5f);
                if(dist<range1 && dist >range2)
                {
                    nodenumber = i;
                    goobject.GetComponent<CarEngine>().currentNode = nodenumber;
                    goobject.GetComponent<CarEngine>().path = respawnpaths[j].transform;
                    float angle;
                    //Find angle
                    if(i == respawnpaths[j].transform.childCount-1){
                        angle = Mathf.Atan2((respawnpaths[j].transform.GetChild(i).position.x-respawnpaths[j].transform.GetChild(0).position.x),(respawnpaths[j].transform.GetChild(i).position.z-respawnpaths[j].transform.GetChild(0).position.z));
                    }
                    else{
                        angle = Mathf.Atan2((respawnpaths[j].transform.GetChild(i).position.x - respawnpaths[j].transform.GetChild(i+1).position.x),(respawnpaths[j].transform.GetChild(i).position.z - respawnpaths[j].transform.GetChild(i+1).position.z));
                    }
                    if(GameObject.FindGameObjectsWithTag("Traffic").Length < 2){
                        Instantiate(goobject, respawnpaths[j].transform.GetChild(i).position,Quaternion.Euler(0f,angle*57.3f,0f));
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }
    // private void OnDrawGizmos(){
    //     Handles.color = new Color(0,1,0,.1f);
    //     Handles.DrawSolidArc(transform.position,new Vector3(0, 1, 0),new Vector3(1, 0, 0),360,range1);
    //     Handles.color = new Color(1,0,0,.4f);
    //     Handles.DrawSolidArc(transform.position,new Vector3(0, 1, 0),new Vector3(1, 0, 0),360,range2);
    // }

    
    // Update is called once per frame
    void Update()
    {   
        respawnpaths = GameObject.FindGameObjectsWithTag("path");
        InstantiationTimer -= Time.deltaTime;
        if(InstantiationTimer<=0 && GameObject.FindGameObjectsWithTag("Traffic").Length < 2)
        {
            spawntraffic();
            InstantiationTimer = InstantiateTimer;
        }
    }
}
