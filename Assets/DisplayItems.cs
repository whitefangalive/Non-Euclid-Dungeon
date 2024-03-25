using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayItems : MonoBehaviour
{
    public HashSet<GameObject> inventory = new HashSet<GameObject>();
    public BagScript bagScript;
    public TMP_Text moneyTextMesh;
    // Start is called before the first frame update
    void Start()
    {
        bagScript = GameObject.Find("bag").GetComponent<BagScript>();
        inventory = bagScript.inventory;
        moneyTextMesh = GameObject.Find("moneyMesh").GetComponent<TMP_Text>();

        moneyTextMesh.text = "$" + bagScript.getTotalValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
