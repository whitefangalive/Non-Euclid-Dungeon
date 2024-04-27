using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossMovement : MonoBehaviour
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
    public AudioSource Attack1Sound;
    public AudioSource AttackLongSound;

    public bool isAttacking = false;

    private float timerNow;
    public float attackInterval = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("HeadCollider");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("VomitAttack")) 
        {
            transform.rotation = targetRotation;
        }
        // Apply the rotation to the enemy
        

        float horizontalDistance = Mathf.Sqrt(Mathf.Pow(transform.position.x - playerPosition.x, 2) + (Mathf.Pow(transform.position.z - playerPosition.z, 2)));

        if (target != null && !BeingBlocked(direction) && horizontalDistance > (target.transform.localScale.y * playerScaleMultiplier))
        {
            if (horizontalDistance < entityFollowRange) 
            {
                // Calculate the desired position the enemy should move towards
                Vector3 targetPosition = transform.position + direction * moveSpeed * Time.deltaTime;

                // Smoothly move the enemy towards the target position
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("VomitAttack"))
                {
                    speed = 1.0f;
                    speed = Mathf.Clamp(speed, -moveSpeed, moveSpeed) * transform.lossyScale.y;
                    transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
                }
                    animator.SetBool("IsMoving", true);
            }
        }
        else 
        {
            animator.SetBool("IsMoving", false);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterIdle") && isAttacking)
            {
                timerNow = Time.timeSinceLevelLoad;
                isAttacking = false;
                Debug.Log("Boss radom attack chosen ");
            }

            if (!isAttacking && (Time.timeSinceLevelLoad - timerNow >= attackInterval))
            {
                int rand = Random.Range(0, 3);
                
                isAttacking = true;
                switch (rand)
                {
                    case 0:
                        AttackLong();
                        break;
                    case 1:
                        Attack1();
                        break;
                    case 2:
                        AttackVomit();
                        break;
                    default:

                        break;
                }
            }
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

    private void Attack1()
    {

        Attack1Sound.Play();
        animator.SetTrigger("Attack1");
    }

    private void AttackLong()
    {

        AttackLongSound.Play();
        animator.SetTrigger("AttackLong");

    }
    private void AttackVomit()
    {

        // sound is hanndled during animation
        animator.SetTrigger("AttackVomit");

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "BodyCollider" || collision.transform.name == "HeadCollider")
        {
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterAttack1") || this.animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterLongAttack") || this.animator.GetCurrentAnimatorStateInfo(0).IsName("VomitAttack"))
            {
                collision.transform.root.GetComponent<PlayerData>().takeDamage(attackDamage, transform);
            }
        }
    }
}
