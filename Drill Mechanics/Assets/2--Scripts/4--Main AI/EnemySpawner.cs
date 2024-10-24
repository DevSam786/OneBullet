using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] enemies;
    [SerializeField] float startTimeBtwSpawn;
    [SerializeField] float timeBtwBurst = 1.5f; // Time between burst spawns
    [SerializeField] int burstSize = 3; // Number of enemies in a burst
    private float timeBtwSpawn;
    private int spawnPattern; // 0 = Cluster, 1 = Sequential, 2 = Random Burst

    void Start()
    {
        timeBtwSpawn = 0;
        ChooseSpawnPattern();
    }

    void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            // Call the selected spawn pattern function
            switch (spawnPattern)
            {
                case 0:
                    ClusterSpawn();
                    break;
                case 1:
                    SequentialSpawn();
                    break;
                case 2:
                    StartCoroutine(RandomBurstSpawn());
                    break;
            }

            // Reset the spawn timer for the next set of spawns
            timeBtwSpawn = startTimeBtwSpawn;
            ChooseSpawnPattern(); // Pick a new pattern for the next wave
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

    // Function to randomly choose the spawn pattern
    void ChooseSpawnPattern()
    {
        spawnPattern = Random.Range(0, 3); // Choose a random pattern between 0, 1, and 2
    }

    // Cluster Spawning: Spawn multiple enemies at one point
    void ClusterSpawn()
    {
        int randSpawn = Random.Range(0, spawnPoints.Length);
        int randEnemy = Random.Range(0, enemies.Length);

        // Spawn 3 enemies at the same spawn point
        for (int i = 0; i < 3; i++)
        {
            Instantiate(enemies[randEnemy], spawnPoints[randSpawn].position, spawnPoints[randSpawn].rotation);
        }
    }

    // Sequential Spawning: Spawn enemies at different points in sequence
    void SequentialSpawn()
    {
        int randEnemy = Random.Range(0, enemies.Length);

        // Spawn an enemy at each spawn point in sequence
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(enemies[randEnemy], spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }

    // Random Burst Spawning: Spawn multiple enemies at random points quickly
    IEnumerator RandomBurstSpawn()
    {
        int randEnemy = Random.Range(0, enemies.Length);

        for (int i = 0; i < burstSize; i++)
        {
            int randSpawn = Random.Range(0, spawnPoints.Length);
            Instantiate(enemies[randEnemy], spawnPoints[randSpawn].position, spawnPoints[randSpawn].rotation);
            yield return new WaitForSeconds(timeBtwBurst); // Delay between each enemy spawn
        }
    }
}
