using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clamp : MonoBehaviour
{   
    private Text nameLabel;
    private Text messageLabel;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start(){
        GameObject baraha = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        
        nameLabel = baraha.GetComponent<Text>();
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        nameLabel.transform.position = namePos;

    
        int vel = Mathf.RoundToInt(rb.velocity.magnitude*3.6f);
        nameLabel.text = vel.ToString();
    }
}
