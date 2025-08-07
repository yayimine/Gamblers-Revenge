using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // Import the UI namespace to work with UI elements
using UnityEngine.SceneManagement; // Import the SceneManagement namespace to load scenes

/// <summary>
/// Controls spawning of werewolf enemies. Spawn rate scales with the game's
/// current stage to ramp difficulty over time.
/// </summary>
public class SpawnManagerWerewolf : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public float spawnRate = 10f;
    public float spawnTimer = 10f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance == null) return;
        spawnRate = 10f / GameManager.instance.gameStage;

        if (spawnTimer > 0f)
        {
            spawnTimer -= Time.deltaTime; // Decrease the timer
        }
        else
        {
            SpawnEnemy(); // Call the method to spawn an enemy
            spawnTimer = spawnRate; // Reset the timer
        }
    }

    /// <summary>Spawn a werewolf in a random spot away from the player.</summary>
    void SpawnEnemy()
    {
        if (PlayerController.instance == null) return;
        // Generate a random position within the screen bounds
        Vector3 randomPos = new Vector3(Random.Range(-28f, 8f), Random.Range(-8f, 20f), 0f);
        // Instantiate the enemy at the random position
        float distanceToPlayer = (randomPos - PlayerController.instance.transform.position).magnitude;
        if (distanceToPlayer > 10f) // Ensure the enemy spawns at least 5 units away from the player
        {
            Instantiate(enemyPrefab, randomPos, enemyPrefab.transform.rotation);
        }
    }
}
