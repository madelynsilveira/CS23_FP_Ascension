using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float minInterval = 5f;
    public float maxInterval = 10f;

    float timeUntilSpawn = 10f;
    
    // Update is called once per frame
    void Update()
    {
        //Spawns the given gameObject in a random range, with a random interval
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8, 8), Random.Range(-3, 3), 0);
            GameObject spawnedObject = Instantiate(spawnObject, spawnPos, Quaternion.identity);
            timeUntilSpawn = Random.Range(minInterval, maxInterval);
        }
        
    }
}
