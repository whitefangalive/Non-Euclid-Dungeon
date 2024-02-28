using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneraotr : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject[] hallways;

    private GenerationManager generationManager;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomDirection = new Vector3(0, 0, 1);
        generationManager = GameObject.Find("GenerationManager").GetComponent<GenerationManager>();
        if (generationManager.CanPlaceRoom(Vector3.forward, rooms[0].GetComponent<Renderer>().bounds.size.x))
        {
            if (generationManager.AmountOfRooms > 0)
            {
                Instantiate(rooms[1], new Vector3(transform.position.x + (((rooms[0].GetComponent<Renderer>().bounds.size.x) / 2) + (rooms[1].GetComponent<Renderer>().bounds.size.x) / 2), 0, 0), Quaternion.identity);
                generationManager.AmountOfRooms -= 1;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Cannot think of better way to do this but think it could be simplified to single line of code
    Vector3 randomDirection()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                return new Vector3(0, 0, 1);
                break;
            case 1:
                return new Vector3(0, 0, -1);
            default:
                return new Vector3(-1, 0, 0);
        }
    }

}

