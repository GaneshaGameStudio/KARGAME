using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Scaler : MonoBehaviour
{   
    private Volume m_Volume;
    public VolumeProfile BW;
    private float mSize = 100.0f;
    private float cSize = 0.0f;
    public bool playanim = false;
    
    
    // Start is called before the first frame update
    void OnEnable()
    {   
        
        m_Volume = GameObject.Find("Post-process Volume").GetComponent<Volume>();
        //VolumeProfile profile = m_Volume.sharedProfile;
        playanim = true;
        mSize = 100.0f;
        cSize = 0.0f;
        if(Chat.isCrash == true){
            GameObject.FindWithTag("UserText").GetComponent<Text>().text = "ºÉÆUÉ >:0";
            InvokeRepeating("Hoge",0.0f,0.005f);
        }
        else if(GameObject.FindWithTag("fuel").GetComponent<fuelreader>().isKhali == true){
            GameObject.FindWithTag("UserText").GetComponent<Text>().text = "SÁ°";
            InvokeRepeating("Khali",0.0f,0.005f);
        }
        else if(GameObject.Find(VehicleID.Vehicle+"(Clone)").GetComponent<Chat>().isLicensetwoWheelercheck == true){
            GameObject.FindWithTag("UserText").GetComponent<Text>().text = "xÀÆ !!";
            InvokeRepeating("Khali",0.0f,0.005f);

        }
        else if(GameObject.Find(VehicleID.Vehicle+"(Clone)").GetComponent<Chat>().isDisplayMessage == true){
            GameObject.FindWithTag("UserText").GetComponent<Text>().text = SetPlayerData.DisplayMessage;
            InvokeRepeating("Hoge",0.0f,0.005f);

        }
        
    }
    void EnableBW(bool enabled)
    {
        //coloradj.enabled.value = enabled;
    }
    // Update is called once per frame
    void Hoge(){
        if(playanim==true){
            if(mSize <=0.0f)
            {
                CancelInvoke("Hoge");
                //set camera
                
                Time.timeScale = 0;
            }
        if(cSize >=1.0f){
            CancelInvoke("Hoge");
        }        
        GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,mSize);
        transform.localScale = new Vector3(cSize, cSize, cSize);
        cSize = cSize + 2f*Time.deltaTime;
        mSize = mSize - 200f*Time.deltaTime;
        }
        
    }

    void Khali(){
        if(playanim==true){
            if(mSize <=0.0f)
            {
                CancelInvoke("Khali");
                if(GameObject.Find(VehicleID.Vehicle+"(Clone)").tag == "4Wheeler")
                {
                    GameObject.FindWithTag("Kit").GetComponent<SimpleCarController>().motorForce = 0f;
                    
                }
                else if (GameObject.Find(VehicleID.Vehicle + "(Clone)").tag == "6Wheeler")
                {
                    GameObject.FindWithTag("6Wheeler").GetComponent<SimpleCarController>().motorForce = 0f;
                    
                }
                else
                {
                    GameObject.FindWithTag("WheelFC").GetComponent<SimpleDrive>().torque = 0f;
                }
                m_Volume.sharedProfile = BW;
                //Time.timeScale = 0;
            }
        if(cSize >=1.0f){
            CancelInvoke("Khali");
        }        
        GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,mSize);
        transform.localScale = new Vector3(cSize, cSize, cSize);
        cSize = cSize + 2f*Time.deltaTime;
        mSize = mSize - 200f*Time.deltaTime;
        }
        
    }
}
