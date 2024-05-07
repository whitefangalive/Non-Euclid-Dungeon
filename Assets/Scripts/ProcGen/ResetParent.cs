using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResetParent : MonoBehaviour
{
    private GameObject previousParent;
    private ProgressionScript progressionScript;
    private ProgressionScript progression;
    private GameObject player;


    private void Start()
    {
        progression = GameObject.Find("ProgressionManager").GetComponent<ProgressionScript>();
        player = GameObject.Find("HeadCollider");
    }

    private HashSet<GameObject> Lights = new HashSet<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HeadCollider") {
            previousParent = GetComponentInChildren<RoomDegenerator>().Parent;
            gameObject.GetComponentInChildren<RoomDegenerator>().Parent = gameObject;
            progressionScript = GameObject.Find("ProgressionManager").GetComponent<ProgressionScript>();
            if (progressionScript.onCurrentLevel()) 
            {
                if (!progression.RoomObjectsExplored.Contains(gameObject)) 
                {
                    progression.RoomObjectsExplored.Add(gameObject);
                    progressionScript.roomsExplored++;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HeadCollider")
        {
            DungeonGenerator dungGen = GetClosestRoom().gameObject.GetComponentInChildren<DungeonGenerator>();
            gameObject.GetComponentInChildren<RoomDegenerator>().Parent = dungGen.gameObject;
            gameObject.GetComponentInChildren<RoomDegenerator>().sideFromInParent = getSide(dungGen);
        }
    }
    Transform GetClosestRoom()
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = player.transform.position;
        foreach (GameObject t in rooms)
        {
            if (t != null) {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist && t.transform.gameObject != gameObject)
                {
                    tMin = t.transform;
                    minDist = dist;
                }
            }
        }
        return tMin;
    }

    int getSide(DungeonGenerator room)
    {
        int result = 0;
        GameObject[] doors = room.roomBehavior.doors;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = player.transform.position;
        int i = 0;
        foreach (GameObject t in doors)
        {
            
            if (t != null)
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    result = i;
                    minDist = dist;
                }
            }
            i++;
        }
        return result;
    }
}
