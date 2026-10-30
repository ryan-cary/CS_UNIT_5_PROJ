using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    #region Leaderboards

    [SerializeField] private ScoreLeaderboard leaderboard;

    private void PostScores()
    {
        try
        {
            leaderboard.POST_SCORE(TimedModeIDs.HighScore, _totalScore);
            leaderboard.POST_SCORE(TimedModeIDs.MostFruits, _fruitSliced);
            leaderboard.POST_SCORE(TimedModeIDs.TotalFruits, _fruitSliced);
            leaderboard.POST_SCORE(TimedModeIDs.TotalBombs, _bombsSliced);
            if (_fruitMissed == 0)
                leaderboard.POST_SCORE(TimedModeIDs.PerfectGames, 1);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error posting scores: {e.Message}");
        }
    }

    #endregion
    
    private int _fruitSliced;
    private int _fruitMissed;
    private int _bombsSliced;
    private float _timeRemaining;

    private int _totalScore;

    [SerializeField] private float timeLimit = 90;
    [SerializeField] private int maxFruitMissed = 3;

    public UnityEvent onFruitSliced;
    public UnityEvent onFruitMissed;
    public UnityEvent onBombSliced;
    public UnityEvent onGameOver;
    
    public float TimeRemaining => _timeRemaining;
    public int FruitSlicedCount => _fruitSliced;
    public int FruitMissedCount => _fruitMissed;
    public int BombsSlicedCount => _bombsSliced;

    public float TotalScore => _totalScore;

    void Start()
    {
        Initialize();
        leaderboard = FindFirstObjectByType<ScoreLeaderboard>();
        onFruitMissed.AddListener(AddFruitMissed);
        onFruitSliced.AddListener(AddFruitSliced);
        onBombSliced.AddListener(AddBombSliced);
    }
    
    void Update()
    {
        if (GameManager.Instance.State == GameState.Play)
        {
            DoTimeRemaining();
        }
    }

    private void Initialize()
    {
        _fruitSliced = 0;
        _fruitMissed = 0;
        _bombsSliced = 0;
        _timeRemaining = timeLimit;
    }

    void DoTimeRemaining()
    {
        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0)
        {
            GameOver();
        }
    }

    private void AddFruitSliced()
    {
        if (GameManager.Instance.State == GameState.Play)
            _fruitSliced++;
    }

    private void AddFruitMissed()
    {
        if (GameManager.Instance.State == GameState.Play)
            _fruitMissed++;
        
        if (FruitMissedCount > maxFruitMissed)
            GameOver();
    }

    private void AddBombSliced()
    {
        if (GameManager.Instance.State == GameState.Play)
            _bombsSliced++;
    }

    private void GameOver()
    {
        GameManager.Instance.ChangeGameState(GameState.GameOver);

        _totalScore = FruitSlicedCount * 5 + FruitMissedCount * -5 + _bombsSliced * -10;
        
        PostScores();
        
        onGameOver?.Invoke();
    }

    public void PlayAgain()
    {
        Initialize();
        GameManager.Instance.ChangeGameState(GameState.Play);
    }

    public void BackToMenu()
    {
        Initialize();
        GameManager.Instance.LoadMenu();
    }
}
