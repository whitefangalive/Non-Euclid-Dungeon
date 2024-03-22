using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    //Player game object and offset
    public GameObject player;
    private Vector3 offset;
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
            if (player == null)
                if (player == null)
                {
                    player = GameObject.Find("FallbackObjects");
                }
            offset = transform.position - player.transform.position;
            forwardOnY = player.transform;
        }
        // add extra distance if you're looking down, this is so if you grab directly below you wont grab the backpack
        float extraDistance = 1;
        if (isInBetweenAngle(player.transform.rotation.x, 50, 130))
        {
            extraDistance = 1.5f;
        }
        //Updates camera position to be position of the player plus the initial offset.
        forwardOnY.rotation = new Quaternion(0, player.transform.rotation.y, 0, player.transform.rotation.w);
        
        Ray r = new Ray(player.transform.position, forwardOnY.TransformDirection(Vector3.forward));
        Debug.DrawRay(player.transform.position, forwardOnY.TransformDirection(Vector3.forward) * (offset.z * extraDistance));

        Vector3 desiredPosition = r.GetPoint(offset.z * extraDistance);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;
    }

    private bool isInBetweenAngle(float playerAngle, float minAngle, float maxAngle) 
    {
        bool result = false;
        // Adjust angles to be in the range [0, 360)
        playerAngle = (playerAngle + 360) % 360;
        minAngle = (minAngle + 360) % 360;
        maxAngle = (maxAngle + 360) % 360;

        // Check if playerAngle is between minAngle and maxAngle
        if ((minAngle <= maxAngle && playerAngle >= minAngle && playerAngle <= maxAngle) ||
            (minAngle > maxAngle && (playerAngle >= minAngle || playerAngle <= maxAngle)))
        {
            result = true;
        }
        return result;
     }

}
