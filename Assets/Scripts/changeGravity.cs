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
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void GravityOff() 
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void GravityOn() 
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
    }
}
