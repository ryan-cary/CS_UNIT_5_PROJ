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
        onBombSliced.AddListener(ClearBoard);
    }
    
    void Update()
    {
        if (GameManager.Instance.State == GameState.Play)
        {
            DoTimeRemaining();
        }
    }

    private void OnDestroy()
    {
        onFruitMissed?.RemoveAllListeners();
        onFruitSliced?.RemoveAllListeners();
        onBombSliced?.RemoveAllListeners();
        onGameOver?.RemoveAllListeners();
    }

    public void Initialize()
    {
        ClearBoard();
        
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
        
        /*
        // Commenting this out as it was originally meant to be used in the endless mode
        //
        if (FruitMissedCount > maxFruitMissed)
            GameOver();
            */
    }

    private void AddBombSliced()
    {
        if (GameManager.Instance.State == GameState.Play)
            _bombsSliced++;
    }
    
    private void ClearBoard()
    {
        
        Fruit[] fruitToBeDestroyed = FindObjectsByType<Fruit>(FindObjectsSortMode.None);
        Bomb[] bombsToBeDestroyed = FindObjectsByType<Bomb>(FindObjectsSortMode.None);

        foreach (Fruit f in (fruitToBeDestroyed))
        {
            Destroy(f.gameObject);
        }

        foreach (Bomb b in bombsToBeDestroyed)
        {
            Destroy(b.gameObject);
        }
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

    public void AddTime(float timeToAdd)
    {
        _timeRemaining += timeToAdd;
        GameManager.Instance.ChangeGameState(GameState.Play);
    }
}
