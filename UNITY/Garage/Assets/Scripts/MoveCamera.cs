using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveCamera : MonoBehaviour
{
    public Vector3[] Positions;
    public Vector3[] Rotations;
    private int mCurrentIndex = 0;
    private int mCarIndex  = 0;
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
    //private Animation anim;
    

    //activate default objects (always the first object in the tree)
    private void SelectCar(int _index, GameObject Goarray)
    {
        for (int i =0; i < Goarray.transform.childCount; i++)
        {
            Goarray.transform.GetChild(i).gameObject.SetActive(i == _index);
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {   
        for(int j =0; j < GO.Length; j++)
        {
            SelectCar(mCarIndex, GO[j]);
        }
        
    }
    void Start()
    {   
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
        Lbtn.onClick.AddListener(LeftClick);
    }
    void LeftClick(){
        AudioSource audio = Audio.GetComponent<AudioSource>();
        if (audio.mute)
                audio.mute = false;
            else
                audio.mute = true;
    }
	void DTaskOnClick()
    {   
        //anim.Play("L");
		Debug.Log ("You have clicked the D button!");
        mCarIndex = 0;
        if(mCurrentIndex < Positions.Length - 1)
        {
            mCurrentIndex++;
        }
        
	}
    void ATaskOnClick()
    {
		Debug.Log ("You have clicked the A button!");
        mCarIndex = 0;
        if(mCurrentIndex > 0)
        {
            mCurrentIndex--;
        }
        
	}

    void STaskOnClick()
    {   
		Debug.Log ("You have clicked the S button!");
        if(mCarIndex < GO[mCurrentIndex].transform.childCount - 1)
        {
            mCarIndex++;
            SelectCar(mCarIndex, GO[mCurrentIndex]);
        }
    }

    void WTaskOnClick()
    {
		Debug.Log ("You have clicked the W button!");
        if(mCarIndex > 0)
        {
            mCarIndex--;
            SelectCar(mCarIndex, GO[mCurrentIndex]);
            
        }
	}
    // Update is called once per frame
    void Update()
    {   
        VehicleText.SetText(VehicleType[mCurrentIndex]);
        Vector3 currentPos = Positions[mCurrentIndex];
        Vector3 currentAngle = Rotations[mCurrentIndex];
        transform.position = Vector3.Lerp(transform.position,currentPos,Speed*Time.deltaTime);
        Quaternion target = Quaternion.Euler(currentAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Speed*Time.deltaTime);
    
    }
}
