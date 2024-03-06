using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionScript : MonoBehaviour
{
    public int roomsExplored = 0;
    public float height;
    private Transform player;
    public bool TimeToProgress = false;

    public GameObject StairAsset;
    public int level = 0;

    private float difference;

    public int AmountOfRoomsLevelOne = 25;

    public int HeightLevelAchieved = 0;
    private void Start()
    {
        player = GameObject.Find("FollowHead").transform;
        RoomBehavior room = StairAsset.GetComponentInChildren<RoomBehavior>();
        difference = (room.doors[0].transform.position.y - room.doors[1].transform.position.y);
    }

    private void LateUpdate()
    {
        height = player.position.y;
        
        
        level = Mathf.RoundToInt(height / difference);
        if (level > HeightLevelAchieved) 
        {
            HeightLevelAchieved = level;
        }

        if (roomsExplored == AmountOfRoomsLevelOne && level == 0)
        {
            TimeToProgress = true;
            roomsExplored = 0;
        }
    }

    public bool onCurrentLevel()
    {
        bool result = false;
        if (level == HeightLevelAchieved) 
        {
            result = true;
        }
        return result;
    }
}
