using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    private float _timeElapsed;
    private int _fruitSliced;
    private int _fruitMissed;
    private int _scorePenaltyIncurred;
    private float _timeRemaining;

    [SerializeField] private float timeLimit = 90;
    [SerializeField] private int maxFruitMissed = 3;

    public UnityEvent onFruitSliced;
    public UnityEvent onFruitMissed;
    
    public float TimeElapsed => _timeElapsed;
    public float TimeRemaining => _timeRemaining;
    public int FruitSlicedCount => _fruitSliced;
    public int FruitMissedCount => _fruitMissed;
    public int Penalty => _scorePenaltyIncurred;

    void Start()
    {
        onFruitMissed.AddListener(AddFruitMissed);
        onFruitSliced.AddListener(AddFruitSliced);
    }
    
    void Update()
    {
        _timeElapsed = Time.timeSinceLevelLoad;
        DoTimeRemaining();
    }

    void DoTimeRemaining()
    {
        _timeRemaining = timeLimit - _timeElapsed;

        if (_timeRemaining <= 0)
        {
            GameOver();
        }
    }

    private void AddFruitSliced()
    {
        _fruitSliced += 1;
    }

    private void AddFruitMissed()
    {
        _fruitMissed += 1;
        if (FruitMissedCount > maxFruitMissed)
            GameOver();
    }

    public void Penalize(int penalty)
    {
        _scorePenaltyIncurred += penalty;
    }

    private void GameOver()
    {
        GameManager.Instance.ChangeGameState(GameState.GameOver);
        UpdateLeaderboards();
        Debug.Log("Game Complete!");
    }

    private void UpdateLeaderboards()
    {
        throw new NotImplementedException();
    }
}
