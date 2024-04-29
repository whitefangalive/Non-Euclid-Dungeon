using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent onDeath;
    private Rigidbody rb;
    private bool justPushedEntity = false;

    public float ThrowBackSpeed = 1;
    public float maxThrowBackSpeed = 1.5f;
    private float speed = 0.0f;
    private Animator animator;
    private Vector3 direction;
    
    private float timerNow;
    private float throwbackMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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

        if (justPushedEntity) 
        {
            if (Time.timeSinceLevelLoad - timerNow < 1)
            {
                speed = 1 - (Time.timeSinceLevelLoad - timerNow);
                speed = Mathf.Clamp(speed, -ThrowBackSpeed, ThrowBackSpeed) * transform.lossyScale.y;
                float massMultiplier = (rb.mass * 0.05f);
                throwbackMultiplier = Mathf.Clamp(throwbackMultiplier, -maxThrowBackSpeed * massMultiplier, maxThrowBackSpeed * massMultiplier);
                transform.position += speed * (throwbackMultiplier * -1) * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
                
            }
            else 
            {
                justPushedEntity = false;
            }
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

            timerNow = Time.timeSinceLevelLoad;
            justPushedEntity = true;
            direction = from.position - transform.position;
            direction.y = 0f;
            direction.Normalize();
            throwbackMultiplier = damage;


        }
    }

    private void Die() 
    {
        dieSound.Play();
        onDeath.Invoke();
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
