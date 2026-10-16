using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private float DEBUG_LevelTimeInSeconds = 0.0f;

    private float _startTime;

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "SCORE";
    }

    //TODO move this to GameManager or a LevelManager
    private void UpdateTime()
    {
        
        float timeRemaining = DEBUG_LevelTimeInSeconds - Time.timeSinceLevelLoad;
        timerText.text = TimeSpan.FromSeconds(timeRemaining).ToString(@"mm\:ss");

        if (timeRemaining <= 0)
        {
            Debug.Log("Stopping time!");
            timerText.text = "00:00";
            StopAllCoroutines();
        }
    }
}
