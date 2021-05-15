using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;  

public class MoveCameraModShop : MonoBehaviour
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
    //private GameObject[] GO;
    private int currentCar;
    public GameObject Audio;
    private string currentvehicle;
    public Image Fade;
    private float iterator;
    private List<GameObject> GO = new List<GameObject>();
    private Material mat; 
    //private Animation anim;
    

    //activate default objects (always the first object in the tree)
    void SelectCar(int _index, List<GameObject> Goarray)
    {
        for (int i = 0; i < Goarray.Count; i++)
        {
            Goarray[i].SetActive(i == _index);
            //Goarray[i].name;
            //currentvehicle = Goarray.transform.GetChild(_index).gameObject.name;
            //VehicleID.Vehicle = currentvehicle;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {   
        VehicleID.Vehicle = "HN-Dio_stock";
        Time.timeScale = 1;
        mCameraIndex  = 0;
        mCurrentIndex  = 0;
        mCarIndex  = 0;
        Vector3 currentPos = Positions[0];
        Vector3 currentAngle = Rotations[0];
        Quaternion target = Quaternion.Euler(currentAngle);
        transform.position = Vector3.Lerp(transform.position,currentPos,Speed*0.0005f*Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Speed*0.0005f*Time.deltaTime);
        // Load the vehicle
        Instantiate(Resources.Load(VehicleID.Vehicle), new Vector3(1.34f, 1f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        //disable unwanted 
        GameObject.Find("Sphere").SetActive(false);
        GameObject.FindGameObjectWithTag("Manushya").SetActive(false);
        GameObject.FindWithTag("2Wheeler").GetComponent<Chat>().enabled = false;
        
        for(int i=0;i<GameObject.FindWithTag("2Wheeler").transform.childCount;i++){
            if(GameObject.FindWithTag("2Wheeler").transform.GetChild(i).tag == "Kit"){
                GO.Add(GameObject.FindWithTag("2Wheeler").transform.GetChild(i).gameObject);
            };
        }
        Texture2D Tex = GO[mCurrentIndex].GetComponent<TexturesCollect>().TexturesCollection[mCarIndex];
        mat = GO[mCurrentIndex].GetComponent<Renderer>().sharedMaterial;
        mat.SetTexture("_BaseMap",Tex);
        
        //loop though child 
        
    
    }
    void Start()
    {   
        
        iterator = 0f;
        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 0f);
        //Vector3 finalPos =finalPositions[0];
        //Vector3 finalAngle = finalRotations[0];
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
        
        if(mCurrentIndex < GO.Count - 1)
        {   
            mCurrentIndex++;
            Texture2D Tex = GO[mCurrentIndex].GetComponent<TexturesCollect>().TexturesCollection[mCarIndex];
            mat = GO[mCurrentIndex].GetComponent<Renderer>().sharedMaterial;
            mat.SetTexture("_BaseMap",Tex);
            SelectCar(mCurrentIndex, GO);
            //VehicleID.Vehicle = currentvehicle;
        }
        
	}
    void ATaskOnClick()
    {
        mCarIndex = 0;
        
        if(mCurrentIndex > 0)
        {   
            mCurrentIndex--;
            Texture2D Tex = GO[mCurrentIndex].GetComponent<TexturesCollect>().TexturesCollection[mCarIndex];
            mat = GO[mCurrentIndex].GetComponent<Renderer>().sharedMaterial;
            mat.SetTexture("_BaseMap",Tex);
            SelectCar(mCurrentIndex, GO);
            //VehicleID.Vehicle = currentvehicle;
        }
        
	}

    void STaskOnClick()
    {  
        if(mCarIndex > 0){
            mCarIndex--;
            Texture2D Tex = GO[mCurrentIndex].GetComponent<TexturesCollect>().TexturesCollection[mCarIndex];
            mat = GO[mCurrentIndex].GetComponent<Renderer>().sharedMaterial;
            mat.SetTexture("_BaseMap",Tex);
        }
        
    }

    void WTaskOnClick()
    {
        if(mCarIndex < GO[mCurrentIndex].GetComponent<TexturesCollect>().TexturesCollection.Length - 1){
            mCarIndex++;
            mat = GO[mCurrentIndex].GetComponent<Renderer>().sharedMaterial;
            Texture2D Tex = GO[mCurrentIndex].GetComponent<TexturesCollect>().TexturesCollection[mCarIndex];
            mat.SetTexture("_BaseMap",Tex);
            
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
                //VehicleText.SetText(VehicleType[mCurrentIndex]);
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
