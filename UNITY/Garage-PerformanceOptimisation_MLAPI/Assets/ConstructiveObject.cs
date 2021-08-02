using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using MLAPI;

public class ConstructiveObject : NetworkBehaviour, IPointerClickHandler
{   
    private int GarbageP = 200;
    private TextMeshProUGUI MoneyPro;
    // Start is called before the first frame update
    void Awake()
    {   
        //MoneyP.Value = PlayerPrefs.GetInt("MoneyPocket");
    }
    
    public void OnPointerClick (PointerEventData eventData)
     {  
        SetMoney();
        Destroy(gameObject);
        //DestroyDeadServerRpc();

     }
    [ServerRpc(RequireOwnership=false)]
    void DestroyDeadServerRpc(){
        Destroy(gameObject.transform.root.gameObject);
    }
    void OnCollisionEnter(Collision col){
           gameObject.transform.root.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.root.gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.transform.root.gameObject.GetComponent<Collider>().isTrigger = true;
        
    }
    
    void SetMoney(){
        PlayerPrefs.SetInt("MoneyPocket", Mathf.Min(2000,PlayerPrefs.GetInt("MoneyPocket") + GarbageP));
        GameObject.Find("Money-number").GetComponent<TextMeshProUGUI>().SetText(PlayerPrefs.GetInt("MoneyPocket").ToString());
    }
    

}
