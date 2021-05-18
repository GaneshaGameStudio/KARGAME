using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class fuelreader : MonoBehaviour
{   
    
    private float TotalDistance;
    private GameObject GO;
    private float CT;
    public float TC;
    private float M;
    static public float RF;
    public float remful;
    public float remainingnorm;
    private float rectwidth;
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    public bool isKhali = false;
    private TextMeshProUGUI DistancePro;
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
        DistancePro = GameObject.Find("Distance-number").GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        GO = GameObject.Find(VehicleID.Vehicle + "(Clone)");
        if(GO.tag == "4Wheeler")
        {
            TC = GameObject.FindWithTag("Kit").GetComponent<SimpleCarController>().tankcap;
            M = GameObject.FindWithTag("Kit").GetComponent<SimpleCarController>().mileage;
            RF = SimpleCarController.remainingfuel;
            Rigidbody rb = GO.GetComponent<Rigidbody>();
            TotalDistance += (rb.velocity.magnitude * Time.deltaTime);
            remful = RF*TC - (rb.velocity.magnitude * Time.deltaTime/M);
            remainingnorm = remful/TC;
            RF = remainingnorm;
            SimpleCarController.remainingfuel = remainingnorm;
        }
        else if (GO.tag == "6Wheeler")
        {
            TC = GameObject.FindWithTag("6Wheeler").GetComponent<SimpleCarController>().tankcap;
            M = GameObject.FindWithTag("6Wheeler").GetComponent<SimpleCarController>().mileage;
            RF = SimpleCarController.remainingfuel;
            Rigidbody rb = GO.GetComponent<Rigidbody>();
            TotalDistance += (rb.velocity.magnitude * Time.deltaTime);
            remful = RF*TC - (rb.velocity.magnitude * Time.deltaTime/M);
        }
        else if (GO.tag == "Manushya")
        {
            TC = GameObject.FindWithTag("Manushya").GetComponent<SimpleBodyController>().tankcap;
            M = GameObject.FindWithTag("Manushya").GetComponent<SimpleBodyController>().mileage;
            RF = SimpleBodyController.remainingfuel;
            if(RF == 1f){
                TotalDistance = 0f;
            }
            CharacterController rb = GO.GetComponent<CharacterController>();
            TotalDistance += (rb.velocity.magnitude * Time.deltaTime);
            remful = TC - (TotalDistance/M);
            remainingnorm = remful/TC;
            RF = remainingnorm;
            SimpleBodyController.remainingfuel = remainingnorm;
        }
        else
        {
            TC = GameObject.FindWithTag("WheelFC").GetComponent<SimpleDrive>().tankcap;
            M = GameObject.FindWithTag("WheelFC").GetComponent<SimpleDrive>().mileage;
            RF = SimpleDrive.remainingfuel;
            Rigidbody rb = GO.GetComponent<Rigidbody>();
            TotalDistance += (rb.velocity.magnitude * Time.deltaTime);
            remful = RF*TC - (rb.velocity.magnitude * Time.deltaTime/M);
            remainingnorm = remful/TC;
            RF = remainingnorm;
            SimpleDrive.remainingfuel = remainingnorm;
        }
        
        DistancePro.SetText((Mathf.Round(TotalDistance*0.001f*10.0f)/10.0f).ToString());
        if(remful <= 0){
            isKhali = true;
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Max(remainingnorm*rectwidth,rectwidth*0.05f), 15f);
        gradient.SetKeys(colorKey, alphaKey);
        GetComponent<Image>().color = gradient.Evaluate(remainingnorm);
    }
}
