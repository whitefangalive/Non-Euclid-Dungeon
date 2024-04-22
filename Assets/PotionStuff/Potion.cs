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
    public float range = 3.0f;
    private AudioSource healSound;
    // Start is called before the first frame update
    void Start()
    {
        velocityCollide = GetComponent<VelocityCollide>();
        healSound = GameObject.Find("HealSoundObject").GetComponent<AudioSource>();
    }

    public void explode() 
    {
        Destroy(gameObject);
        Instantiate(exploded, transform.position, transform.rotation);
        Instantiate(explodedParticles, transform.position, Quaternion.identity);
        GameObject player = GameObject.Find("Player");
        if (Vector3.Distance(transform.position, player.transform.position) <= range) {

            player.GetComponent<PlayerData>().health = Mathf.Clamp(player.GetComponent<PlayerData>().health + healthToAdd, 0, player.GetComponent<PlayerData>().maxHealth);
            //Magic spell heal bright bell brid reverb.wav by ryusa -- https://freesound.org/s/531082/ -- License: Attribution 4.0
            healSound.Play();
        }
        
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
