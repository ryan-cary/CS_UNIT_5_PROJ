using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Play,
    Pause,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    public void Start()
    {
        _gameState = GameState.Menu;
        onPlayerNameUpdated.AddListener(UpdateName);
    }
    
    #region Game State

    private GameState _gameState;
    
    public GameState State => _gameState;

    public UnityEvent<GameState, GameState> onGameStateChanged = new UnityEvent<GameState, GameState>();

    public void ChangeGameState(GameState target)
    {
        GameState previous = _gameState;
        _gameState = target;
        onGameStateChanged?.Invoke(previous, _gameState);
    }

    #endregion

    #region Player Name

    private string _playerName;

    public string PlayerName => _playerName;

    [CanBeNull] public UnityEvent<string> onPlayerNameUpdated = new UnityEvent<string>();

    public void UpdateName(string newName)
    {
        _playerName = newName;
    }

    #endregion

    #region Scene Loading

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadTimedMode()
    {
        LoadScene("CuttingBoard");
        ChangeGameState(GameState.Play);
    }

    public void LoadEndlessMode()
    {
        throw new NotImplementedException();
    }

    #endregion
    
}
