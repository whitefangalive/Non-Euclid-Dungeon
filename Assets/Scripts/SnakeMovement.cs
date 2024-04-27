using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SnakeMovement : MonoBehaviour
{
    public float moveSpeed;
    private float speed = 0.0f;
    public GameObject target;

    private Rigidbody rb;
    public float sweepSize = 0.5f;
    public LayerMask ignoreLayer;
    public int attackDamage = 1;

    public float entityFollowRange = 50.0f;

    public float playerScaleMultiplier = 1.5f;

    public Animator animator;
    public AudioSource SnakeHiss;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("HeadCollider");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current position of the Player
        Vector3 playerPosition = target.transform.position;

        // Calculate the direction towards the Player
        Vector3 direction = playerPosition - transform.position;
        direction.y = 0f; // Ensure the enemy moves only along the X-Z plane

        // Normalize the direction vector to maintain constant speed
        direction.Normalize();

        // Calculate the rotation needed to face the Player
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        //Correct rotation
        targetRotation *= Quaternion.Euler(0, 0, 0);

        // Apply the rotation to the enemy
        transform.rotation = targetRotation;

        float horizontalDistance = Mathf.Sqrt(Mathf.Pow(transform.position.x - playerPosition.x, 2) + (Mathf.Pow(transform.position.z - playerPosition.z, 2)));

        if (target != null && !BeingBlocked(direction) && horizontalDistance > (target.transform.localScale.y * playerScaleMultiplier))
        {
            if (horizontalDistance < entityFollowRange) 
            {
                // Calculate the desired position the enemy should move towards
                Vector3 targetPosition = transform.position + direction * moveSpeed * Time.deltaTime;

                // Smoothly move the enemy towards the target position
                speed = 1.0f;
                speed = Mathf.Clamp(speed, -moveSpeed, moveSpeed) * transform.lossyScale.y;
                transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
                animator.SetBool("IsMoving", true);
            }
        }
        else 
        {
            animator.SetBool("IsMoving", false);
            Attack();
        }

    }

    private bool BeingBlocked(Vector3 Direction)
    {
        bool result = false;
        RaycastHit[] hit;
        hit = rb.SweepTestAll(Direction, sweepSize, QueryTriggerInteraction.Ignore);
        foreach (RaycastHit thing in hit)
        {
            if ((ignoreLayer & (1 << thing.transform.gameObject.layer)) == 0 && (thing.transform.name == "BodyCollider" || thing.transform.name == "HeadCollider"))
            {
                result = true;
            }
        }
        return result;
    }

    private void Attack()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("SnakeIdle")) 
        {
            SnakeHiss.Play();
            animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "BodyCollider" || other.transform.name == "HeadCollider") 
        {
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("SnakeAttack")) 
            {
                other.transform.root.GetComponent<PlayerData>().takeDamage(attackDamage, transform);
            }
        }
    }
}
