using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private VelocityCollide velocityCollide;
    public float explodeMagnitude = 5;
    public GameObject exploded;
    // Start is called before the first frame update
    void Start()
    {
        velocityCollide = GetComponent<VelocityCollide>();
    }

    public void explode() 
    {
        
        Destroy(gameObject);
        Instantiate(exploded, transform.position, transform.rotation);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (velocityCollide.previousVelocity.magnitude > explodeMagnitude)
        {
            explode();
        }
        VelocityCollide otherColl = collision.transform.gameObject.GetComponent<VelocityCollide>();
        if (collision.rigidbody != null && otherColl != null)
        {

            if (otherColl.previousVelocity.magnitude - velocityCollide.previousVelocity.magnitude > explodeMagnitude)
            {
                explode();
            }
        }

    }
}
