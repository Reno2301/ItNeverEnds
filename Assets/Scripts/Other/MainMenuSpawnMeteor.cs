using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpawnMeteor : MonoBehaviour
{
    public Vector2 targetPos;
    public Vector2 spawnPos;
    public GameObject meteor;
    public int meteorCount;
    public int maxMeteors;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnMeteor();
    }

    private void SpawnMeteor()
    {
        if (meteorCount < maxMeteors)
        {
            meteorCount++;
            spawnPos = new Vector2(Random.Range(targetPos.x - 40, targetPos.x), Random.Range(targetPos.y + 30, targetPos.y + 10));
            Instantiate(meteor, spawnPos, Quaternion.identity);
        }
    }
}
