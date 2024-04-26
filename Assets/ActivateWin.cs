using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWin : MonoBehaviour
{
    private WinGame winScript;
    // Start is called before the first frame update
    void Start()
    {
        winScript = GameObject.Find("WinManager").GetComponent<WinGame>();
    }

    public void activateWin() 
    {
        winScript.winRightNow = true;
    }

}
