using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    private int _fruitSliced;
    private int _fruitMissed;
    private int _scorePenaltyIncurred;
    private float _timeRemaining;

    private float _totalScore;

    [SerializeField] private float timeLimit = 90;
    [SerializeField] private int maxFruitMissed = 3;

    public UnityEvent onFruitSliced;
    public UnityEvent onFruitMissed;

    public UnityEvent onGameOver;
    
    public float TimeRemaining => _timeRemaining;
    public int FruitSlicedCount => _fruitSliced;
    public int FruitMissedCount => _fruitMissed;
    public int Penalty => _scorePenaltyIncurred;

    public float TotalScore => _totalScore;

    void Start()
    {
        Initialize();
        onFruitMissed.AddListener(AddFruitMissed);
        onFruitSliced.AddListener(AddFruitSliced);
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
        _scorePenaltyIncurred = 0;
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
            _fruitSliced += 1;
    }

    private void AddFruitMissed()
    {
        if (GameManager.Instance.State == GameState.Play)
            _fruitMissed += 1;
        
        if (FruitMissedCount > maxFruitMissed)
            GameOver();
    }

    public void Penalize(int penalty)
    {
        if (GameManager.Instance.State == GameState.Play)
            _scorePenaltyIncurred -= penalty;
    }

    private void GameOver()
    {
        GameManager.Instance.ChangeGameState(GameState.GameOver);

        _totalScore = FruitSlicedCount * 5 + FruitMissedCount * -5 + _scorePenaltyIncurred;
        
        onGameOver?.Invoke();
    }

    public void BackToMenu()
    {
        GameManager.Instance.LoadMenu();
    }
}
