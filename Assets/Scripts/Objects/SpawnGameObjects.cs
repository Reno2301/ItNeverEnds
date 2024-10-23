using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObjects : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject meteor;
    public GameObject planet;
    public GameObject enemy;
    public GameObject blackHole;

    private Vector2 spawnPos;

    [Header("Meteors")]
    public int meteorCount;
    public int minSpawnRangeMeteor;
    public int maxSpawnRangeMeteor;
    public float meteorSpawnTime;
    private float meteorTimer;

    [Header("Planets")]
    public int planetCount;
    public int minSpawnRangePlanet;
    public int maxSpawnRangePlanet;
    public float planetSpawnTime;
    private float planetTimer;

    [Header("Enemies")]
    public int enemyCount;
    public int minSpawnRangeEnemy;
    public int maxSpawnRangeEnemy;
    public float enemyMinSpawnTime;
    public float enemyMaxSpawnTime;
    public float enemySpawnTime;
    private float enemyTimer;    
    
    [Header("Black Hole")]
    public int blackHoleCount;
    public int minSpawnRangeBh;
    public int maxSpawnRangeBh;
    public float blackHoleSpawnTime;
    private float blackHoleTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        meteorTimer = meteorSpawnTime;
        planetTimer = planetSpawnTime;
        enemyTimer = 0;
        blackHoleTimer = blackHoleSpawnTime;

        enemySpawnTime = Random.Range(enemyMinSpawnTime, enemyMaxSpawnTime);
    }

    private void Update()
    {
        //different timers for the different object that have to be spawned
        meteorTimer += Time.deltaTime;
        planetTimer += Time.deltaTime;
        enemyTimer += Time.deltaTime;
        blackHoleTimer += Time.deltaTime;

        //spawns the objects (within the min and max spawn range) when the timer is done,
        //then resets the timer
        if (meteorTimer > meteorSpawnTime)
        {
            SpawnObjects(meteor, minSpawnRangeMeteor, maxSpawnRangeMeteor, meteorCount);
            meteorTimer = 0;
        }

        if (planetTimer > planetSpawnTime)
        {
            SpawnObjects(planet, minSpawnRangePlanet, maxSpawnRangePlanet, planetCount);
            planetTimer = 0;
        }

        if (enemyTimer > enemySpawnTime)
        {
            SpawnObjects(enemy, minSpawnRangeEnemy, maxSpawnRangeEnemy, enemyCount);
            enemySpawnTime = Random.Range(enemyMinSpawnTime, enemyMaxSpawnTime);
            enemyTimer = 0;
        }
        
        if (blackHoleTimer > blackHoleSpawnTime)
        {
            SpawnObjects(blackHole, minSpawnRangeBh, maxSpawnRangeBh, blackHoleCount);
            blackHoleTimer = 0;
        }
    }

    public void SetSpawnPosition(int maxSpawnRange)
    {
        //Set the spawn position (of any object) to a certain position between the player and the maxSpawnRange)
        spawnPos = new Vector2(
                Random.Range(player.transform.position.x - maxSpawnRange, player.transform.position.x + maxSpawnRange),
                Random.Range(player.transform.position.y - maxSpawnRange, player.transform.position.y + maxSpawnRange));
    }

    public void SpawnObjects(GameObject gameObject, int minSpawnRange, int maxSpawnRange, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SetSpawnPosition(maxSpawnRange);

            float distance = Vector2.Distance(spawnPos, player.transform.position);

            //if the object has spawned between the player and minSpawnRange
            //set the spawnPosition again until it's between the min and max spawn range
            while (distance < minSpawnRange)
            {
                SetSpawnPosition(maxSpawnRange);
                distance = Vector2.Distance(spawnPos, player.transform.position);
            }

            Instantiate(gameObject, new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
        }
    }
}
