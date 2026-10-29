using UnityEngine;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;
using Unity.Services.Core;

using System;
using Newtonsoft.Json;
using Unity.Services.Leaderboards.Models;

using UnityEngine.UI;
using TMPro;

public class ScoreLeaderboard : MonoBehaviour
{
    private const string LEADERBOARD_ID = "timed_leaderboard";

    [SerializeField] private int DEBUG_score = 0;
    [SerializeField] private int DEBUG_highScore = 0;
    [SerializeField] private TMP_Text DEBUG_ScoreText;

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

    public async void POST_SCORE()
    {
        LeaderboardEntry scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LEADERBOARD_ID, DEBUG_score);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    public async void GET_SCORE()
    {
        LeaderboardEntry scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LEADERBOARD_ID);
        
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
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
}
