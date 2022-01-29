using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    
    public static EnemySpawner instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            var enemy = PoolManager.instance.SpawnEnemy(GameTypes.Enemies.normal, spawnPoint.position,
                spawnPoint.rotation);
            enemy.GetComponent<Enemy>().Reset();
        }
    }

    public void RandomSpawn()
    {
        int rand = Random.Range(0, 4);
        
        var enemy = PoolManager.instance.SpawnEnemy(GameTypes.Enemies.normal, spawnPoints[rand].position,
            spawnPoints[rand].rotation);
        enemy.GetComponent<Enemy>().Reset();
    }
}
