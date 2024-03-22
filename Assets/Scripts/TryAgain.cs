using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    public string sceneName;
    public void SendToScene()
    {
        Debug.Log("Clicked Try again button");
        Destroy(GameObject.Find("Player"));
        SteamVR_LoadLevel.Begin(sceneName);
    }
}
