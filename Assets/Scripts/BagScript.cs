using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagScript : MonoBehaviour
{
    public HashSet<item> inventory = new HashSet<item>();
    private Rigidbody rb;
    private item it;
    public LayerMask ExludeLayers;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (item thing in inventory) 
        {
            if (thing.bag != gameObject) 
            {
                thing.bag = gameObject;
            }
            if (Vector3.Distance(thing.transform.position, transform.position) >= 1.10) 
            {
                inventory.Remove(thing.transform.parent.gameObject.GetComponent<item>());
                thing.transform.parent.gameObject.GetComponent<item>().bag = null;
                GameObject.Find("ValueOfItemsText").GetComponent<TMP_Text>().text = "$" + getTotalValue();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject thing = other.transform.gameObject;
        rb = thing.GetComponent<Rigidbody>();
        it = null;
        if (thing.transform.parent != null) 
        {
            it = thing.transform.parent.gameObject.GetComponent<item>();
        }
        
        if (it != null && rb != null)
        {
            inventory.Add(thing.transform.parent.gameObject.GetComponent<item>());
        }

        GameObject.Find("ValueOfItemsText").GetComponent<TMP_Text>().text = "<size=60%>in Bag \r\n<size=100%>$" + getTotalValue();
    }
    public int getTotalValue()
    {
        int result = 0;
        foreach (item thing in inventory) 
        { 
            result += thing.value;
        }
        return result;
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject thing = other.transform.gameObject;
        it = null;
        if (thing.transform.parent != null)
        {
            it = thing.transform.parent.gameObject.GetComponent<item>();
        }
        if (it != null)
        {
            Vector3 positionToGoTo = transform.position + it.offsetPos;
            if (it != null && (thing.transform.parent.position == positionToGoTo || it.handAttached == true))
            {
                rb = thing.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    inventory.Remove(thing.transform.parent.gameObject.GetComponent<item>());
                    thing.transform.parent.gameObject.GetComponent<item>().bag = null;
                }
            }
            GameObject.Find("ValueOfItemsText").GetComponent<TMP_Text>().text = "$" + getTotalValue();
        }
    }

}
