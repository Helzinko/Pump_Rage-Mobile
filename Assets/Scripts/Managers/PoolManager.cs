using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    
    private Dictionary<GameTypes.Bullets, Queue<GameObject>> BulletPool;
    private Dictionary<GameTypes.Enemies, Queue<GameObject>> EnemyPool;
    private Dictionary<GameTypes.Sounds, Queue<GameObject>> SoundsPool;

    [SerializeField] private PoolManagerSO _pools;
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

    public async Task Init()
    {
        BulletPool = new Dictionary<GameTypes.Bullets, Queue<GameObject>>();
        EnemyPool = new Dictionary<GameTypes.Enemies, Queue<GameObject>>();
        SoundsPool = new Dictionary<GameTypes.Sounds, Queue<GameObject>>();

        foreach (var bullet in _pools.BulletPool)
        {
            Queue<GameObject> bulletPool = new Queue<GameObject>();

            for (int i = 0; i < bullet.poolSize; i++)
            {
                GameObject _bullet = Instantiate(bullet.prefab, gameObject.transform);
                _bullet.SetActive(false);
                bulletPool.Enqueue(_bullet);
            }
            
            BulletPool.Add(bullet.type, bulletPool);
        }
        
        foreach (var enemy in _pools.EnemiesPool)
        {
            Queue<GameObject> enemyPool = new Queue<GameObject>();

            for (int i = 0; i < enemy.poolSize; i++)
            {
                GameObject _enemy = Instantiate(enemy.prefab, gameObject.transform);
                _enemy.SetActive(false);
                enemyPool.Enqueue(_enemy);
            }
            
            EnemyPool.Add(enemy.type, enemyPool);
        }
        
        foreach (var sound in _pools.SoundsPool)
        {
            Queue<GameObject> soundPool = new Queue<GameObject>();

            for (int i = 0; i < sound.poolSize; i++)
            {
                GameObject _sound = Instantiate(sound.prefab, gameObject.transform);
                _sound.SetActive(false);
                soundPool.Enqueue(_sound);
            }
            
            SoundsPool.Add(sound.type, soundPool);
        }
    } 

    public GameObject SpawnBullet(GameTypes.Bullets bullet, Vector3 pos, Quaternion rot)
    {
        if(!BulletPool.ContainsKey(bullet))
            return null;
        
        var bulletToSpawn = BulletPool[bullet].Dequeue();

        bulletToSpawn.SetActive(true);
        bulletToSpawn.transform.position = pos;
        bulletToSpawn.transform.rotation = rot;
        
        BulletPool[bullet].Enqueue(bulletToSpawn);

        return bulletToSpawn;
    }
    
    public GameObject SpawnEnemy(GameTypes.Enemies enemy, Vector3 pos, Quaternion rot)
    {
        if(!EnemyPool.ContainsKey(enemy))
            return null;
        
        var enemyToSpawn = EnemyPool[enemy].Dequeue();

        enemyToSpawn.SetActive(true);
        enemyToSpawn.transform.position = pos;
        enemyToSpawn.transform.rotation = rot;
        
        EnemyPool[enemy].Enqueue(enemyToSpawn);

        return enemyToSpawn;
    }
    
    public GameObject SpawnSound(GameTypes.Sounds sound)
    {
        var soundToSpawn = SoundsPool[sound].Dequeue();
        soundToSpawn.SetActive(true);
        SoundsPool[sound].Enqueue(soundToSpawn);
        return soundToSpawn;
    }
}

[Serializable]
public class Pool <T>
{
    public T type;
    public GameObject prefab;
    public int poolSize;
}
