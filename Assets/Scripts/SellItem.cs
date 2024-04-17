using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItem : MonoBehaviour
{

    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<PlayerData>();
    }
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody != null) 
        {
            rigidbody.velocity *= 0.5f;
            rigidbody.angularVelocity *= 0.5f;
            item it = other.transform.parent.GetComponent<item>();
            if (it != null) 
            {
                

                if (rigidbody.velocity.magnitude < 0.05) 
                {
                    sell(other.transform.parent.gameObject, it.value);
                }
            }
        }
    }

    private void sell(GameObject obj, int price) 
    {
        Destroy(obj);
        playerData.money += price;
    }

}
