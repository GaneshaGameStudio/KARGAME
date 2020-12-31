using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.Presets;
using UnityEngine.Scripting;

public class Import : MonoBehaviour
{   
    [MenuItem("APIExamples/ImportAsset")]
    static void Calltheloopimport(){
        
        ImportAssetOnlyImportsSingleAsset(35,36);
        
    }

    public static void ImportAssetOnlyImportsSingleAsset(int one, int two)
    {   
        Object go;
        Object tar;
        int i,j;
        for(i=one;i<two;i++){
            for(j=0;j<35;j++){
                try
                    {
                        FileUtil.CopyFileOrDirectory("/Users/vishakhbegari/Documents/GGS/KARGAME/Blender_elements/World/Bangalore_Maps/export/map_4_"+i+"_"+j+".fbx", "Assets/Resources/Map/map_4_"+i+"_"+j+".fbx");
                        AssetDatabase.Refresh();
                    }
        
                catch (IOException)
                    {
                        print("file exists");
                    }  
                print("map_4_"+i+"_"+j);
                go = Resources.Load("DefaultMaterial");
                tar = Resources.Load("Map/map_4_"+i+"_"+j);
                
                CopyObjectSerialization(go, tar); 
            }
        }       

    }
    public static void CopyObjectSerialization(Object source, Object target)
    {
        Preset preset = new Preset(source);
        preset.ApplyTo(target);
    }
}


