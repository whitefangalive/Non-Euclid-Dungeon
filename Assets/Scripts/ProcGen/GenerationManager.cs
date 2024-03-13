using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    public int AmountOfRooms = 4;
    public GameObject[] rooms;
    public GameObject[] hallways;

    public float generationDistance = 50.0f;
    public int ChanceToDespawn = 10;

    private Vector3 center;
    private Vector3 scale;
    private Quaternion rotation;
    private int count;
    private Collider[] m_HitDetect = new Collider[16];
    RaycastHit hit;
    Vector3 forward;
    public LayerMask m_LayerMask;

    public bool CanPlaceRoom(GameObject door, GameObject node, QueryTriggerInteraction collide)
    {
        bool result = true;

        rotation = door.transform.rotation;
        forward = door.transform.forward;

        scale = RotateVector(node.GetComponent<BoxCollider>().size / 1.25f, rotation);
        Vector3 absScale = new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));
        center = door.transform.position + (1.0f * door.transform.forward) + ((Vector3.Scale(door.transform.forward, absScale) / 2));
        
        Debug.DrawRay(center, Vector3.Scale(absScale / 2, forward), Color.red);
        
        count = Physics.OverlapBoxNonAlloc(center, absScale / 2, m_HitDetect, Quaternion.identity, m_LayerMask, collide);
        if (count > 0)
        {
            result = false;
        }
        
        return result;
    }
    void OnDrawGizmos()
    {
        if (count > 0) 
        {

            Gizmos.color = Color.red;
        }
        Vector3 absScale = new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));
        Gizmos.DrawWireCube(center, absScale);
       
    }
    Vector3 RotateVector(Vector3 vector, Quaternion rotation)
    {
        return rotation * vector;
    }
    public bool CanPlaceRoomRayCast(Transform pos, Vector3 Direction, float prefabLength)
    {
        bool result = true;
        RaycastHit hit;

        if (Physics.Raycast(pos.position, Direction, out hit, prefabLength))
        {
            result = false;
        }
        return result;
    }


    public bool IsVisibleToCamera(GameObject obj)
    {
        return obj.GetComponent<Renderer>().isVisible || obj.GetComponentInChildren<Renderer>().isVisible;
    }
}
