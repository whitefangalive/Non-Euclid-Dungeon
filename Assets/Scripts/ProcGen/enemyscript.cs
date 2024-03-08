using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyScript : MonoBehaviour
{
    public GameObject Player; // Reference to the Player's transform
    public float moveSpeed = 50f; // Speed at which the enemy moves towards the Player
    public TextMeshProUGUI scoreText;
    public EnemySpawner enemySpawner; // Reference to the EnemySpawner component
    public GameObject playAgain;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");

        // Check if Player GameObject was found
        if (Player == null)
        {
            Debug.LogError("Player GameObject not found!");
        }

        playAgain = GameObject.FindWithTag("PlayAgain");

        if(playAgain == null)
        {
            Debug.LogError("PlayAgain GameObject not found");
        }

        // Find the TextMeshProUGUI score text dynamically
        scoreText = FindObjectOfType<TextMeshProUGUI>();

        // Check if the TextMeshProUGUI object was found
        if (scoreText == null)
        {
            Debug.LogError("scoreText object not found in the scene!");
        }

        // Find and get the EnemySpawner component in the scene
        enemySpawner = FindObjectOfType<EnemySpawner>();

    }


    void Update()
    {
        if (Player != null) // Check if the Player reference is not null
        {
            // Get the current position of the Player
            Vector3 playerPosition = Player.transform.position;

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

    void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player")) // Check if the collider belongs to the Player
        {
            
            scoreText.text = "You Died";

            

            enemySpawner.canSpawn = false;
            // Access the enemyInstances list from EnemySpawner
            List<GameObject> enemyInstances = EnemySpawner.enemyInstances;

            // Loop through all enemy instances
            foreach (GameObject enemyInstance in enemyInstances)
            {
                // Destroy the enemy instance
                Destroy(enemyInstance);
            }

            // Clear the enemyInstances list
            enemyInstances.Clear();

            Vector3 pos = new Vector3(0f, 1.321f, 2.711f);

            playAgain.transform.position = pos;

        }
    }
}

