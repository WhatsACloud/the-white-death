using UnityEngine;
using System.Collections.Generic;

public class SpawningManager : MonoBehaviour
{ // should only spawn on top.
    public GameObject enemyPrefab;   // Assign the projectile prefab in the Inspector
    public int enoughEnemies;   // Number of enemies such that if there are less than that number you start spawning.
    public int clusterMin; // Minimum enemies per cluster
    public int clusterVariance; // clusterMin + rand[0,clusterVariance] is the cluster spawned.
    public float maxDistance;
    public float spawnDeltaY; // spawning y difference from player so that it spawns off screen
    public float spawnSpreadX;
    public float clusterSpread;

    private GameObject player;            // Reference to player (object)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    GameObject Spawn(Vector2 position){
       GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
       return enemy;
    }

    void SpawnCluster(int size, Vector2 position, float spread){
        for (int i=0;i<size;i++){
            Spawn(position + (Random.Range(-spread,+spread) * Vector2.left)
                 + (Random.Range(-spread,+spread) * Vector2.up));
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {   
            GameObject enemy = enemies[i];
            if (Vector2.Distance(player.transform.position, enemy.transform.position) > maxDistance)
            {
                Destroy(enemy); 
            }
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length<enoughEnemies){
            SpawnCluster(
                clusterMin + Random.Range(0,clusterVariance+1), 
                player.transform.position +
                    (Vector3.up * spawnDeltaY) + (Vector3.left * Random.Range(-spawnSpreadX,spawnSpreadX)),
                clusterSpread
            );
        }
    }
}
