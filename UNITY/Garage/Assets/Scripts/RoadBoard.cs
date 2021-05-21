using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoadBoard : MonoBehaviour
{   
    public string Areaname;
    private GameObject road;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {   
        
        if(other.tag=="Kit"){
            road = GameObject.Find("RoadBoard");
            road.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(Areaname);
            StartCoroutine("roadshow");
        }
    }
    private IEnumerator roadshow(){
        while(true){
            road.transform.localPosition = new Vector3(road.transform.localPosition.x, road.transform.localPosition.y+5f, road.transform.localPosition.z);
            if(road.transform.position.y>150f){
                break;
            }
            yield return new WaitForSeconds(0.01f); 
        }
    
        yield return new WaitForSeconds (3f);
        while(true){
            road.transform.localPosition = new Vector3(road.transform.localPosition.x, road.transform.localPosition.y-5f, road.transform.localPosition.z);
            if(road.transform.position.y<0f){
                break;
            }
            yield return new WaitForSeconds(0.01f); 
        }
    }

}
