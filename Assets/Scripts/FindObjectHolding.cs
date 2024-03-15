using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObjectHolding : MonoBehaviour
{
    public bool pickUp;
    public bool detach;
    public bool holding;

    public void hold() 
    {
        holding = true;
    }
    
}
