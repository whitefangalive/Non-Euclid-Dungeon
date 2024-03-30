using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public GameObject target;

    private Rigidbody rb;
    public float sweepSize = 0.5f;
    public LayerMask ignoreLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("HeadCollider");
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

        if (target != null && !BeingBlocked(direction) && horizontalDistance > target.transform.localScale.y)
        {
            // Calculate the desired position the enemy should move towards
            Vector3 targetPosition = transform.position + direction * moveSpeed * Time.deltaTime;

            // Smoothly move the enemy towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        }
        //Renderer renderer = GetComponent<Renderer>();
        //bool grounded = Physics.Raycast(transform.position, Vector3.down, sweepSize, ~ignoreLayer);
        //if (grounded) { rb.useGravity = false; } else { rb.useGravity = true; }

    }

    private bool BeingBlocked(Vector3 Direction)
    {
        bool result = false;
        RaycastHit[] hit;
        hit = rb.SweepTestAll(Direction, sweepSize, QueryTriggerInteraction.Ignore);
        foreach (RaycastHit thing in hit)
        {
            if ((ignoreLayer & (1 << thing.transform.gameObject.layer)) == 0)
            {
                result = true;
            }
        }
        return result;
    }
}
