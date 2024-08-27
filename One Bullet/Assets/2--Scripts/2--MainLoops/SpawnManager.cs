using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject enemy;
    public float spawnRate;
    float timeBtwSpawn;
    // Start is called before the first frame update
    void Start()
    {
        timeBtwSpawn = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwSpawn <= 0)
        {
            Invoke("SelectSpawnPoint", 2f);
            timeBtwSpawn = spawnRate;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
    void SelectSpawnPoint()
    {        
        SpawnEnemies();
    }
    void SpawnEnemies()
    {
        Instantiate(enemy, spawnPoint[Random.Range(0, spawnPoint.Length)].position, enemy.transform.rotation);
    }
}
