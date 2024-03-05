using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the enemy moves towards the player
    private Transform playerTransform; // Reference to the player's transform

    void Start()
    {
        // Find the player GameObject by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            // Get the player's transform component
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found!");
        }
    }

    void Update()
    {
        // Check if the player reference is not null
        if (playerTransform != null)
        {
            // Calculate the direction towards the player
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            direction.y = 0f; // Ensure the enemy moves only along the X-Z plane

            // Move the enemy towards the player
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Calculate the rotation needed to face the player
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Apply the rotation to the enemy smoothly
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
        }
    }
}
