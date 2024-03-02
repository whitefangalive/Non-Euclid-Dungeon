using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    public int AmountOfRooms = 4;
    public GameObject[] rooms;
    public GameObject[] hallways;

    public float generationDistance = 50.0f;

    
    public bool CanPlaceRoom(Transform pos, Vector3 Direction, float prefabLength)
    {
        bool result = true;
        RaycastHit hit;
        
        if (Physics.Raycast(pos.position, Direction, out hit, prefabLength))
        {
                result = false;
                Debug.Log("Couln't Generate room at " + pos.position.ToString() + " facing " + Direction.ToString() + " Because of " + hit.transform.name + " tag " + hit.transform.tag );
                Debug.DrawRay(pos.position, Direction * (prefabLength - 0.5f), Color.red);
        }
        Debug.DrawRay(pos.position, Direction * (prefabLength - 3f), Color.green);
        return result;
    }

    public bool IsVisibleToCamera(GameObject obj)
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(obj.transform.position);
        return ((visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1) && visTest.z >= 0) || obj.GetComponent<Renderer>().isVisible;
    }
}
