using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public GameObject bag = null;
    private Vector3 offsetPos;
    private Quaternion offsetRot;
    private Rigidbody rb;
    private bool once = true;
    private bool originalUseGrav;
    private new Collider collider;
    public bool handAttached;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        collider = gameObject.transform.GetChild(0).GetComponent<Collider>();
        originalUseGrav = rb.useGravity;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (bag != null)
        {
            if (once)
            {
                offsetPos = transform.position - bag.transform.position;
                offsetRot = transform.rotation * Quaternion.Inverse(bag.transform.rotation);
                
                collider.excludeLayers = 1;
                once = false;
            }

            transform.position = bag.transform.position + offsetPos;
            transform.rotation = bag.transform.rotation * offsetRot;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
        }
        else 
        {
            once = true;
            rb.useGravity = originalUseGrav;
            collider.excludeLayers = 0;
        }
    }

    public void attached()
    {
        handAttached = true;
    }
    public void unattached()
    {
        handAttached = false;
    }
}
