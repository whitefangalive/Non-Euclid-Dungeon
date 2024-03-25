using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class VelocityCollide : MonoBehaviour
{
    private Rigidbody rb;
    public float audioVelocity = 4.0f;

    private AudioSource collisionSound;
    private Vector3 previousVelocity;

    public float DamageMutliplier;
    public float currentDamage;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collisionSound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Time.frameCount % 5 == 0)
        {
            previousVelocity = rb.velocity;
        }
    }
    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Damage" + previousVelocity.magnitude.ToString());
        currentDamage = previousVelocity.magnitude * DamageMutliplier;
        if (previousVelocity.magnitude > audioVelocity) 
        { 
            collisionSound.Play(0);
        }
        if (collision.rigidbody != null)
        {
            if (collision.rigidbody.velocity.magnitude > audioVelocity)
            {
                collisionSound.Play(0);
            }
        }
        
    }
}
