using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float minInterval = 2f;
    public float maxInterval = 7f;

    float timeUntilSpawn = 3f;
    
    // Update is called once per frame
    void Update()
    {
        //Spawns the given gameObject in a random range, with a random interval
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-20, 20), Random.Range(0, 30), 0);
            GameObject spawnedObject = Instantiate(spawnObject, spawnPos, Quaternion.identity);
            timeUntilSpawn = Random.Range(minInterval, maxInterval);
        }
        
    }
}
