using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using System.Collections;

public class GameCenter : MonoBehaviour {
    
    void Start () {
        // Authenticate and register a ProcessAuthentication callback
        // This call needs to be made before we can proceed to other calls in the Social API
        Social.localUser.Authenticate (ProcessAuthentication);
        StartCoroutine(KeepCheckingAvatar());
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

    private IEnumerator KeepCheckingAvatar()
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

}