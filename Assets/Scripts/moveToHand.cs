using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToHand : MonoBehaviour
{
    private Transform handle;
    public bool onBack = true;
    public bool handReaching = false;
    private GameObject parent;
    private GameObject bag;
    private bool inHand = false;
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
        if (onBack && handReaching == false) 
        {
            handle.transform.position = transform.position + new Vector3(0, -0.01f, 0);
            bag.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
