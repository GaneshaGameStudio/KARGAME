using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;
public class CheckPlayer : MonoBehaviour
{
    void Awake(){
        if(gameObject.transform.root.tag!="Manushya"){
            Animator anim = gameObject.GetComponent<Animator>();
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Rider");
            gameObject.GetComponent<SimpleBodyController>().enabled = false;
            gameObject.GetComponent<CharacterController>().enabled = false;
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name=="Garage"){
                gameObject.GetComponent<Animate>().enabled = false;
            }
            else{
                 gameObject.GetComponent<Animate>().enabled = true;
            }
            
            gameObject.GetComponent<Chat>().enabled = false;
            Component[] capsulecolliders;

            capsulecolliders = GetComponents(typeof(CapsuleCollider));

            foreach (CapsuleCollider cc in capsulecolliders)
                cc.enabled = false;
            
            gameObject.transform.Find("Sphere").gameObject.SetActive(false);
            Rigidbody[] allRBs = GetComponentsInChildren<Rigidbody>();
            for (int r=0;r<allRBs.Length;r++) {
                allRBs[r].isKinematic = false;
                allRBs[r].useGravity = false;
            }
            Collider[] allCCs = GetComponentsInChildren<Collider>();
            for (int r=0;r<allCCs.Length;r++) {
                      allCCs[r].enabled = false;
                }
            gameObject.GetComponent<RigBuilder>().enabled=true;
            gameObject.transform.GetChild(2).gameObject.GetComponent<Collider>().enabled=true;
            
        }
        else{
            gameObject.GetComponent<Animate>().enabled = false;
            gameObject.GetComponent<SimpleBodyController>().enabled = true;
            gameObject.GetComponent<CharacterController>().enabled = true;
            gameObject.GetComponent<Chat>().enabled = true;
            Component[] capsulecolliders;

            capsulecolliders = GetComponents(typeof(CapsuleCollider));

            foreach (CapsuleCollider cc in capsulecolliders)
                cc.enabled = false;
            
            gameObject.transform.Find("Sphere").gameObject.SetActive(true);
            Rigidbody[] allRBs = GetComponentsInChildren<Rigidbody>();
            for (int r=0;r<allRBs.Length;r++) {
                allRBs[r].isKinematic = false;
                allRBs[r].useGravity = false;
            }
            Collider[] allCCs = GetComponentsInChildren<Collider>();
            for (int r=0;r<allCCs.Length;r++) {
                      allCCs[r].enabled = false;
                }
            gameObject.GetComponent<RigBuilder>().enabled=false;
            gameObject.transform.GetChild(2).gameObject.GetComponent<Collider>().enabled=true;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = true;

        }
        
    }
}
