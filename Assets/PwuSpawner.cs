using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PwuSpawner : MonoBehaviour
{
    
    [SerializeField]
    private List<Transform> spawnPoints;
    
    [SerializeField]
    private List<GameObject> powerUps;
    
    [SerializeField]
    private float timeBetweenSpawns = 15.0f;
    
    [SerializeField]
    private  float timeBeforeFirstSpawn = 5.0f;
    
    
    private float timeSinceLastSpawn = 30.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timeSinceLastSpawn > timeBetweenSpawns)
        {
            timeSinceLastSpawn = Time.time;
            SpawnPowerUp();
        }
    }
    
    private void SpawnPowerUp()
    {
        int randomPowerUpIndex = Random.Range(0, powerUps.Count);
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
        
        Instantiate(powerUps[randomPowerUpIndex], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
    }
}
