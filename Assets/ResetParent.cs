using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetParent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") {
            gameObject.GetComponentInChildren<RoomDegenerator>().Parent = gameObject;
        }
    }
}
