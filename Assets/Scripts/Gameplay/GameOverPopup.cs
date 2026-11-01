using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button replayButton;

    [Header("Text Fields")] 
    [SerializeField] private TMP_Text fruitsText;
    [SerializeField] private TMP_Text missesText;
    [SerializeField] private TMP_Text bombsText;
    [SerializeField] private TMP_Text totalScoreText;

    private void Start()
    {
        gameOverUI.SetActive(false);
        
        ScoreManager.Instance.onGameOver?.AddListener(EnableGameOverScreen);
        ScoreManager.Instance.onGameOver?.AddListener(UpdateFields);
        
        menuButton.onClick.AddListener(GameManager.Instance.LoadMenu);
        replayButton.onClick.AddListener(ScoreManager.Instance.PlayAgain);
    }

    private void UpdateFields()
    {
        fruitsText.text = $"Fruits Sliced: {ScoreManager.Instance.FruitSlicedCount} x 5pts";
        missesText.text = $"Fruits Missed: {ScoreManager.Instance.FruitMissedCount} x -5pts";
        bombsText.text = $"Bombs Sliced: {ScoreManager.Instance.BombsSlicedCount} x -10pts";
        totalScoreText.text = $"Total: {ScoreManager.Instance.TotalScore}";
    }

    private void EnableGameOverScreen()
    {
        gameOverUI.gameObject.SetActive(true);
        //FindFirstObjectByType<RewardedAdsButton>().LoadAd();
    }

    public void playRewardedAd()
    {
        //Unity legacy ads is deprecated so we're doing this instead of rewarded ads
        ScoreManager.Instance.AddTime(30);
        HapticsController.Instance.SendToast("Legacy ads are deprecated, but we'll give you time anyway!");
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.onGameOver?.RemoveListener(EnableGameOverScreen);
        ScoreManager.Instance.onGameOver?.RemoveListener(UpdateFields);

        menuButton.onClick.RemoveAllListeners();
        replayButton.onClick.RemoveAllListeners();
    }
}
