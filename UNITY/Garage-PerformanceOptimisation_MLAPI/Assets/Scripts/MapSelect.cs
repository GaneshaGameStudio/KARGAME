using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public void Letsprintthis(string arr) {
        string[] jaaga = arr.Split(',');
        PlayerPrefs.SetFloat("SpawnLoc.x", float.Parse(jaaga[0]));
        PlayerPrefs.SetFloat("SpawnLoc.y",float.Parse(jaaga[1]));
        PlayerPrefs.SetFloat("SpawnLoc.z",float.Parse(jaaga[2]));
        PlayerPrefs.SetFloat("SpawnRot.x",float.Parse(jaaga[3]));
        PlayerPrefs.SetFloat("SpawnRot.y",float.Parse(jaaga[4]));
        PlayerPrefs.SetFloat("SpawnRot.z",float.Parse(jaaga[5]));
    }
}
