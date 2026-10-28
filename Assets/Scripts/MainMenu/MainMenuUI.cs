using System;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject logo;

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

    public void UpdatePlayerName(string playerName)
    {
        
    }
}
