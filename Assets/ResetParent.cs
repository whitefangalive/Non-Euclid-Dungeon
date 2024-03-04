using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetParent : MonoBehaviour
{
    private GameObject previousParent;
    private ProgressionScript progressionScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HeadCollider") {
            previousParent = GetComponentInChildren<RoomDegenerator>().Parent;
            gameObject.GetComponentInChildren<RoomDegenerator>().Parent = gameObject;
            progressionScript = GameObject.Find("ProgressionManager").GetComponent<ProgressionScript>();
            if (progressionScript.onCurrentLevel()) 
            {
                progressionScript.roomsExplored++;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HeadCollider")
        {
            gameObject.GetComponentInChildren<RoomDegenerator>().Parent = GetClosestRoom().gameObject.GetComponentInChildren<DungeonGenerator>().gameObject;
        }
    }

    Transform GetClosestRoom()
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = GameObject.Find("HeadCollider").transform.position;
        foreach (GameObject t in rooms)
        {
            if (t != null) {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t.transform;
                    minDist = dist;
                }
            }
        }
        return tMin;
    }
}
