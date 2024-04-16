using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagScript : MonoBehaviour
{
    public HashSet<GameObject> inventory = new HashSet<GameObject>();
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
        foreach (GameObject thing in inventory) 
        {
            if (thing.GetComponent<item>().bag != gameObject) 
            {
                thing.GetComponent<item>().bag = gameObject;
            }
        }
    }
    private void OnTriggerStay(Collider other)
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
            inventory.Add(thing.transform.parent.gameObject);
        }

        GameObject.Find("ValueOfItemsText").GetComponent<TMP_Text>().text = "$" + getTotalValue();
    }
    public int getTotalValue()
    {
        int result = 0;
        foreach (GameObject thing in inventory) 
        { 
            result += thing.GetComponent<item>().value;
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
        if (thing.transform.parent.position != it.postionWanted) {
            Debug.Log("Pos is not right");
        }
        Debug.Log(it.offsetPos);

        Vector3 positionToGoTo = transform.position + it.offsetPos;
        if (it != null && thing.transform.parent.position == positionToGoTo) 
        {
            rb = thing.GetComponent<Rigidbody>();
            if (rb != null)
            {
                inventory.Remove(thing.transform.parent.gameObject);
                thing.transform.parent.gameObject.GetComponent<item>().bag = null;
            }
        }
        GameObject.Find("ValueOfItemsText").GetComponent<TMP_Text>().text = "$" + getTotalValue();
    }

}
