using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD _instance;
    
    // -- SCORE --
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highscore;
    
    // -- PLAYER STATS --
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _bullets;
    

    private void Awake()
    {
        _instance = this;
    }

    public void SetScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void SetHighscore(int highscore)
    {
        _highscore.text = "Highscore: " + highscore.ToString();
    }
    
    public void SetHealth(int health)
    {
        _health.text = "Health: " + health.ToString();
    }
    
    public void SetBullets(int bullets)
    {
        _bullets.text = "Bullets: " +  bullets.ToString();
    }
}
