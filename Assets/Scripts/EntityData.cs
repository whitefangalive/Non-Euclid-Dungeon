using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : MonoBehaviour
{
    public int maxHealth = 2;
    public int health = 0;

    public GameObject damageParticles;
    public GameObject deathParticles;

    public float MaxInvernabilityFrames = 10;
    public float InvernabilityFrames = 0;

    public AudioSource dieSound;
    public AudioSource hurtSound;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }


    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die();
        }
        if (InvernabilityFrames > 0)
        {
            InvernabilityFrames -= Time.deltaTime;
        }
    }

    public void takeDamage(int damage, Transform from)
    {
        if (damage > 0 && InvernabilityFrames <= 0)
        {
            hurtSound.Play();
            Instantiate(damageParticles, from.position, Quaternion.Inverse(from.localRotation));
            health -= damage;
            InvernabilityFrames = MaxInvernabilityFrames;
        }
    }

    private void Die() 
    {
        dieSound.Play();
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
