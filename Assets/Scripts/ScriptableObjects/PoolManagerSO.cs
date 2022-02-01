using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolManagerSO", menuName = "ScriptableObjects/PoolManagerSO", order = 1)]
public class PoolManagerSO : ScriptableObject
{
    public Pool<GameTypes.Bullets>[] BulletPool;
    public Pool<GameTypes.Enemies>[] EnemiesPool;
    public Pool<GameTypes.Sounds>[] SoundsPool;
}
