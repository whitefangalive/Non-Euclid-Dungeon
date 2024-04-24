using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKilledChecked : MonoBehaviour
{
    private bool killed = false;
    private Transform door;
    private Transform positionToGoTo;
    public float DoorMoveSpeed = 0.5f;
    public AudioSource doorOpenSound;
    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.Find("DoorToBeRemoved").transform;
        positionToGoTo = GameObject.Find("PositionDoorGoesTo").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (killed) 
        {
            door.position = Vector3.Lerp(door.position, positionToGoTo.position, Time.deltaTime * DoorMoveSpeed);
        }
    }

    public void BossKilled() 
    { 
        killed = true;
        doorOpenSound.Play();
    }
}
