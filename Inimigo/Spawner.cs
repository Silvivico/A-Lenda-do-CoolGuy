using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    public GameObject enemyPrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 2f; 
    public int maxEnemies = 10; 

    private int currentEnemyCount = 0; 
    private float nextSpawnTime = 0f;

    void Update()
    {
        if (!IsServer) { return; }
        
        if (Time.time >= nextSpawnTime && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval; 
        }
    }

    void SpawnEnemy()
    {

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);


        currentEnemyCount++;


        enemy.GetComponent<Inimigo>().OnEnemyDestroyed += HandleEnemyDestroyed;

        if (!enemy.GetComponent<NetworkObject>().IsSpawned) { enemy.GetComponent<NetworkObject>().Spawn(true); }

    }

    [ServerRpc]
    public void SpawnZombieServerRPC()
    {

    }

    void HandleEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
