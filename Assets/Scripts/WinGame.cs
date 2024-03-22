using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class WinGame : MonoBehaviour
{
    public HashSet<GameObject> inventory = new HashSet<GameObject>();
    private BagScript bagScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendToScene()
    {
        Debug.Log("Clicked Try again button");
        Destroy(GameObject.Find("Player"));
        SteamVR_LoadLevel.Begin("StartScene");
    }
}
