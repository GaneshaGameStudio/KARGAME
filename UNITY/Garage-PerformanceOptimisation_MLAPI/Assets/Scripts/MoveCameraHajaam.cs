using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;  
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.AddressableAssets;
public class MoveCameraHajaam : MonoBehaviour
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
    private int currentCar;
    public GameObject Audio;
    private string currentvehicle;
    public Image Fade;
    private float iterator;
    private List<GameObject> GO = new List<GameObject>();
    private Material mat; 
    private Volume m_Volume;
    public VolumeProfile Glitch;
    public VolumeProfile Default;
    private string oldVehicle;
    private string oldVehicleTag;
    private List<GameObject> Assets { get; } = new List<GameObject>();
    //activate default objects (always the first object in the tree)
    void SelectCar(int _index, List<GameObject> Goarray)
    {
        for (int i = 0; i < Goarray.Count; i++)
        {
            Goarray[i].SetActive(i == _index);
            PlayerPrefs.SetString(VehicleID.Vehicle + "_KIT", Goarray[_index].name);
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {   
        oldVehicle = VehicleID.Vehicle;
        oldVehicleTag = VehicleID.VehicleTag;
        VehicleID.Vehicle = "Vibe2009rig-redoCSY";
        VehicleID.VehicleTag = "Manushya";
        Time.timeScale = 1;
        MoveCameraModShop.mCameraIndex  = 0;
        mCurrentIndex  = 0;
        mCarIndex  = 0;
        Vector3 currentPos = Positions[0];
        Vector3 currentAngle = Rotations[0];
        Quaternion target = Quaternion.Euler(currentAngle);
        transform.position = Vector3.Lerp(transform.position,currentPos,Speed*0.0005f*Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Speed*0.0005f*Time.deltaTime);
        // Load the vehicle
        CreateAddressablesLoader.InitByNameOrLabel("Vehicles_prefabs/" + VehicleID.Vehicle, Assets, new Vector3(0f, 0f, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        
        //Instantiate(Resources.Load("Vehicles_prefabs/" + VehicleID.Vehicle), new Vector3(1.34f, 1f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        //disable unwanted 
        }
    IEnumerator Checkspawn(){
        //disable unwanted 
        //print("random");
        while(true){
            if(CreateAddressablesLoader.Instafin==true){
                GameObject.Find("Sphere").SetActive(false);
        //if(GameObject.FindGameObjectWithTag("Manushya")){
         //   GameObject.FindGameObjectWithTag("Manushya").SetActive(false);
        //}
        
        GameObject.FindWithTag(VehicleID.VehicleTag).GetComponent<Chat>().enabled = false;
        GameObject.FindWithTag(VehicleID.VehicleTag).GetComponent<VehicleINIT>().enabled = false;
        if(VehicleID.VehicleTag=="2Wheeler"){
            VehicleText.SetText(VehicleType[0]);
            GameObject.FindGameObjectWithTag("Manushya").SetActive(false);
        }
        else if(VehicleID.VehicleTag=="3Wheeler"){
            VehicleText.SetText(VehicleType[1]);
        }
        else if(VehicleID.VehicleTag=="4Wheeler"){
            VehicleText.SetText(VehicleType[2]);
        }
        else{
            VehicleText.SetText(VehicleType[4]);
        }
        
        for(int i=0;i<GameObject.FindWithTag(VehicleID.VehicleTag).transform.childCount;i++){
            if(GameObject.FindWithTag(VehicleID.VehicleTag).transform.GetChild(i).tag == "Kit"){
                GO.Add(GameObject.FindWithTag(VehicleID.VehicleTag).transform.GetChild(i).gameObject);
            };
        }
        Texture2D Tex = GO[mCurrentIndex].GetComponent<TexturesCollect>().TexturesCollection[mCarIndex];
        mat = GO[mCurrentIndex].GetComponent<Renderer>().sharedMaterial;
        mat.SetTexture("_BaseMap",Tex);
        PlayerPrefs.SetString(VehicleID.Vehicle + "_MAT", Tex.name);
        GameObject.Find("Points-number").GetComponent<TextMeshProUGUI>().SetText((PlayerPrefs.GetInt("MoneyBank")).ToString());
        break;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    void Start()
    {   
        StartCoroutine(Checkspawn());
        m_Volume = GameObject.Find("Post-process Volume").GetComponent<Volume>();
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
            StartCoroutine("StartGlitch");
            mCurrentIndex++;
            Texture2D Tex = GO[mCurrentIndex].GetComponent<TexturesCollect>().TexturesCollection[mCarIndex];
            mat = GO[mCurrentIndex].GetComponent<Renderer>().sharedMaterial;
            mat.SetTexture("_BaseMap",Tex);
            PlayerPrefs.SetString(VehicleID.Vehicle + "_MAT", Tex.name);
            SelectCar(mCurrentIndex, GO);
            
            
            //VehicleID.Vehicle = currentvehicle;
        }
        
	}
    void ATaskOnClick()
    {
        mCarIndex = 0;
        
        if(mCurrentIndex > 0)
        {   
            StartCoroutine("StartGlitch");
            mCurrentIndex--;
            Texture2D Tex = GO[mCurrentIndex].GetComponent<TexturesCollect>().TexturesCollection[mCarIndex];
            mat = GO[mCurrentIndex].GetComponent<Renderer>().sharedMaterial;
            mat.SetTexture("_BaseMap",Tex);
            PlayerPrefs.SetString(VehicleID.Vehicle + "_MAT", Tex.name);
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
            PlayerPrefs.SetString(VehicleID.Vehicle + "_MAT", Tex.name);
        }
        
    }

    void WTaskOnClick()
    {
        if(mCarIndex < GO[mCurrentIndex].GetComponent<TexturesCollect>().TexturesCollection.Length - 1){
            mCarIndex++;
            mat = GO[mCurrentIndex].GetComponent<Renderer>().sharedMaterial;
            Texture2D Tex = GO[mCurrentIndex].GetComponent<TexturesCollect>().TexturesCollection[mCarIndex];
            mat.SetTexture("_BaseMap",Tex);
            PlayerPrefs.SetString(VehicleID.Vehicle + "_MAT", Tex.name);
        }
	}
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "SceneTrigger"){
            Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 1f);
             Addressables.LoadSceneAsync(VehicleID.Scene);
        }
        
    }
    // Update is called once per frame
    void Update()
    {   
        if(MoveCameraModShop.mCameraIndex!=100){
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
            VehicleID.Vehicle = oldVehicle ;
            VehicleID.VehicleTag = oldVehicleTag ;
            Quaternion finaltarget = Quaternion.Euler(finalRotations[0]);
            transform.position = Vector3.Lerp(transform.position,finalPositions[0],Speed*Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, finaltarget,  Speed*Time.deltaTime);
            Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, iterator);
            Audio.GetComponent<AudioSource>().volume = Audio.GetComponent<AudioSource>().volume - 0.1f*iterator;
            iterator = iterator + 0.1f;
        }
        
    }
    private IEnumerator StartGlitch(){
        m_Volume.sharedProfile = Glitch;
        yield return new WaitForSeconds(0.5f);
        m_Volume.sharedProfile = Default;

    }
}
