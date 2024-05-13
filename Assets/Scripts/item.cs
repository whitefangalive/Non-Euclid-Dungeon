using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public GameObject bag = null;
    public Vector3 offsetPos;
    private Vector3 offsetPosRigid;
    private Quaternion offsetRot;
    private Quaternion offsetRotRigid;
    private Rigidbody rb;
    private bool once = true;
    private bool once2 = true;
    private bool originalUseGrav;
    private Collider collider;
    public bool handAttached;
    public int value = 50;
    private Vector3 postionWanted;
    private Quaternion rotationWanted;
    private GameObject rigidObject;

    public bool inbag = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        collider = gameObject.transform.GetChild(0).GetComponent<Collider>();
        originalUseGrav = rb.useGravity;
        rigidObject = rb.gameObject;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (bag != null)
        {
            once2 = true;
            if (once)
            {
                offsetPos = transform.position - bag.transform.position;
                offsetPosRigid = rigidObject.transform.position - bag.transform.position;
                offsetRot = transform.rotation * Quaternion.Inverse(bag.transform.rotation);
                offsetRotRigid = rigidObject.transform.rotation * Quaternion.Inverse(bag.transform.rotation);

                collider.excludeLayers = 1;
                once = false;
            }

            if (bag.GetComponent<BagScript>().locked)
            {
                if (!handAttached)
                {
                    postionWanted = bag.transform.position + offsetPosRigid;
                    rotationWanted = bag.transform.rotation * offsetRotRigid;
                    rigidObject.transform.position = postionWanted;
                    rigidObject.transform.rotation = rotationWanted;

                    offsetPos = transform.position - bag.transform.position;
                    offsetRot = transform.rotation * Quaternion.Inverse(bag.transform.rotation);
                }
                else
                {
                    offsetPosRigid = rigidObject.transform.position - bag.transform.position;
                    offsetRotRigid = rigidObject.transform.rotation * Quaternion.Inverse(bag.transform.rotation);


                    rotationWanted = bag.transform.rotation * offsetRot;
                    postionWanted = bag.transform.position + offsetPos;
                    transform.position = postionWanted;
                    transform.rotation = rotationWanted;
                }
            }
            else 
            {
                offsetPosRigid = rigidObject.transform.position - bag.transform.position;
                offsetRotRigid = rigidObject.transform.rotation * Quaternion.Inverse(bag.transform.rotation);

                rotationWanted = bag.transform.rotation * offsetRot;
                postionWanted = bag.transform.position + offsetPos;
                transform.position = postionWanted;
                transform.rotation = rotationWanted;
            }
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
        }
        else 
        {
            once = true;
            if (once2)
            {
                once2 = false;
                
                rb.useGravity = originalUseGrav;
                collider.excludeLayers = 0;
            }

        }

    }

    public void attached()
    {
        handAttached = true;
        gameObject.transform.parent = null;

        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    public void unattached()
    {
        handAttached = false;
    }
}
