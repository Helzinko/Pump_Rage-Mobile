using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysHandler : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    
    public static EnemysHandler instance;

    private List<Enemy> enemies;

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

        enemies = new List<Enemy>();
    }

    void Start()
    {
        GameManager._instance.GameplayReset.AddListener(Reset);
        
        InitialSpawn();
    }

    public void RandomSpawn()
    {
        int rand = Random.Range(0, 4);

        var enemy = PoolManager.instance.SpawnEnemy(GameTypes.Enemies.normal, spawnPoints[rand].position,
            spawnPoints[rand].rotation).GetComponent<Enemy>();
        
        enemies.Add(enemy);
        enemy.Reset();
    }

    private void InitialSpawn()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            var enemy = PoolManager.instance.SpawnEnemy(GameTypes.Enemies.normal, spawnPoint.position,
                spawnPoint.rotation).GetComponent<Enemy>();
            
            enemies.Add(enemy);
            enemy.Reset();
        }
    }

    private void Reset()
    {
        foreach (var enemy in enemies.ToArray())
        {
            enemy.Kill(true);
        }
        
        enemies.Clear();

        InitialSpawn();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
