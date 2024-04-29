using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFollowsPlayer : MonoBehaviour
{
    private CapsuleCollider collider;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        player = GameObject.Find("VRCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        collider.center = new Vector3(player.localPosition.x, collider.center.y, player.localPosition.z);
    }
}
