using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public GameObject bag = null;
    private Transform offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (bag != null) 
        {
            transform.position = bag.transform.position + offset.position;
            transform.rotation = bag.transform.rotation * offset.rotation;
        }
    }
}
