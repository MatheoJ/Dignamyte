using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaningSystem : MonoBehaviour
{
    
    [SerializeField]
    private GameObject enemyPrefab;
    
    // List of transform objects that represent the spawn points
    public List<Transform> spawnPoints;

    public bool spawnActivated { get; set; } = false;

    
    private float lastSpawnTime = 0.0f;
    
    [SerializeField]
    private float spawnInterval = 1.0f;
    
    void Start()
    {
        InitializeSpawnPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldSpawnEnemy())
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }
    }
    
    bool ShouldSpawnEnemy()
    {
        if(Time.time - lastSpawnTime > spawnInterval && spawnActivated)
        {
            lastSpawnTime = Time.time;
            return true;
        }
        return false;
    }
    
    void SpawnEnemy()
    {
        // Randomly select a spawn point
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        
        // Get the position of the spawn point
        Vector3 spawnPosition = spawnPoints[spawnPointIndex].position;
        
        // Instantiate the enemy prefab at the spawn position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    
    void InitializeSpawnPoints()
    {
        // Find all the spawn points in the scene
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("EnemySpawningPoint");
        
        // Add the transform of each spawn point to the list
        foreach(GameObject spawnPointObject in spawnPointObjects)
        {
            spawnPoints.Add(spawnPointObject.transform);
        }
    }
}
