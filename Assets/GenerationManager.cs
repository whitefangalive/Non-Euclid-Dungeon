using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    public int AmountOfRooms = 4;
    public bool CanPlaceRoom(Vector3 Direction, float prefabLength)
    {
        bool result = true;
        RaycastHit hit;
        Debug.DrawRay(transform.position, Direction * prefabLength, Color.yellow);
        if (Physics.Raycast(transform.position, Direction, out hit, prefabLength))
        {
            if (hit.transform.CompareTag("Room"))
            {
                result = false;
            }
        }
        return result;
    }
}
