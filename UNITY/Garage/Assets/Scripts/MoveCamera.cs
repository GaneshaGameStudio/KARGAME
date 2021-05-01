using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;  

public class MoveCamera : MonoBehaviour
{
    public Vector3[] Positions;
    public Vector3[] Rotations;
    public Vector3[] finalPositions;
    public Vector3[] finalRotations;
    static public int mCurrentIndex = 0;
    static public int mCameraIndex = 0;
    static public int mCarIndex  = 0;
    public float Speed = 2.0f;
    public Button W_Button;
    public Button A_Button;
    public Button S_Button;
    public Button D_Button;
    public Button LeftTrigger_Button;
    public Button RightTrigger_Button;
    public string[] VehicleType;
    public TextMeshProUGUI VehicleText;
    public GameObject[] GO;
    private int currentCar;
    public GameObject Audio;
    private string currentvehicle;
    public Image Fade;
    private float iterator;
    //private Animation anim;
    

    //activate default objects (always the first object in the tree)
    void SelectCar(int _index, GameObject Goarray)
    {
        for (int i =0; i < Goarray.transform.childCount; i++)
        {
            Goarray.transform.GetChild(i).gameObject.SetActive(i == _index);
            currentvehicle = Goarray.transform.GetChild(_index).gameObject.name;
            VehicleID.Vehicle = currentvehicle;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {   
        Time.timeScale = 1;
        mCameraIndex  = 0;
        mCurrentIndex  = 0;
        mCarIndex  = 0;
        Vector3 currentPos = Positions[0];
        Vector3 currentAngle = Rotations[0];
        Quaternion target = Quaternion.Euler(currentAngle);
        transform.position = Vector3.Lerp(transform.position,currentPos,Speed*0.0005f*Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Speed*0.0005f*Time.deltaTime);
        
        for(int j =0; j < GO.Length; j++)
        {
            SelectCar(0, GO[j]);

        }
        //SelectCar(mCarIndex, GO[mCurrentIndex]);
        
        
    }
    void Start()
    {   
        iterator = 0f;
        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 0f);
        Vector3 finalPos =finalPositions[0];
        Vector3 finalAngle = finalRotations[0];
        Button Wbtn = W_Button.GetComponent<Button>();
		Button Abtn = A_Button.GetComponent<Button>();
        Button Sbtn = S_Button.GetComponent<Button>();
        Button Dbtn = D_Button.GetComponent<Button>();

        Button Lbtn = LeftTrigger_Button.GetComponent<Button>();
        Button Rbtn = RightTrigger_Button.GetComponent<Button>();

        Wbtn.onClick.AddListener(WTaskOnClick);
        Abtn.onClick.AddListener(ATaskOnClick);
        Sbtn.onClick.AddListener(STaskOnClick);
		Dbtn.onClick.AddListener(DTaskOnClick);
        Rbtn.onClick.AddListener(RightClick);

        currentvehicle = GO[mCurrentIndex].transform.GetChild(mCarIndex).gameObject.name;
        VehicleID.Vehicle = currentvehicle;
    }
    void RightClick(){
        AudioSource audio = Audio.GetComponent<AudioSource>();
        if (audio.mute)
                audio.mute = false;
            else
                audio.mute = true;
    }
	void DTaskOnClick()
    {   
        mCarIndex = 0;
        if(mCurrentIndex < Positions.Length - 1)
        {
            mCurrentIndex++;
            currentvehicle = GO[mCurrentIndex].transform.GetChild(mCarIndex).gameObject.name;
            VehicleID.Vehicle = currentvehicle;
        }
        
	}
    void ATaskOnClick()
    {
        mCarIndex = 0;
        if(mCurrentIndex > 0)
        {
            mCurrentIndex--;
            currentvehicle = GO[mCurrentIndex].transform.GetChild(mCarIndex).gameObject.name;
            VehicleID.Vehicle = currentvehicle;
        }
        
	}

    void STaskOnClick()
    {   
        if(mCarIndex < GO[mCurrentIndex].transform.childCount - 1)
        {
            mCarIndex++;
            SelectCar(mCarIndex, GO[mCurrentIndex]);
        }
    }

    void WTaskOnClick()
    {
        if(mCarIndex > 0)
        {
            mCarIndex--;
            SelectCar(mCarIndex, GO[mCurrentIndex]);
            
        }
	}
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "SceneTrigger"){
            Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 1f);
            SceneManager.LoadScene(VehicleID.Scene);
        }
        
    }
    // Update is called once per frame
    void Update()
    {   
        if(mCameraIndex!=100){
            if(GameObject.Find("Stats")){
                //VehicleText.SetText(VehicleType[1]);
                Vector3 currentPos = Positions[2];
                Vector3 currentAngle = Rotations[2];
                transform.position = Vector3.Lerp(transform.position,currentPos,Speed*Time.deltaTime);
                Quaternion target = Quaternion.Euler(currentAngle);
                transform.rotation = Quaternion.Slerp(transform.rotation, target,  Speed*Time.deltaTime);
            }
            else{
                VehicleText.SetText(VehicleType[mCurrentIndex]);
                Vector3 currentPos = Positions[mCurrentIndex];
                Vector3 currentAngle = Rotations[mCurrentIndex];
                transform.position = Vector3.Lerp(transform.position,currentPos,Speed*Time.deltaTime);
                Quaternion target = Quaternion.Euler(currentAngle);
                transform.rotation = Quaternion.Slerp(transform.rotation, target,  Speed*Time.deltaTime);
            }
            
        }
        else{
            Quaternion finaltarget = Quaternion.Euler(finalRotations[0]);
            transform.position = Vector3.Lerp(transform.position,finalPositions[0],Speed*Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, finaltarget,  Speed*Time.deltaTime);
            Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, iterator);
            Audio.GetComponent<AudioSource>().volume = Audio.GetComponent<AudioSource>().volume - 0.1f*iterator;
            iterator = iterator + 0.1f;
        }
        
    }
}
