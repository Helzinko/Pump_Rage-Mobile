using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager _instance;

    private int _currentScore = 0;

    private int highscore;

    private void Start()
    {
        Reset();
        
        GameManager._instance.GameplayReset.AddListener(Reset);

        highscore = SaveSystem.Load();
        
        HUD._instance.SetHighscore(highscore);
    }

    private void Reset()
    {
        _currentScore = 0;
        HUD._instance.SetScore(_currentScore);
        HUD._instance.SetHighscore(highscore);
    }
    private void Awake()
    {
        _instance = this;
    }

    public void AddScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        HUD._instance.SetScore(_currentScore);

        if (_currentScore > highscore)
        {
            highscore = _currentScore;
            SaveSystem.Save(highscore);
            HUD._instance.SetHighscore(highscore);
        }
    }
}
