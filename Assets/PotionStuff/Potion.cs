using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Potion : MonoBehaviour
{
    public float maxIntensity = 0.6f;
    private VelocityCollide velocityCollide;
    public float explodeMagnitude = 5;
    public GameObject exploded;
    public GameObject explodedParticles;
    public int healthToAdd = 1;
    // Start is called before the first frame update
    void Start()
    {
        velocityCollide = GetComponent<VelocityCollide>();
    }

    public void explode() 
    {
        Destroy(gameObject);
        Instantiate(exploded, transform.position, transform.rotation);
        Instantiate(explodedParticles, transform.position, Quaternion.identity);
        GameObject.Find("Player").GetComponent<PlayerData>().health += healthToAdd;
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
