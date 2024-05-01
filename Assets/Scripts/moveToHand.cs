using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class moveToHand : MonoBehaviour
{
    private Transform handle;
    public bool onBack = true;
    public bool handReaching = false;
    private GameObject parent;
    private GameObject bag;
    private bool inHand = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        handle = GameObject.Find("BagHandle").transform;
        parent = handle.parent.gameObject;
        bag = GameObject.Find("bag");
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "Sphere (2)")
        {
            handReaching = true;
            handle.transform.position = other.transform.position;
        }
        if (other.transform.name == "BagHandle" && inHand == false)
        {
            parent.transform.parent = gameObject.transform;
            if (handReaching == false) 
            {
                onBack = true;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Sphere (2)")
        {
            handReaching = false;
        }
    }

    public void grabbed()
    {
        inHand = true;
        onBack = false;
        parent.transform.parent = null;
        bag.GetComponent<MeshRenderer>().enabled = true;
    }
    public void unGrabbed() 
    {
        inHand = false;
    }

    private void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.Find("VRCamera");
        }
        if (onBack && handReaching == false) 
        {

            float extraDistance = 0;
            // add extra distance if you're looking down, this is so if you grab directly below you wont grab the backpack
            if (isInBetweenAngle(player.transform.rotation.eulerAngles.x, 75, 130))
            {
                extraDistance = -0.5f;
            }
            Transform forwardOnY = player.transform;
            forwardOnY.rotation = new Quaternion(0, player.transform.rotation.y, 0, player.transform.rotation.w);
            Ray r = new Ray(transform.position, forwardOnY.TransformDirection(Vector3.forward));
            Debug.DrawRay(transform.position, forwardOnY.TransformDirection(Vector3.forward) * (extraDistance));
            handle.transform.position = r.GetPoint(extraDistance);
            bag.GetComponent<MeshRenderer>().enabled = false;
        }
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
