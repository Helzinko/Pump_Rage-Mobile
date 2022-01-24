using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Player player;

    public static GameManager _instance;

    private void Awake()
    {
        _instance = this;
    }
}
