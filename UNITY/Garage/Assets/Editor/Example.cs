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

}
