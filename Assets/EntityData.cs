using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : MonoBehaviour
{
    public int maxHealth = 2;
    public int health = 0;

    public float speed = 1;
    public float attackDamage = 1;

    public GameObject deathParticles;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) 
        {
            Die();
        }
    }

    public void takeDamage(int damage, Transform from)
    {
        if (damage > 0)
        {
            Instantiate(deathParticles, from.position, from.localRotation);
            health -= damage;
        }
        
        
    }

    private void Die() 
    {
        Instantiate(deathParticles, transform.position, transform.localRotation);
        Destroy(gameObject);
    }
}
