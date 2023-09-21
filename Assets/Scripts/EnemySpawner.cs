using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    private float spawnNext = 0;
    public float xLimit;
    public float maxLifeTime = 6f;

    // Update is called once per frame
    void Update()
    {
        Camera camara = Camera.main;
        if(camara != null){
            xLimit = camara.orthographicSize * camara.aspect;
        }
        if(Time.time > spawnNext){
            spawnNext = Time.time + 60/spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;
            float rand = Random.Range(-xLimit,xLimit);
            Vector2 spawnPosition = new Vector2(rand, 12f);
            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            Destroy(meteor, maxLifeTime);
        }
    }
}