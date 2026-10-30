using System;
using TMPro;
using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    [Header("Text Fields")] 
    [SerializeField] private TMP_Text fruitsText;
    [SerializeField] private TMP_Text missesText;
    [SerializeField] private TMP_Text bombsText;
    [SerializeField] private TMP_Text totalScoreText;

    private void Start()
    {
        gameOverUI.SetActive(false);
        
        ScoreManager.Instance.onGameOver.AddListener(EnableGameOverScreen);
        ScoreManager.Instance.onGameOver.AddListener(UpdateFields);
    }

    private void UpdateFields()
    {
        fruitsText.text = $"Fruits Sliced: {ScoreManager.Instance.FruitSlicedCount}";
        fruitsText.text = $"Fruits Missed: {ScoreManager.Instance.FruitMissedCount}";
        fruitsText.text = $"Bomb Penalty: {ScoreManager.Instance.Penalty}";
        fruitsText.text = $"Total: {ScoreManager.Instance.TotalScore}";
    }

    private void EnableGameOverScreen()
    {
        gameOverUI.gameObject.SetActive(true);
    }
}
