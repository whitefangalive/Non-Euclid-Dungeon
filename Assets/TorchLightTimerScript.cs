using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLightTimerScript : MonoBehaviour
{
    public GameObject torch;
    private float StartTime;
    private Light lightSource;
    public float torchDelay = 1.0f;
    public float playerDistanceMultiplier = 1.0f;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.timeSinceLevelLoad;
        lightSource = torch.GetComponentInChildren<Light>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad - StartTime > (torchDelay * (Vector3.Distance(player.transform.position, torch.transform.position) * playerDistanceMultiplier)))
        {
            lightSource.enabled = true;
        }
    }
}
