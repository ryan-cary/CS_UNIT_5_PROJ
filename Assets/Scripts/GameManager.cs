using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Play,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    private GameState _gameState;
    
    public GameState State => _gameState;

    public UnityEvent<GameState, GameState> onGameStateChanged = new UnityEvent<GameState, GameState>();

    public void Awake()
    {
        _gameState = GameState.Menu;
    }

    public void ChangeGameState(GameState target)
    {
        GameState previous = _gameState;
        _gameState = target;
        onGameStateChanged?.Invoke(previous, _gameState);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
