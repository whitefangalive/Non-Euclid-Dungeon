using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using TMPro;
using System.Data.SqlTypes;
using Valve.VR.InteractionSystem;

public class ShopPassable : MonoBehaviour
{
    public int cost = 1;
    public int money;
    private GameObject player;
    private PlayerData playerData;
    private TMP_Text moneyText;
    private bool sold = false;
    private new Collider collider;
    private GameObject item;
    private item itemScript;
    private Throwable throwable;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerData = player.GetComponent<PlayerData>();
        GameObject.Find("Cost").GetComponent<TMP_Text>().text = ("$" + cost);
        moneyText = GameObject.Find("Money").GetComponent<TMP_Text>();
        collider = GetComponent<Collider>();
        itemScript = transform.parent.GetComponentInChildren<item>();
        item = itemScript.gameObject;
        throwable = item.GetComponentInChildren<Throwable>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = ("$" + money);
        money = playerData.money;
        if (money >= cost)
        {
            collider.enabled = false;
            
            throwable.enabled = true;
        }
        else if (!sold)
        {
            collider.enabled = true;
            itemScript.enabled = false;
            throwable.enabled = false;

        }
    }

    public void GrabItem() 
    {
        if (!sold) 
        {
            playerData.money -= cost;
            sold = true;
            itemScript.enabled = true;
        }
    }
}
