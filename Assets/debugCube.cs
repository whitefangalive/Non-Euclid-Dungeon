using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class debugCube : MonoBehaviour
{
    private TMP_Text progressBar;
    private GameObject compass;
    private ProgressionScript progression;
    // Start is called before the first frame update
    void Start()
    {
        progressBar = GameObject.Find("ProgressBar").GetComponent<TMP_Text>();
        compass = GameObject.Find("Compass");
        progression = GameObject.Find("ProgressionManager").GetComponent<ProgressionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        progressBar.text = progression.roomsExplored.ToString();
        GameObject stairs = GameObject.Find("LocationOfStairs");
        if (stairs != null) 
        {
            compass.transform.LookAt(stairs.transform.position);
        }
    }
}
