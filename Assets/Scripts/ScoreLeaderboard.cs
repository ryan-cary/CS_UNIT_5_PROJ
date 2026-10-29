using UnityEngine;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;
using Unity.Services.Core;

using System;
using Newtonsoft.Json;
using Unity.Services.Leaderboards.Models;
using TMPro;

[Serializable]
public struct ScoreboardEntry
{
    public string playerId;
    public string playerName;
    public int rank;
    public float score;
    public string updatedTime;
}

public class ScoreLeaderboard : MonoBehaviour
{
    private async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public async void POST_SCORE(string leaderboardID, int score)
    {
        LeaderboardEntry scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, score);
        
        string json = JsonConvert.SerializeObject(scoreResponse);
        ScoreboardEntry result = JsonUtility.FromJson<ScoreboardEntry>(json);

        Debug.Log(result.score);
    }

    public async void GET_SCORE(string leaderboardID)
    {
        LeaderboardEntry scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardID);
        
        string json = JsonConvert.SerializeObject(scoreResponse);
        ScoreboardEntry result = JsonUtility.FromJson<ScoreboardEntry>(json);
        
        Debug.Log(result.score);
    }

    #region DEBUG

    private const string DEBUG_LEADERBOARD = "DEBUG_LEADERBOARD";
    
    [SerializeField] private int DEBUG_score = 0;
    [SerializeField] private int DEBUG_highScore = 0;
    [SerializeField] private TMP_Text DEBUG_ScoreText;
    
    public void DEBUG_PostScore()
    {
        POST_SCORE(DEBUG_LEADERBOARD, DEBUG_score);
        DEBUG_UpdateScoreDisplay();
    }

    public void DEBUG_GetScore()
    {
        GET_SCORE(DEBUG_LEADERBOARD);
        DEBUG_UpdateScoreDisplay();
    }

    public void DEBUG_AddScore()
    {
        DEBUG_score++;
        DEBUG_UpdateScoreDisplay();
    }

    public void DEBUG_SubtractScore()
    {
        DEBUG_score--;
        DEBUG_UpdateScoreDisplay();
    }

    private void DEBUG_UpdateScoreDisplay()
    {
        DEBUG_ScoreText.text = "Score = " + DEBUG_score + "\nHigh Score = " + DEBUG_highScore;
    }

    #endregion
    
}
