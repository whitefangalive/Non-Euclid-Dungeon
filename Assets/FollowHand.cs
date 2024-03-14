using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    //Player game object and offset
    public GameObject player;
    private Vector3 offset;
    public float smoothSpeed = 0.125f;
    // Start is called before the first frame update
    void Start()
    {
        //Initializes the offset to be equal to camera distance from player
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //Updates camera position to be position of the player plus the initial offset.
        Vector3 direction = Vector3.Scale(player.transform.TransformDirection(Vector3.forward), new Vector3(1, 0, 1));
        Ray r = new Ray(player.transform.position, direction);
        Debug.DrawRay(player.transform.position, direction * offset.z);

        Vector3 desiredPosition = r.GetPoint(offset.z);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;
    }
}
