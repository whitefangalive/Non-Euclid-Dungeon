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
        if (GameObject.Find("bag") != null) 
        {
            inventory = GameObject.Find("bag").GetComponent<BagScript>().inventory;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (winRightNow) 
        {
            SendToScene(0);
        }
    }

    public void SendToScene(int chanceToLoseItems)
    {
        foreach (GameObject thing in inventory)
        {
            if (thing != null && Random.Range(0, chanceToLoseItems) == 1)
            {
                thing.AddComponent<DontDestroyOnLoad>();
            }
        }
        GetComponent<SteamVR_LoadLevel>().enabled = true;
    }
}
