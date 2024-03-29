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
    }

    // Update is called once per frame
    void Update()
    {

        if (target != null && !BeingBlocked())
        {
            // Get the current position of the Player
            Vector3 playerPosition = target.transform.position;

            // Calculate the direction towards the Player
            Vector3 direction = playerPosition - transform.position;
            direction.y = 0f; // Ensure the enemy moves only along the X-Z plane

            // Normalize the direction vector to maintain constant speed
            direction.Normalize();

            // Calculate the desired position the enemy should move towards
            Vector3 targetPosition = transform.position + direction * moveSpeed * Time.deltaTime;

            // Smoothly move the enemy towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

            // Calculate the rotation needed to face the Player
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            //Correct rotation
            targetRotation *= Quaternion.Euler(0, 0, 0);

            // Apply the rotation to the enemy
            transform.rotation = targetRotation;

        }
    }

    private bool BeingBlocked()
    {
        bool result = false;
        RaycastHit[] hit;
        hit = rb.SweepTestAll(Vector3.forward, sweepSize, QueryTriggerInteraction.Ignore);
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
