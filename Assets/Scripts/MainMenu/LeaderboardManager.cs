using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public enum GameMode
{
    Timed,
    Endless
}

public class LeaderboardManager : MonoBehaviour
{
    private FirebaseManager _fbManager;
    private GameMode _chosenGameMode;

    private UnityEvent<GameMode> _onGameModeUpdated;

    [SerializeField] private TMP_Dropdown modeSelector;
    [SerializeField] private GameObject timedModePanel;
    [SerializeField] private GameObject endlessModePanel;

    [Header("Timed Mode - Player")]
    [SerializeField] private TMP_Text mostFruits_PLAYER_TIMED;
    [SerializeField] private TMP_Text mostBombs_PLAYER_TIMED;
    [SerializeField] private TMP_Text totalFruits_PLAYER_TIMED;
    [SerializeField] private TMP_Text totalbombs_PLAYER_TIMED;
    [SerializeField] private TMP_Text perfectGames_PLAYER_TIMED;
    [Header("Timed Mode - Top")]
    [SerializeField] private TMP_Text mostFruits_TOP_TIMED;
    [SerializeField] private TMP_Text mostBombs_TOP_TIMED;
    [SerializeField] private TMP_Text totalFruits_TOP_TIMED;
    [SerializeField] private TMP_Text totalbombs_TOP_TIMED;
    [SerializeField] private TMP_Text perfectGames_TOP_TIMED;
    
    [Header("Endless Mode - Player")] 
    [SerializeField] private TMP_Text mostFruits_PLAYER_ENDLESS;
    [SerializeField] private TMP_Text totalFruits_PLAYER_ENDLESS;
    [SerializeField] private TMP_Text totalbombs_PLAYER_ENDLESS;
    [SerializeField] private TMP_Text perfectGames_PLAYER_ENDLESS;
    [Header("Endless Mode - Top")] 
    [SerializeField] private TMP_Text mostFruits_TOP_ENDLESS;
    [SerializeField] private TMP_Text totalFruits_TOP_ENDLESS;
    [SerializeField] private TMP_Text totalbombs_TOP_ENDLESS;
    [SerializeField] private TMP_Text perfectGames_TOP_ENDLESS;

    private void Awake()
    {
        _fbManager = FindFirstObjectByType<FirebaseManager>();
    }

    private void OnEnable()
    {
        //Initialize menu to Timed Mode stats
        ChangeChosenGameMode();
    }

    public void UpdateTimedMode()
    {
        //firebase call for info
    }

    public void UpdateEndlessMode()
    {
        //firebase call for info   
    }

    private void EnableTimedMode()
    {
        endlessModePanel.gameObject.SetActive(false);
        timedModePanel.gameObject.SetActive(true);
    }
    
    private void EnableEndlessMode()
    {
        endlessModePanel.gameObject.SetActive(true);
        timedModePanel.gameObject.SetActive(false);
    }

    public void ChangeChosenGameMode()
    {
        int index = modeSelector.value;
        _chosenGameMode = (GameMode)index;

        switch (index)
        {
            case(0):
                UpdateTimedMode();
                EnableTimedMode();
                break;
            case(1):
                UpdateEndlessMode();
                EnableEndlessMode();
                break;
        }
    }
}
