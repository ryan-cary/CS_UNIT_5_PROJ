using UnityEngine;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;
using Unity.Services.Core;

using System;
using Newtonsoft.Json;
using Unity.Services.Leaderboards.Models;

public class ScoreLeaderboard : MonoBehaviour
{
    private const string LEADERBOARD_ID = "timed_leaderboard";

    public int score = 0;

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
        LeaderboardEntry scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LEADERBOARD_ID, score);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    public async void GET_SCORE()
    {
        LeaderboardEntry scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LEADERBOARD_ID);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }
}
