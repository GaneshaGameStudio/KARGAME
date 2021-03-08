using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fuelreader : MonoBehaviour
{   
    
    private float TotalDistance;
    private GameObject GO;
    private float CT;
    public float TC;
    private float M;
    private float RF;
    public float remful;
    private float rectwidth;
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    public bool isKhali = false;
    // Start is called before the first frame update
    void Start()
    {
        gradient = new Gradient();
        rectwidth = 110;
        //CT = 9f; //this is where player tank capacity needs to go!!
        colorKey = new GradientColorKey[3];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[2].color = new Color(0.1725f,0.1725f,0.1725f);
        colorKey[2].time = 0.4f;
        colorKey[1].color = new Color(1f,0.6f,0.0f);
        colorKey[1].time = 0.25f;

        alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 0.25f;
        alphaKey[2].alpha = 1.0f;
        alphaKey[2].time = 0.4f;
        gradient.SetKeys(colorKey, alphaKey);
        GetComponent<Image>().color = gradient.Evaluate(1f);

        
    }

    // Update is called once per frame
    void Update()
    {   
        if(GameObject.Find(VehicleID.Vehicle+"(Clone)").tag == "4Wheeler")
        {
            TC = GameObject.FindWithTag("4Wheeler").GetComponent<SimpleCarController>().tankcap;
            M = GameObject.FindWithTag("4Wheeler").GetComponent<SimpleCarController>().mileage;
            RF = GameObject.FindWithTag("4Wheeler").GetComponent<SimpleCarController>().remainingfuel;
        }
        else if (GameObject.Find(VehicleID.Vehicle + "(Clone)").tag == "6Wheeler")
        {
            TC = GameObject.FindWithTag("6Wheeler").GetComponent<SimpleCarController>().tankcap;
            M = GameObject.FindWithTag("6Wheeler").GetComponent<SimpleCarController>().mileage;
            RF = GameObject.FindWithTag("6Wheeler").GetComponent<SimpleCarController>().remainingfuel;
        }
        else
        {
            TC = GameObject.FindWithTag("WheelFC").GetComponent<SimpleDrive>().tankcap;
            M = GameObject.FindWithTag("WheelFC").GetComponent<SimpleDrive>().mileage;
            RF = GameObject.FindWithTag("WheelFC").GetComponent<SimpleDrive>().remainingfuel;
        }
        GO = GameObject.Find(VehicleID.Vehicle + "(Clone)");
        Rigidbody rb = GO.GetComponent<Rigidbody>();
        TotalDistance += (rb.velocity.magnitude * Time.deltaTime);
        float remainingnorm = (TC*M*RF - TotalDistance)/(TC*M*RF);
        remful = remainingnorm * TC * RF;
        if(remful <= 0){
            isKhali = true;
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Max(remainingnorm*rectwidth,rectwidth*0.05f), 15f);
        gradient.SetKeys(colorKey, alphaKey);
        GetComponent<Image>().color = gradient.Evaluate(remainingnorm);
        

    }
}
