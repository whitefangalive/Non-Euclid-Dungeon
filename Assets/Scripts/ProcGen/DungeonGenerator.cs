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
    public bool SpawnStairs = false;
    public int maxNodes;
    public int currentPlacedNodes = 0;
    private int rand = -1;
    private ProgressionScript progression;

    // Start is called before the first frame update
    void Start()
    {
        roomBehavior = GetComponent<RoomBehavior>();
        generationManager = GameObject.Find("GenerationManager").GetComponent<GenerationManager>();
        maxNodes = Mathf.Clamp(roomBehavior.doors.Length - 1, 0, 2);
        progression = GameObject.Find("ProgressionManager").GetComponent<ProgressionScript>();
    }


    // Update is called once per frame
    void Update()
    {
        int randomNode = Random.Range(0, possibleNodes.Length - 1);
        progressionEnforcer();
        if (SpawnStairs) 
        {
            randomNode = possibleNodes.Length - 1;
        }

        if (currentPlacedNodes < maxNodes && generationManager.IsVisibleToCamera(gameObject) && (generationManager.AmountOfRooms > 0 || SpawnStairs) && Vector3.Distance(transform.position, Camera.main.transform.position) < generationManager.generationDistance) {
            SpawnStairs = false;

            rand = Random.Range(0, roomBehavior.doors.Length);
            GameObject randomWall = roomBehavior.doors[rand];
            if (generationManager.CanPlaceRoom(randomWall.transform, randomWall.transform.forward, possibleNodes[randomNode].GetComponent<BoxCollider>().size.z))
            {
                 generateRoom(randomWall, randomNode);
            }
            else
            {
                rand = randomNumberThatIsnt(rand, 0, roomBehavior.doors.Length);
                randomWall = roomBehavior.doors[rand];
                if (generationManager.CanPlaceRoom(randomWall.transform, randomWall.transform.forward, possibleNodes[randomNode].GetComponent<BoxCollider>().size.z))
                {
                    generateRoom(randomWall, randomNode);
                }
            }
        }
    }

    private void progressionEnforcer() 
    {
       if (progression.TimeToProgress) 
        {
            SpawnStairs = true;
        }
    }
    private void generateRoom(GameObject randomWall, int randomNode)
    {
        roomBehavior.UpdateRoomWall(true, rand);
        generationManager.AmountOfRooms -= 1;
        currentPlacedNodes++;
        GameObject nextRoom = Instantiate(possibleNodes[randomNode], randomWall.transform.position, Quaternion.Euler(0,
        randomWall.transform.rotation.eulerAngles.y, 0));
        nextRoom.name = name + generationManager.AmountOfRooms.ToString();
        nextRoom.GetComponentInChildren<RoomDegenerator>().Parent = gameObject;
        
    }

    private int randomNumberThatIsnt(int number, int min, int max)
    {
        int result = number;
        while (result == number) 
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
            if (generationManager.CanPlaceRoom(wall.transform, wall.transform.forward, 1.0f))
            {
                roomBehavior.UpdateRoomWall(false, i);
            }
        }
    }
}

