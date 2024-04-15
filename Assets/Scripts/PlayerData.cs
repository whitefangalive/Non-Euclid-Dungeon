using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int maxHealth = 2;
    public int health = 0;

    public GameObject damageParticles;
    public GameObject deathParticles;

    public float MaxInvernabilityFrames = 10;
    public float InvernabilityFrames = 0;
    public AudioSource damageTakeSound;
    public int money = 0;


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

    private void FixedUpdate()
    {
        if (InvernabilityFrames > 0)
        {
            InvernabilityFrames -= Time.deltaTime;
        }
    }

    public void takeDamage(int damage, Transform from)
    {
        if (damage > 0 && InvernabilityFrames <= 0)
        {
            //Instantiate(damageParticles, from.position, Quaternion.Inverse(from.localRotation));
            damageTakeSound.Play(0);
            health -= damage;
            InvernabilityFrames = MaxInvernabilityFrames;
        }
    }

    private void Die()
    {
        health = maxHealth;
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        GameObject.Find("WinManager").GetComponent<WinGame>().SendToScene(1);
    }
}
