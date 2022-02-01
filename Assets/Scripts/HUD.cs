using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD _instance;
    
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        _scoreText.text = 0.ToString();
    }

    public void SetScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
