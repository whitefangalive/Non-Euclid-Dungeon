using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] walls; // 0 up 1 down 2 right 3 left
    // Start is called before the first frame update
    public bool[] testStatus;
    void Start()
    {
        UpdateRoom(testStatus);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}
