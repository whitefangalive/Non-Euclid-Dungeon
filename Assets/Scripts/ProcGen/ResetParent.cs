using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetParent : MonoBehaviour
{
    private GameObject previousParent;
    private ProgressionScript progressionScript;
    public float torchDelay = 1.0f;
    public float playerDistanceMultiplier = 1.0f;

    private HashSet<GameObject> Lights = new HashSet<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HeadCollider") {
            previousParent = GetComponentInChildren<RoomDegenerator>().Parent;
            gameObject.GetComponentInChildren<RoomDegenerator>().Parent = gameObject;
            progressionScript = GameObject.Find("ProgressionManager").GetComponent<ProgressionScript>();
            if (progressionScript.onCurrentLevel()) 
            {
                bool newLevel = false;
                foreach (GameObject lightObject in Lights) 
                {
                    Light lightSource = lightObject.GetComponentInChildren<Light>();
                    if (!lightSource.enabled) 
                    {
                        newLevel = true;
                        float then = Time.timeSinceLevelLoad;

                        if (Time.timeSinceLevelLoad - then > (torchDelay * (Vector3.Distance(other.transform.position, lightObject.transform.position) * playerDistanceMultiplier)))
                        {
                            lightSource.enabled = true;
                        }
                    }
                }
                if (newLevel)
                {
                    progressionScript.roomsExplored++;
                }
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
    private void OnTriggerStay(Collider other)
    {
        Light lightSource = other.transform.gameObject.GetComponentInChildren<Light>();
        if (lightSource != null) 
        {
            Lights.Add(other.transform.gameObject);
            lightSource.enabled = false;
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
                if (dist < minDist && t.transform.gameObject != gameObject)
                {
                    tMin = t.transform;
                    minDist = dist;
                }
            }
        }
        return tMin;
    }
}
