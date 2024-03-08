using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public float initialSpawnRate = 2f; // Initial spawn rate (time between spawns)
    public float spawnRateDecrease = 0.1f; // Amount to decrease spawn rate over time
    public float minSpawnRate = 0.5f; // Minimum spawn rate
    private float currentSpawnRate; // Current spawn rate
    private float timeSinceLastSpawn; // Time elapsed since last spawn
    public bool canSpawn = true; // Flag to control spawning
    public static List<GameObject> enemyInstances = new List<GameObject>(); // List to store references to all enemy instances

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        timeSinceLastSpawn = 0f;
    }

    void Update()
    {
        // Check if spawning is allowed
        if (canSpawn)
        {
            // Update time since last spawn
            timeSinceLastSpawn += Time.deltaTime;

            // Check if it's time to spawn a new enemy
            if (timeSinceLastSpawn >= currentSpawnRate)
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0f; // Reset time since last spawn
                // Decrease spawn rate (with a minimum)
                currentSpawnRate = Mathf.Max(currentSpawnRate - spawnRateDecrease, minSpawnRate);
            }
        }
    }

    void SpawnEnemy()
    {
        // Instantiate a new enemy at a random position around the edges of the plane
        Vector3 spawnPosition = CalculateRandomSpawnPosition();
        Quaternion spawnRotation = Quaternion.identity; // No rotation for simplicity
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, spawnRotation);

        // Add the spawned enemy instance to the list
        enemyInstances.Add(enemyInstance);
    }

    Vector3 CalculateRandomSpawnPosition()
    {
        // Calculate a random position around the edges of the plane
        // You'll need to adjust this based on the size and shape of your stage

        int side = Random.Range(0, 4);
        float x = 0;
        float z = 0;
        switch (side)
        {
            case 0: //Top edge
                x = Random.Range(-19f, 19f);
                z = Random.Range(15.5f, 18.5f);
                break;
            case 1: //Left edge
                x = Random.Range(-15.5f, -18.5f);
                z = Random.Range(-19f, 19f);
                break;
            case 2: //Right edge
                x = Random.Range(15.5f, 18.5f);
                z = Random.Range(-19f, 19f);
                break;
            case 3: //Bottom edge
                x = Random.Range(-19f, 19f);
                z = Random.Range(-15.5f, -18.5f);
                break;
        }
        return new Vector3(x, 0f, z);
    }
}
