using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    public int AmountOfRooms = 4;
    public GameObject[] rooms;
    public GameObject[] hallways;
    public bool CanPlaceRoom(Transform pos, Vector3 Direction, float prefabLength)
    {
        bool result = true;
        RaycastHit hit;
        Debug.DrawRay(pos.position, Direction * prefabLength, Color.yellow);
        if (Physics.Raycast(pos.position, Direction, out hit, prefabLength))
        {
            if (hit.transform.CompareTag("Room"))
            {
                result = false;
            }
        }
        return result;
    }
}
