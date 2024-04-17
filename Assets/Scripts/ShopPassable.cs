using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using TMPro;
using System.Data.SqlTypes;

public class ShopPassable : MonoBehaviour
{
    public int cost = 1;
    public int money;
    private GameObject player;
    private PlayerData playerData;
    private TMP_Text moneyText;
    private bool sold = false;
    private new Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerData = player.GetComponent<PlayerData>();
        GameObject.Find("Cost").GetComponent<TMP_Text>().text = ("$" + cost);
        moneyText = GameObject.Find("Money").GetComponent<TMP_Text>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = ("$" + money);
        money = playerData.money;
        if (money >= cost)
        {
            collider.enabled = false;
        }
        else if (!sold)
        {
            collider.enabled = true;
        }
    }

    public void GrabItem() 
    {
        if (!sold) 
        {
            playerData.money -= cost;
            sold = true;
        }
    }
}
