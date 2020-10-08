using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TrafficPool : MonoBehaviour
{   
    private int nodenumber;
    private GameObject[] respawns;
    GameObject closest = null;
    public float InstantiateTimer = 20f;
    private float InstantiationTimer;
    public GameObject goobject;
    public GameObject gopath;
    public float range1;
    public float range2;
    // Start is called before the first frame update
    void Start()
    {
        respawns = GameObject.FindGameObjectsWithTag("node");
        
    }
    private void OnTriggerEnter(Collider other){
        gopath = other.gameObject;
    }
    void spawntraffic(){
        goobject = Resources.Load("BMTC_1") as GameObject;
        int i = 0;
        
        foreach (GameObject respawn in respawns)
        {
            double dist = Mathf.Pow(Mathf.Pow((transform.position.x - respawn.transform.position.x),2f) + Mathf.Pow((transform.position.z - respawn.transform.position.z),2f),0.5f);
            if(dist<range1 && dist >range2)
            {
                closest = respawn;
                nodenumber = i;
                goobject.GetComponent<CarEngine>().currentNode = nodenumber;
                goobject.GetComponent<CarEngine>().path = gopath.transform;
                Instantiate(goobject, respawn.transform.position, Quaternion.identity);
            }
            i++;
            
        }
        InstantiationTimer = InstantiateTimer;
    }
    private void OnDrawGizmos(){
        Handles.color = new Color(0,1,0,.1f);
        Handles.DrawSolidArc(transform.position,new Vector3(0, 1, 0),new Vector3(1, 0, 0),360,range1);
        Handles.color = new Color(1,0,0,.4f);
        Handles.DrawSolidArc(transform.position,new Vector3(0, 1, 0),new Vector3(1, 0, 0),360,range2);
    }
    // Update is called once per frame
    void Update()
    {   
        InstantiationTimer -= Time.deltaTime;
        if(InstantiationTimer<=0)
        {
            spawntraffic();
            InstantiationTimer = InstantiateTimer;
        }
    }
}
