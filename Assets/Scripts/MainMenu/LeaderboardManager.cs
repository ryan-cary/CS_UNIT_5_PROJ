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

public class LeaderboardManager : MonoBehaviour
{
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
        try //retrieve high score
        {
            ScoreboardEntry HSResult = await leaderboard.GET_SCORE(TimedModeIDs.HighScore);
            highScore_PLAYER_TIMED.text = $"{HSResult.score} (Rank {HSResult.rank})";
        }
        catch (Exception e)
        {
            Debug.LogError($"Error retrieving score: {e.Message}");
            highScore_PLAYER_TIMED.text = "0";
        }
        
        try //retrieve max fruits
        {
            ScoreboardEntry MFResult = await leaderboard.GET_SCORE(TimedModeIDs.MostFruits);
            mostFruits_PLAYER_TIMED.text = $"{MFResult.score} (Rank {MFResult.rank})";
        }
        catch (Exception e)
        {
            Debug.LogError($"Error retrieving score: {e.Message}");
            mostFruits_PLAYER_TIMED.text = "0";
        }
        
        try //retrieve lifetime fruits
        {
            ScoreboardEntry TFResult = await leaderboard.GET_SCORE(TimedModeIDs.TotalFruits);
            totalFruits_PLAYER_TIMED.text = $"{TFResult.score} (Rank {TFResult.rank})";
        }
        catch (Exception e)
        {
            Debug.LogError($"Error retrieving score: {e.Message}");
            totalFruits_PLAYER_TIMED.text = "0";
        }
        
        try //retrieve lifetime bombs
        {
            ScoreboardEntry TBResult = await leaderboard.GET_SCORE(TimedModeIDs.TotalBombs);
            totalbombs_PLAYER_TIMED.text = $"{TBResult.score} (Rank {TBResult.rank})";
        }
        catch (Exception e)
        {
            Debug.LogError($"Error retrieving score: {e.Message}");
            totalbombs_PLAYER_TIMED.text = "0";
        }
        
        try //retrieve lifetime perfect games
        {
            ScoreboardEntry PGResult = await leaderboard.GET_SCORE(TimedModeIDs.PerfectGames);
            perfectGames_PLAYER_TIMED.text = $"{PGResult.score} (Rank {PGResult.rank})";
        }
        catch (Exception e)
        {
            Debug.LogError($"Error retrieving score: {e.Message}");
            perfectGames_PLAYER_TIMED.text = "0";
        }
        
        /*
        try
        {
            ScoreboardEntry result = await leaderboard.GET_TOP_SCORE(TimedModeIDs.HighScore);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error retrieving score: {e.Message}");
            perfectGames_PLAYER_TIMED.text = "0";
        }
        */
    }

    public void UpdateEndlessMode()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        HapticsController.Instance.SendToast("Endless mode is not implemented yet! Sorry!");
#else
        Debug.Log("Endless mode hasn't been implemented yet!");
#endif
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
