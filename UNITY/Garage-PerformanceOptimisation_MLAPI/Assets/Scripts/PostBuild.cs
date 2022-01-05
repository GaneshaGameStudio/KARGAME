#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.IO;

public class PostBuild
{
/// <summary>
/// Description for IDFA request notification 
/// [sets NSUserTrackingUsageDescription]
/// </summary>
const string TrackingDescription = "This allows the KARGAME app to collect Gameplay Content";
    [PostProcessBuild(0)]

public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToXcode)
{
    if (buildTarget == BuildTarget.iOS)
        {
            AddPListValues(pathToXcode);
        }
    }

static void AddPListValues(string pathToXcode)
{
    #if UNITY_IOS
    // Get Plist from Xcode project 
    string plistPath = pathToXcode + "/Info.plist";
    // Read in Plist 
    PlistDocument plistObj = new PlistDocument();
    plistObj.ReadFromString(File.ReadAllText(plistPath));
    // set values from the root obj
    PlistElementDict plistRoot = plistObj.root;
    // Set value in plist
    plistRoot.SetString("NSUserTrackingUsageDescription", TrackingDescription);
    // save
    File.WriteAllText(plistPath, plistObj.WriteToString());
    #endif
    }
}
#endif