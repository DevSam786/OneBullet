using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public EnemyBase[] enemy;
        public int count;
        public float timeBtwSpawns;
    }

    public Wave[] wave;
    public Transform[] spawnPoint;
    
    public float timeBtwWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;    
   
    bool finishedSpawning;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }
    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBtwWaves);
        StartCoroutine(SpawnWave(index));
    }
    IEnumerator SpawnWave(int index)
    {
        currentWave = wave[index];
        for (int i = 0;i < currentWave.count; i++)
        {
            if (player == null)
            {
                yield break;
            }
            else
            {
                int randSpawnPoint = Random.Range(0,spawnPoint.Length);
                EnemyBase enemyMain = currentWave.enemy[Random.Range(0, currentWave.enemy.Length)]; 
                Instantiate(enemyMain, spawnPoint[randSpawnPoint].position, spawnPoint[randSpawnPoint].rotation);

                if(i == currentWave.count - 1)
                {
                    finishedSpawning = true;
                }
                else
                {
                    finishedSpawning = false;
                }

                yield return new WaitForSeconds(currentWave.timeBtwSpawns);
            }
        }
    }
    private void Update()
    {
        if(finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;
            if(currentWaveIndex + 1 < wave.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                Debug.Log("Game Finished;");
            }
        }
    }
}
