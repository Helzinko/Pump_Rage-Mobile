using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager _instance;

    private int _currentScore = 0;

    private void Start()
    {
        Reset();
        
        GameManager._instance.GameplayReset.AddListener(Reset);
    }

    private void Reset()
    {
        _currentScore = 0;
        HUD._instance.SetScore(_currentScore);
    }
    private void Awake()
    {
        _instance = this;
    }

    public void AddScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        HUD._instance.SetScore(_currentScore);
    }
}
