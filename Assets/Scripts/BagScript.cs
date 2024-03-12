using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();
    private Rigidbody rb;
    private item it;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject thing in inventory) 
        {
            if (thing.transform.parent == null)
            {
                if (thing.GetComponent<item>().bag != gameObject) 
                {
                    thing.GetComponent<item>().bag = gameObject;
                }
            }
            else 
            {
                if (thing.GetComponent<item>().bag != null) 
                {
                    thing.GetComponent<item>().bag = null;
                }
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject thing = other.transform.gameObject;
        rb = thing.GetComponent<Rigidbody>();
        it = thing.transform.parent.gameObject.GetComponent<item>();
        if (it != null && rb != null)
        {
            inventory.Add(thing.transform.parent.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject thing = other.transform.gameObject;
        rb = thing.GetComponent<Rigidbody>();
        it = thing.transform.parent.gameObject.GetComponent<item>();
        if (it != null && rb != null)
        {
            inventory.Remove(thing.transform.parent.gameObject);
        }
    }
}
