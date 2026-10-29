using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Unity.Services.Core;

public enum GameMode
{
    Timed,
    Endless
}

public readonly struct TimedModeIDs
{
    public const string HighScore = "TIMED_highScores";
    public const string MostFruits = "TIMED_maxFruitsSliced";
    public const string TotalFruits = "TIMED_totalFruitsSliced";
    public const string TotalBombs = "TIMED_totalBombsSliced";
    public const string PerfectGames = "TIMED_perfectGames";
}

public class LeaderboardManager : MonoBehaviour
{
    private FirebaseManager _fbManager;
    private GameMode _chosenGameMode;

    private UnityEvent<GameMode> _onGameModeUpdated;

    [SerializeField] private ScoreLeaderboard leaderboard;
    
    [Header("UI Elements")]
    [SerializeField] private TMP_Dropdown modeSelector;
    [SerializeField] private GameObject timedModePanel;
    [SerializeField] private GameObject endlessModePanel;
    [SerializeField] private TMP_Text nameText;

    [Header("Timed Mode - Player")]
    [SerializeField] private TMP_Text highScore_PLAYER_TIMED;
    [SerializeField] private TMP_Text mostFruits_PLAYER_TIMED;
    [SerializeField] private TMP_Text totalFruits_PLAYER_TIMED;
    [SerializeField] private TMP_Text totalbombs_PLAYER_TIMED;
    [SerializeField] private TMP_Text perfectGames_PLAYER_TIMED;
    [Header("Timed Mode - Top")]
    [SerializeField] private TMP_Text highScore_TOP_TIMED;
    [SerializeField] private TMP_Text mostFruits_TOP_TIMED;
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

    private void Start()
    {
        GameManager.Instance.onPlayerNameUpdated.AddListener(UpdateNameTag);
    }

    private void OnEnable()
    {
        if(UnityServices.State == ServicesInitializationState.Initialized)
        //Initialize menu to Timed Mode stats
        ChangeChosenGameMode();
    }

    private void UpdateNameTag(string playerName)
    {
        nameText.text = playerName;
    }

    public async void UpdateTimedMode()
    {
        ScoreboardEntry HSResult = await leaderboard.GET_SCORE(TimedModeIDs.HighScore);
        highScore_PLAYER_TIMED.text = $"{HSResult.score} (Rank {HSResult.rank})";
        
        ScoreboardEntry MFResult = await leaderboard.GET_SCORE(TimedModeIDs.MostFruits);
        highScore_PLAYER_TIMED.text = $"{MFResult.score} (Rank {MFResult.rank})";
        
        ScoreboardEntry TFResult = await leaderboard.GET_SCORE(TimedModeIDs.TotalFruits);
        highScore_PLAYER_TIMED.text = $"{TFResult.score} (Rank {TFResult.rank})";
        
        ScoreboardEntry TBResult = await leaderboard.GET_SCORE(TimedModeIDs.TotalBombs);
        highScore_PLAYER_TIMED.text = $"{TBResult.score} (Rank {TBResult.rank})";
        
        ScoreboardEntry PGResult = await leaderboard.GET_SCORE(TimedModeIDs.PerfectGames);
        highScore_PLAYER_TIMED.text = $"{PGResult.score} (Rank {PGResult.rank})";
    }

    public void UpdateEndlessMode()
    {
        Debug.Log("Endless mode is not yet implemented!");
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
