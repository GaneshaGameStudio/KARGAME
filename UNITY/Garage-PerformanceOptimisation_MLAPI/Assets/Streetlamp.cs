using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streetlamp : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable(){
        if(int.Parse(DisplayTime.wordstime[1])>7 || int.Parse(DisplayTime.wordstime[1])<=17){
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    
}
