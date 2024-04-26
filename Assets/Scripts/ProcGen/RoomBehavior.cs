using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] walls; // 0 up 1 right 2 down 3 left
    // Start is called before the first frame update
    public bool[] testStatus;
    void Start()
    {
        UpdateRoom(testStatus);
    }
    void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            if (doors != null && walls != null) 
            {
                doors[i].SetActive(status[i]);
                walls[i].SetActive(!status[i]);
            }
        }
    }

    public void UpdateRoomWall(bool doorOpen, int wallNumber) 
    {
        doors[wallNumber].SetActive(doorOpen);
        walls[wallNumber].SetActive(!doorOpen);
    }
}
