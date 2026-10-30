using System;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        HUD.gameObject.SetActive(true);
        ScoreManager.Instance.onGameOver?.AddListener(HideHUD);
    }

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

    private void HideHUD()
    {
        HUD.gameObject.SetActive(false);
    }
}
