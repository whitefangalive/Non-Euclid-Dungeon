using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public GameObject leftHand;
    private GameObject rightHand;
    private GameObject playerFeetPosition;
    // Start is called before the first frame update
    void Start()
    {
        handle = GameObject.Find("BagHandle").transform;
        parent = handle.parent.gameObject;
        bag = GameObject.Find("bag");
        leftHand = GameObject.Find("HandColliderLeft(Clone)");
        rightHand = GameObject.Find("HandColliderRight(Clone)");
        playerFeetPosition = GameObject.Find("Player");
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "Sphere (2)")
        {
            if (Vector3.Distance(playerFeetPosition.transform.position, leftHand.transform.position) >= 0.65 &&
                Vector3.Distance(playerFeetPosition.transform.position, rightHand.transform.position) >= 0.65)
            {
                handReaching = true;
                handle.transform.position = other.transform.position;
            }
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
            playerFeetPosition = GameObject.Find("Player");
        }
        if (leftHand == null) 
        {
            leftHand = GameObject.Find("HandColliderLeft(Clone)");
            rightHand = GameObject.Find("HandColliderRight(Clone)");
        }
        if (onBack && handReaching == false) 
        {
            float extraDistance = 0;
            // add extra distance if you're looking down, this is so if you grab directly below you wont grab the backpack
            
            if (isInBetweenAngle(player.transform.localRotation.x, -75, -170))
            {
                extraDistance = -0.5f;
                if (Vector3.Distance(playerFeetPosition.transform.position, leftHand.transform.position) < 0.65 ||
                    Vector3.Distance(playerFeetPosition.transform.position, rightHand.transform.position) < 0.65) 
                {
                    extraDistance = -1.1f;
                }
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
        // Convert minAngle and maxAngle to radians
        float minAngleRad = (minAngle * Mathf.Deg2Rad) * 0.5f;
        float maxAngleRad = (maxAngle * Mathf.Deg2Rad) * 0.5f;

        // Adjust playerAngle to be in the range [0, 2*pi)
        playerAngle = (playerAngle + 2 * Mathf.PI) % (2 * Mathf.PI);

        // Check if playerAngle is between minAngle and maxAngle
        if ((minAngleRad <= maxAngleRad && playerAngle >= minAngleRad && playerAngle <= maxAngleRad) ||
            (minAngleRad > maxAngleRad && (playerAngle >= minAngleRad || playerAngle <= maxAngleRad)))
        {
            result = true;
        }
        return result;
    }
}
