using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{   
    private string Vehicletemp;
    private GameObject GOO;
    private Vector3 buildingcoord;
    private Vector3 buildingrot;
    private float deg2rad = 0.01745311f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider building)
    {   
        if(building.tag=="2Wheeler"){
            print("buildingentered");
            GOO = GameObject.Find(VehicleID.Vehicle + "(Clone)");
            buildingcoord = GOO.transform.position;
            buildingrot = GOO.transform.localRotation.eulerAngles;
            Vehicletemp = VehicleID.Vehicle;
            Destroy(GOO);
            VehicleID.Vehicle = "Vibe2009rig-redoCSY";
            Instantiate(Resources.Load("Vibe2009rig-redoCSY"), new Vector3(buildingcoord.x + 1.0f*Mathf.Sin(deg2rad*buildingrot.y),buildingcoord.y ,buildingcoord.z+ 1.0f*Mathf.Cos(deg2rad*buildingrot.y)), Quaternion.Euler(new Vector3(0f,buildingrot.y,0f)));
            GOO = GameObject.Find(VehicleID.Vehicle + "(Clone)");
            CameraFollowController.objectToFollow = GOO.transform;
        }
        
    }
    private void OnTriggerExit(Collider building)
    {  
        if(building.tag=="Manushya"){
        print("buildingexit");
        GOO = GameObject.Find(VehicleID.Vehicle + "(Clone)");
        buildingcoord = GOO.transform.position;
        buildingrot = GOO.transform.localRotation.eulerAngles;
        VehicleID.Vehicle = Vehicletemp;
        Destroy(GOO);
        Instantiate(Resources.Load(VehicleID.Vehicle), new Vector3(buildingcoord.x + 1.0f*Mathf.Sin(deg2rad*buildingrot.y),buildingcoord.y ,buildingcoord.z+ 1.0f*Mathf.Cos(deg2rad*buildingrot.y)), Quaternion.Euler(new Vector3(0f,buildingrot.y,0f)));
        GOO = GameObject.Find(VehicleID.Vehicle + "(Clone)");
        CameraFollowController.objectToFollow = GOO.transform;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
