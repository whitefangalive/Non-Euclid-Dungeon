using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartLocation : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.transform.position = transform.position;
    }
}
