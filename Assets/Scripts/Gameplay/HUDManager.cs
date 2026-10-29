using System;
using TMPro;
using UnityEngine;

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

    private void UpdateTime()
    {
        timerText.text = TimeSpan.FromSeconds(ScoreManager.Instance.TimeRemaining).ToString(@"mm\:ss");
    }
}
