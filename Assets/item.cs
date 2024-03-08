using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public GameObject bag = null;
    private Vector3 offsetPos;
    private Quaternion offsetRot;

    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {

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
                once = false;
            }

            transform.position = bag.transform.position + offsetPos;
            transform.rotation = bag.transform.rotation * offsetRot;
        }
    }
}
