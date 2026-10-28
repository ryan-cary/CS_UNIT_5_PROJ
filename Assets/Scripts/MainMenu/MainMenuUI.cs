using System;
using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Header("Menu Elements")]
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject logo;

    [Header("Name")] [SerializeField] private TMP_InputField nameTag;

    private WheelOfFruit[] _wheels;

    private void Start()
    {
        _wheels = FindObjectsByType<WheelOfFruit>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }

    public void ShowLeaderboard()
    {
        leaderboard.SetActive(true);
        
        mainMenu.gameObject.SetActive(false);
        logo.gameObject.SetActive(false);
        foreach (WheelOfFruit wheel in _wheels)
        {
            wheel.gameObject.SetActive(false);
        }
    }

    public void HideLeaderboard()
    {
        leaderboard.SetActive(false);
        
        mainMenu.gameObject.SetActive(true);
        logo.gameObject.SetActive(true);
        foreach (WheelOfFruit wheel in _wheels)
        {
            wheel.gameObject.SetActive(true);
        }
    }

    public void UpdatePlayerName()
    {
        GameManager.Instance.onPlayerNameUpdated?.Invoke(nameTag.text);
    }
}
