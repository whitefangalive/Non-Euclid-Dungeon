using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RoomBehavior))]
[RequireComponent(typeof(RoomDegenerator))]
public class DungeonGenerator : MonoBehaviour
{

    private GenerationManager generationManager;
    private RoomBehavior roomBehavior;
    public GameObject[] possibleNodes;
    public GameObject[] specialNodes;
    public int maxNodes;
    public int currentPlacedNodes = 0;
    private int rand = -1;
    private ProgressionScript progression;

    // Start is called before the first frame update
    void Start()
    {
        roomBehavior = GetComponent<RoomBehavior>();
        generationManager = GameObject.Find("GenerationManager").GetComponent<GenerationManager>();
        maxNodes = Mathf.Clamp(roomBehavior.doors.Length - 1, 0, roomBehavior.doors.Length / 2);
        progression = GameObject.Find("ProgressionManager").GetComponent<ProgressionScript>();
    }


    // Update is called once per frame
    void Update()
    {
        GameObject node = possibleNodes[Random.Range(0, possibleNodes.Length)];

        if (progression.TimeToProgress) 
        {
            node = specialNodes[0];
        }

        if (currentPlacedNodes < maxNodes && generationManager.IsVisibleToCamera(gameObject) && (generationManager.AmountOfRooms > 0 ||
            progression.TimeToProgress) && Vector3.Distance(transform.position, Camera.main.transform.position) < generationManager.generationDistance) {

            rand = Random.Range(0, roomBehavior.doors.Length);
            GameObject randomWall = roomBehavior.doors[rand];
            if (generationManager.CanPlaceRoom(randomWall, node, QueryTriggerInteraction.Collide))
            {
                 generateRoom(randomWall, node);
            }
            else
            {
                rand = randomNumberThatIsnt(rand, 0, roomBehavior.doors.Length);
                randomWall = roomBehavior.doors[rand];
                if (generationManager.CanPlaceRoom(randomWall, node, QueryTriggerInteraction.Collide))
                {
                    generateRoom(randomWall, node);
                }
            }
        }
    }
    private void generateRoom(GameObject randomWall, GameObject node)
    {
        roomBehavior.UpdateRoomWall(true, rand);
        generationManager.AmountOfRooms -= 1;
        currentPlacedNodes++;
        GameObject nextRoom = Instantiate(node, randomWall.transform.position, Quaternion.Euler(0,
        randomWall.transform.rotation.eulerAngles.y, 0));
        nextRoom.name = name + generationManager.AmountOfRooms.ToString();
        nextRoom.GetComponentInChildren<RoomDegenerator>().Parent = gameObject;
        progression.TimeToProgress = false;

    }

    private int randomNumberThatIsnt(int number, int min, int max)
    {
        int result = number;
        while (result == number && max - min > 1) 
        {
            result = Random.Range(min, max);
        }
        return result;
    }

    public void ResetAllWalls() 
    {
        for (int i = 0; i < roomBehavior.doors.Length; i++)
        {
            GameObject wall = roomBehavior.doors[i];
            if (generationManager.CanPlaceRoomRayCast(wall.transform, wall.transform.forward, 1.0f))
            {
                roomBehavior.UpdateRoomWall(false, i);
            }
            else 
            {
                roomBehavior.UpdateRoomWall(true, i);
            }
        }
    }
}

