using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = ScoreManager.Instance.FruitSlicedCount.ToString();
    }

    //TODO move this to GameManager or a LevelManager
    private void UpdateTime()
    {
        timerText.text = TimeSpan.FromSeconds(ScoreManager.Instance.TimeRemaining).ToString(@"mm\:ss");
    }
}
