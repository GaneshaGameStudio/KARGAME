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
    public float range1;
    public float range2;
    // Start is called before the first frame update
    void Start()
    {   
        respawnpaths = GameObject.FindGameObjectsWithTag("path");
    }
    private void OnTriggerEnter(Collider other){
        gopath = other.gameObject;
    }
    void spawntraffic(){
        goobject = Resources.Load("BMTC_1") as GameObject;
        for (int j=0;j<respawnpaths.Length;j++){
            for(int i=0; i<respawnpaths[j].transform.childCount;i++)
            {   
                double dist = Mathf.Pow(Mathf.Pow((transform.position.x - respawnpaths[j].transform.GetChild(i).position.x),2f) + Mathf.Pow((transform.position.z - respawnpaths[j].transform.GetChild(i).position.z),2f),0.5f);
                if(dist<range1 && dist >range2)
                {
                    nodenumber = i;
                    goobject.GetComponent<CarEngine>().currentNode = nodenumber;
                    goobject.GetComponent<CarEngine>().path = respawnpaths[j].transform;
                    Instantiate(goobject, respawnpaths[j].transform.GetChild(i).position, Quaternion.identity);

                }
                else
                {
                    continue;
                }
            }
        }
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
