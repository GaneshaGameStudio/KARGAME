using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PlayGames : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
