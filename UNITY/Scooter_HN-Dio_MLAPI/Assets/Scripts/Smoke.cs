using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{   
    private float VelE;
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        VelE = GameObject.Find("HN-Dio_stock(Clone)").GetComponent<Lean>().vel;
        var main = ps.main;
        main.simulationSpeed = Mathf.Max(5f,VelE);
        main.startSpeed = Mathf.Max(0.05f,VelE/36f);
    }
}
