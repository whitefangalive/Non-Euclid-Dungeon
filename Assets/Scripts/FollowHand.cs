using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    //Player game object and offset
    public GameObject player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    private Transform forwardOnY;

    // Start is called before the first frame update
    void Start()
    {

        //Initializes the offset to be equal to camera distance from player
        offset = transform.position - player.transform.position;
        forwardOnY = player.transform;
    }
    

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.Find("VRCamera");
        }
        forwardOnY = player.transform;

        //Updates camera position to be position of the player plus the initial offset.
        forwardOnY.rotation = new Quaternion(0, player.transform.rotation.y, 0, player.transform.rotation.w);
        
        Ray r = new Ray(player.transform.position, forwardOnY.TransformDirection(Vector3.forward));
        Debug.DrawRay(player.transform.position, forwardOnY.TransformDirection(Vector3.forward) * (offset.z));

        Vector3 desiredPosition = r.GetPoint(offset.z);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;
    }

    

}
