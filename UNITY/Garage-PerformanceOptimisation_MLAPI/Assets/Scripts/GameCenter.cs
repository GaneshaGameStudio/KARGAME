using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCenter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_IOS
        Debug.Log(SystemInfo.operatingSystem);
        if(SystemInfo.operatingSystem.Contains("iOS")){
            // Authenticate and register a ProcessAuthentication callback
        // This call needs to be made before we can proceed to other calls in the Social API
        Social.localUser.Authenticate (ProcessAuthentication);
        StartCoroutine(KeepCheckingAvatarIOS());
        }
        #endif

        #if UNITY_ANDROID
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name=="Classroom"){
            Debug.Log(SystemInfo.operatingSystem);
            StartCoroutine(KeepCheckingAvatar());
        }
        #endif
    
}

void OnEnable()
    {
        #if UNITY_ANDROID
        Debug.Log(SystemInfo.operatingSystem);
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        
            StartCoroutine(KeepCheckingAvatar());
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>{

            Debug.Log("Username is"+Social.Active.localUser.userName);
            Debug.Log("width is"+Social.Active.localUser.image.width);
            Rect rect = new Rect (0, 0, Social.Active.localUser.image.width, Social.Active.localUser.image.width);
            GetComponent<Image>().sprite = Sprite.Create(Social.Active.localUser.image, rect, new Vector2(0, 0));
    });
        #endif
    }


// This function gets called when Authenticate completes
    // Note that if the operation is successful, Social.localUser will contain data from the server. 
    void ProcessAuthentication (bool success) {
        if (success) {
            Debug.Log ("Authenticated, checking achievements");
            Debug.Log(Social.Active.localUser.userName);
            Debug.Log("UniqueID="+SystemInfo.deviceUniqueIdentifier);
            Rect rect = new Rect (0, 0, Social.Active.localUser.image.width, Social.Active.localUser.image.width);
            GetComponent<Image>().sprite = Sprite.Create(Social.Active.localUser.image, rect, new Vector2(0, 0));
        }
        else
            Debug.Log ("Failed to authenticate");
    
    }
    

    private IEnumerator KeepCheckingAvatarIOS()
    {
        float secondsOfTrying = 10;
        float secondsPerAttempt = 0.2f;
        while (secondsOfTrying > 0)
        {
            Rect rect = new Rect (0, 0, Social.Active.localUser.image.width, Social.Active.localUser.image.width);
            GetComponent<Image>().sprite = Sprite.Create(Social.Active.localUser.image, rect, new Vector2(0, 0));
            
            Debug.Log(Social.Active.localUser.userName);
            secondsOfTrying -= secondsPerAttempt;
            yield return new WaitForSeconds(secondsPerAttempt);
        }
    }

    private IEnumerator KeepCheckingAvatar()
    {
        float secondsOfTrying = 10;
        float secondsPerAttempt = 0.2f;
        while (Social.Active.localUser.image == null)
        {
                yield return null;
        }
        Rect rect = new Rect (0, 0, Social.Active.localUser.image.width, Social.Active.localUser.image.width);
        GetComponent<Image>().sprite = Sprite.Create(Social.Active.localUser.image, rect, new Vector2(0, 0));
        Debug.Log(Social.Active.localUser.userName);
    }

    }
