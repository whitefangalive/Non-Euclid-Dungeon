using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class debugCube : MonoBehaviour
{
    public TMP_Text progressBar;
    public GameObject compass;
    private ProgressionScript progression;
    private int previousExplored = 0;
    public AudioSource levelIncreased;
    public AudioSource compassReady;

    private bool compassReadyOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        progression = GameObject.Find("ProgressionManager").GetComponent<ProgressionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //check for change then update;
        int explored = progression.roomsExplored;
        if (explored != previousExplored)
        {
            levelIncreased.Play();
            previousExplored = explored;
            string black = "<color=#000000>";
            string green = "<color=#29ff62>";
            string gold = "<color=#f5d442>";
            StringBuilder stringBuilder = new StringBuilder();

            if (progression.DisableDegeneration == true)
            {
                for (int i = 0; i < (progression.AmountOfRoomsLevelOne); ++i)
                {
                    stringBuilder.Append(gold + "| ");
                }
            }
            else 
            {
                for (int i = 0; i < explored; ++i)
                {
                    stringBuilder.Append(green + "| ");
                }
                for (int i = 0; i < (progression.AmountOfRoomsLevelOne - explored); ++i)
                {
                    stringBuilder.Append(black + "| ");
                }
            }
            progressBar.text = stringBuilder.ToString();
        }
        
        GameObject stairs = GameObject.Find("LocationOfStairs");
        if (stairs != null && progression.DisableDegeneration == true)
        {
            compass.transform.LookAt(stairs.transform.position);
            if (compassReadyOnce)
            {
                compassReady.Play();
                compassReadyOnce = false;
            }
        }
        else 
        {
            compassReadyOnce = true;
        }
    }
}
