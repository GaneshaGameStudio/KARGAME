using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using MLAPI;

public class Coffin : NetworkBehaviour, IPointerClickHandler
{   

    NetworkVariableInt MoneyP = new NetworkVariableInt(2);
    public NetworkVariableBool Dead = new NetworkVariableBool(false);
    private TextMeshProUGUI MoneyPro;
    // Start is called before the first frame update
    void Awake()
    {   
        MoneyP.Value = PlayerPrefs.GetInt("MoneyPocket");
        
    }
    
    public void OnPointerClick (PointerEventData eventData)
     {  
        SetMoney();
        Dead.Value = true;
        DestroyDeadServerRpc();

     }
    [ServerRpc(RequireOwnership=false)]
    void DestroyDeadServerRpc(){
        Destroy(gameObject.transform.root.gameObject);
    }
    void SetMoney(){
        PlayerPrefs.SetInt("MoneyPocket", Mathf.Max(2000,PlayerPrefs.GetInt("MoneyPocket") + MoneyP.Value));
        GameObject.Find("Money-number").GetComponent<TextMeshProUGUI>().SetText((MoneyP.Value).ToString());
    }
    

}
