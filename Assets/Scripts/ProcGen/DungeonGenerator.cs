using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RoomBehavior))]
public class DungeonGeneraotr : MonoBehaviour
{
    
    private GenerationManager generationManager;
    private RoomBehavior roomBehavior;
    public GameObject[] possibleNodes;

    // Start is called before the first frame update
    void Start()
    {
        int randomNode = Random.Range(0, possibleNodes.Length);
        roomBehavior = GetComponent<RoomBehavior>();
        int rand = Random.Range(0, roomBehavior.doors.Length - 1);
        GameObject randomWall = roomBehavior.doors[rand];
        generationManager = GameObject.Find("GenerationManager").GetComponent<GenerationManager>();
        if (generationManager.CanPlaceRoom(randomWall.transform, randomWall.transform.forward, possibleNodes[randomNode].GetComponent<BoxCollider>().size.z))
        {
            if (generationManager.AmountOfRooms > 0)
            {
                Instantiate(possibleNodes[randomNode], randomWall.transform.position, Quaternion.Euler(0,
                    randomWall.transform.rotation.eulerAngles.y, 0));

                generationManager.AmountOfRooms -= 1;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}

