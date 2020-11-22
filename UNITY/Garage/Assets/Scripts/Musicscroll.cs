using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Musicscroll : MonoBehaviour
{   
    public AudioSource musicplaying;
    public TextMeshProUGUI MoosicPro;
    private string music;
    private Vector3 final;
    private float xtargetleft = -5f;
    private float xtargetright;
    private float xmove;
    // Start is called before the first frame update
    void Start()
    {   
        xmove = transform.localPosition.x;
        xtargetright = transform.localPosition.x;
        StartCoroutine(SetMusicText());
    }
    IEnumerator SetMusicText(){
        while(true){
            music = musicplaying.clip.name;
            MoosicPro.SetText(music);
            if(xmove>xtargetleft){
                while(true){
                    transform.localPosition = new Vector3(transform.localPosition.x - 5f, transform.localPosition.y, transform.localPosition.z);
                    if(transform.localPosition.x < xtargetleft){
                        print("breaking");
                        break;
                    }
                    yield return new WaitForSeconds(0.2f); 
                }
                while(true){
                  transform.localPosition = new Vector3(transform.localPosition.x + 5f, transform.localPosition.y, transform.localPosition.z);
                    if(transform.localPosition.x > xtargetright){
                        break;
                    }
                    yield return new WaitForSeconds(0.2f); 
                }
                
            }
                
            }
        } 
    
    // Update is called once per frame
    void Update()
    {   
        
          
    }
}
