using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class TryAgain : MonoBehaviour
{
    public string sceneName;
    public HashSet<GameObject> inventory = new HashSet<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("bag") != null)
        {
            inventory = GameObject.Find("bag").GetComponent<BagScript>().inventory;
        }

    }
    public void SendToScene()
    {
        Debug.Log("Clicked Try again button");
        Destroy(GameObject.Find("Player"));

        foreach (GameObject thing in inventory)
        {
            if (thing != null && Random.Range(0, 0) == 0)
            {
                thing.AddComponent<DontDestroyOnLoad>();
            }
        }
        GetComponent<SteamVR_LoadLevel>().enabled = true;
    }
}
