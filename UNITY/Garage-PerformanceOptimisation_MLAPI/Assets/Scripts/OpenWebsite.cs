using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class OpenWebsite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick (PointerEventData eventData)
     {  
        Debug.Log("Click worked");

        // MoneyLeft = Mathf.Max(MoneyLeft - Moneyperlife,0);
        // if(MoneyLeft!=0){
        //     bellanim.SetTrigger("Playbell");
        //     Chat.Life = Mathf.Min(Chat.Life + 1,2);
        //     if(Chat.Life == 0){
        //         GameObject.Find("Life").transform.Find("Life0").gameObject.SetActive(true);
        //     }
        //     else if(Chat.Life == 1){
        //         GameObject.Find("Life").transform.Find("Life1").gameObject.SetActive(true);
        //     }
        //     else if(Chat.Life == 2){
        //         GameObject.Find("Life").transform.Find("Life2").gameObject.SetActive(true);
        //     }
        //     MoneyPro.SetText((MoneyLeft).ToString());
        //     PlayerPrefs.SetInt("MoneyPocket", MoneyLeft);

        // }
        
     }
}
