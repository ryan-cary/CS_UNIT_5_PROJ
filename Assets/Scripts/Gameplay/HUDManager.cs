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
        ScoreManager.Instance.onFruitSliced?.AddListener(UpdateScore);
    }

    void Update()
    {
        UpdateTime();
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

    private void OnDestroy()
    {
        ScoreManager.Instance.onGameOver?.RemoveListener(HideHUD);
        ScoreManager.Instance.onFruitSliced?.RemoveListener(UpdateScore);
    }
}
