using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficPool : MonoBehaviour
{   
    private int nodenumber;
    private GameObject[] respawns;
    GameObject closest = null;
    public float InstantiateTimer = 20f;
    private float InstantiationTimer;
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        respawns = GameObject.FindGameObjectsWithTag("node");
        
    }
    void spawntraffic(){
        go = Resources.Load("BMTC_1") as GameObject;
        int i = 0;
        
        foreach (GameObject respawn in respawns)
        {
            double dist = Mathf.Pow(Mathf.Pow((transform.position.x - respawn.transform.position.x),2f) + Mathf.Pow((transform.position.z - respawn.transform.position.z),2f),0.5f);
            if(dist<120 && dist >70)
            {
                closest = respawn;
                //print(closest);
                nodenumber = i;
                print(nodenumber);
                go.GetComponent<CarEngine>().currentNode = nodenumber;
                Instantiate(go, respawn.transform.position, Quaternion.identity);
            }
            i++;
            
        }
        InstantiationTimer = InstantiateTimer;
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
