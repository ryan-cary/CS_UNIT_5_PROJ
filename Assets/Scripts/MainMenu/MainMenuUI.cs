using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Menu Elements")]
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject creditsPanel;

    [Header("Name")] [SerializeField] private TMP_InputField nameTag;

    [Header("Buttons")] 
    [SerializeField] private Button timedButton;
    [SerializeField] private Button endlessButton;

    private WheelOfFruit[] _wheels;

    private void Start()
    {
        _wheels = FindObjectsByType<WheelOfFruit>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        if (GameManager.Instance.PlayerName != null)
            nameTag.text = GameManager.Instance.PlayerName;
        
        timedButton.onClick.AddListener(GameManager.Instance.LoadTimedMode);
        endlessButton.onClick.AddListener(GameManager.Instance.LoadEndlessMode);
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

    public void ToggleCredits()
    {
        if (creditsPanel.activeSelf)
            creditsPanel.SetActive(false);
        else
            creditsPanel.SetActive(true);
    }

    public void UpdatePlayerName()
    {
        GameManager.Instance.onPlayerNameUpdated?.Invoke(nameTag.text);
    }
}
