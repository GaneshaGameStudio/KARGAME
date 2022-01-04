using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{   
    public GameObject popup;
    // Start is called before the first frame update
    void OnEnable()
    {   
        print(PlayerPrefs.GetString("Popupmessage"));
        popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Popupmessage");
        
        //animation here later
    }
    public void btn_actv(string message){
        PlayerPrefs.SetString("Popupmessage", message);
        popup.SetActive(true);
        print("activated");
        
    }
    public void btn_click(){
        GameObject.Find("PopUp").SetActive(false);
    }
    // Update is called once per frame
    void Start(){
        //popup.SetActive(false);
    }
    
}
