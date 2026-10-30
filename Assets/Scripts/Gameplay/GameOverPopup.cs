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
        fruitsText.text = $"Fruits Sliced: {ScoreManager.Instance.FruitSlicedCount} x 5";
        missesText.text = $"Fruits Missed: {ScoreManager.Instance.FruitMissedCount} x -5";
        bombsText.text = $"Bomb Penalty: {ScoreManager.Instance.Penalty}";
        totalScoreText.text = $"Total: {ScoreManager.Instance.TotalScore}";
    }

    private void EnableGameOverScreen()
    {
        gameOverUI.gameObject.SetActive(true);
    }
}
