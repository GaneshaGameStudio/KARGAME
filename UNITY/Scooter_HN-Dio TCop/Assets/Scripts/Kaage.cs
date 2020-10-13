using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaage : MonoBehaviour
{   
    public GameObject rotationcenter;
    public GameObject aatagara;
    public float vin = 10;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Updateposition", 0, 100.0f);
    }
    void Updateposition(){
        transform.position = aatagara.transform.position + new Vector3 (-5f,0f,0f);;
        transform.rotation = aatagara.transform.rotation;
        rotationcenter.transform.position = transform.position + new Vector3 (100f,0f,0f);
        rotationcenter.transform.rotation = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {   

        //transform.position = aatagara.transform.position;
        transform.RotateAround(rotationcenter.transform.position, Vector3.up, vin * Time.deltaTime);

    }
}
