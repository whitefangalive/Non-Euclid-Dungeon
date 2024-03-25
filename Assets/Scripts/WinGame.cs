using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class WinGame : MonoBehaviour
{
    public HashSet<GameObject> inventory = new HashSet<GameObject>();
    public bool winRightNow;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("bag").GetComponent<BagScript>().inventory;
    }

    // Update is called once per frame
    void Update()
    {
        if (winRightNow) 
        {
            SendToScene();
        }
    }

    public void SendToScene()
    {
        foreach (GameObject thing in inventory)
        {
            if (thing != null)
            {
                thing.AddComponent<DontDestroyOnLoad>();
            }
        }
        GetComponent<SteamVR_LoadLevel>().enabled = true;
    }
}
