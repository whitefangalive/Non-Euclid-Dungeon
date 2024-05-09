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
    private Collider itemCollider;
    public GameObject mirrorParticles;
    public AudioSource mirrorSound;
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
        itemCollider = item.GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = ("$" + money);
        money = playerData.money;
        if (money >= cost)
        {
            collider.isTrigger = true;
            
            
            itemCollider.enabled = true;
        }
        else if (!sold)
        {
            collider.isTrigger = false;
            itemScript.enabled = false;
            itemCollider.enabled = false;

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
    private void OnTriggerStay(Collider other)
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 25) 
        {
            Instantiate(mirrorParticles, other.transform.position, other.transform.rotation);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        mirrorSound.gameObject.transform.position = other.transform.position;
        mirrorSound.Play();
    }
    private void OnTriggerExit(Collider other)
    {
        mirrorSound.Stop();
    }
}
