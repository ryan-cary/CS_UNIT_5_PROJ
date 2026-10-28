using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    private float _timeElapsed;
    private int _fruitSliced;
    private int _fruitMissed;

    [SerializeField] private int maxFruitMissed = 3;

    public UnityEvent onFruitSliced;
    public UnityEvent onFruitMissed;
    
    public float TimeElapsed => _timeElapsed;
    public int FruitSlicedCount => _fruitSliced;
    public int FruitMissedCount => _fruitMissed;

    void Start()
    {
        onFruitMissed.AddListener(AddFruitMissed);
        onFruitSliced.AddListener(AddFruitSliced);
    }
    
    void Update()
    {
        _timeElapsed = Time.timeSinceLevelLoad;
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

    private void GameOver()
    {
        Debug.Log("You lose!");
    }
}
