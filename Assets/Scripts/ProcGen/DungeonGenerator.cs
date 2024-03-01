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
        roomBehavior = GetComponent<RoomBehavior>();
        generationManager = GameObject.Find("GenerationManager").GetComponent<GenerationManager>();
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    void Update()
    {
/*        for (int i = 0; i < roomBehavior.doors.Length; i++) 
        {
            generationManager.CanPlaceRoom(roomBehavior.doors[i].transform, roomBehavior.doors[i].transform.forward, possibleNodes[0].GetComponent<BoxCollider>().size.z);
        }*/
    }

    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(0.5f);


        for (int i = 0; i < Mathf.Clamp(roomBehavior.doors.Length, 0, 2); i++)
        {
            int randomNode = Random.Range(0, possibleNodes.Length);

            int rand = Random.Range(0, roomBehavior.doors.Length);
            GameObject randomWall = roomBehavior.doors[rand];
            if (generationManager.CanPlaceRoom(randomWall.transform, randomWall.transform.forward, possibleNodes[randomNode].GetComponent<BoxCollider>().size.z))
            {
                if (generationManager.AmountOfRooms > 0)
                {
                    generateRoom(randomWall, randomNode);
                }

            }
            else 
            {
                rand = randomNumberThatIsnt(rand, 0, roomBehavior.doors.Length);
                randomWall = roomBehavior.doors[rand];
                if (generationManager.CanPlaceRoom(randomWall.transform, randomWall.transform.forward, possibleNodes[randomNode].GetComponent<BoxCollider>().size.z))
                {
                    if (generationManager.AmountOfRooms > 0)
                    {
                        generateRoom(randomWall, randomNode);
                    }
                }
            }
        }
    }

    private void generateRoom(GameObject randomWall, int randomNode)
    {
            GameObject nextRoom = Instantiate(possibleNodes[randomNode], randomWall.transform.position, Quaternion.Euler(0,
                randomWall.transform.rotation.eulerAngles.y, 0));
            nextRoom.name = name + generationManager.AmountOfRooms.ToString();
            generationManager.AmountOfRooms -= 1;
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
}

