using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeGravity : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void GravityOff() 
    {
        rb.useGravity = false;
    }
    public void GravityOn() 
    {
        rb.useGravity = true;
    }
}
