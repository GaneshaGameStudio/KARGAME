using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class Example
{
    // Creates a new menu item 'Examples > Create Prefab' in the main menu.
    [MenuItem("Examples/Create Prefab")]
    static void CreatePrefab()
    {
        
        GameObject[] folderObjects;

        folderObjects = Resources.LoadAll<GameObject>("BusRoutes_prefabs");

        foreach (GameObject par in folderObjects)
        {   
            EditorUtility.SetDirty(par);
            par.GetComponent<MeshRenderer>().enabled = true;
            for (int i = 0; i < par.transform.childCount; i++){
                GameObject childobject = par.transform.GetChild(i).gameObject;
                childobject.GetComponent<MeshRenderer>().enabled = false;
                
                Debug.Log(childobject.name);
            }
            //PrefabUtility.SaveAsPrefabAsset(par,"BusRoutes_prefabs_mod");
            //Destroy(par);
                
        }
            
    }
    [MenuItem("Examples/Create Collider")]
    static void Createcollider()
    {
        
        GameObject[] folderObjs;
        folderObjs = null;
        MeshCollider MC;
        MeshCollider MCP;
        PhysicMaterial phymMaterial = null;
        phymMaterial = (PhysicMaterial)Resources.Load("road");

        folderObjs = Resources.LoadAll<GameObject>("Roads_prefabs");

        //int j = 0;

       // foreach (GameObject pars in folderObjs)
       for(int k=0; k<folderObjs.Length; k++)
        {
            GameObject pars = folderObjs[k];
           // j++;
            
            EditorUtility.SetDirty(pars);
            pars.AddComponent<MeshCollider>();
            MCP = pars.GetComponent<MeshCollider>();
            MCP.sharedMaterial = phymMaterial;
            //par.AddComponent<MeshCollider>();
            Debug.Log(pars.name);
            for (int i = 0; i < pars.transform.childCount; i++){
                GameObject childobjects = pars.transform.GetChild(i).gameObject;
                EditorUtility.SetDirty(childobjects);
                childobjects.AddComponent<MeshCollider>();
                MC = childobjects.GetComponent<MeshCollider>();

                MC.sharedMaterial = phymMaterial;
               // Debug.Log(childobjects.name);
            }

            /*if (j >= 2)
            {
                Debug.Log(j);
                break;
            }*/

            //PrefabUtility.SaveAsPrefabAsset(pars,"Roads_prefabs");
            //break;
            //Destroy(par);

        }
            
    }

}
