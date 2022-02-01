using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Player player;

    public static GameManager _instance;

    public UnityEvent GameplayReset;

    private void Awake()
    {
        _instance = this;
        GameplayReset = new UnityEvent();
    }

    public void RestartGameplay()
    {
        GameplayReset.Invoke();
    }
}
