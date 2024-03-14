using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followObject : MonoBehaviour
{
    //Player game object and offset
    public GameObject followee;
    private Vector3 offset;
    private Quaternion offsetRot;
    // Start is called before the first frame update
    void Start()
    {
        //Initializes the offset to be equal to camera distance from player
        offset = transform.position - followee.transform.position;
        offsetRot = transform.rotation * Quaternion.Inverse(followee.transform.rotation);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Updates camera position to be position of the player plus the initial offset.
        transform.position = followee.transform.position + offset;
        //transform.rotation = new Quaternion(transform.rotation.x, followee.transform.rotation.y + offset.y, transform.rotation.z, transform.rotation.w);
    }

}
