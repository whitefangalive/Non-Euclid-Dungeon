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
    public TMP_Text moneyText;
    public TMP_Text costText;
    private bool sold = false;
    private Collider collider;
    private GameObject item;
    private item itemScript;
    private Collider Collider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerData = player.GetComponent<PlayerData>();
        costText.GetComponent<TMP_Text>().text = ("$" + cost);
        moneyText = moneyText.GetComponent<TMP_Text>();
        collider = GetComponent<Collider>();
        itemScript = transform.parent.GetComponentInChildren<item>();
        item = itemScript.gameObject;
        Collider = item.GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = ("$" + money);
        money = playerData.money;
        if (money >= cost)
        {
            collider.enabled = false;
            
            Collider.enabled = true;
        }
        else if (!sold)
        {
            collider.enabled = true;
            itemScript.enabled = false;
            Collider.enabled = false;

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
