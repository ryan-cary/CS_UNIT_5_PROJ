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
        //DEBUG_LogGameStateChange(previous);
    }

    private void DEBUG_LogGameStateChange(GameState prev)
    {
        Debug.Log($"Changed from {prev} to {_gameState}");
    }

    #endregion

    #region Player Name

    private string _playerName;

    public string PlayerName => _playerName;

    public UnityEvent<string> onPlayerNameUpdated = new UnityEvent<string>();

    public void UpdateName(string newName)
    {
        _playerName = newName;
        Debug.Log("New Active Player: " + _playerName);
    }

    #endregion

    #region Scene Loading

    private void LoadScene(string name)
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
        #if UNITY_ANDROID && !UNITY_EDITOR
        HapticsController.Instance.SendToast("Endless mode is not implemented yet! Sorry!");
        #else
        Debug.Log("Endless mode hasn't been implemented yet!");
        #endif
    }

    public void LoadMenu()
    {
        ScoreManager.Instance.Initialize();
        LoadScene("MainMenu");
        ChangeGameState(GameState.Menu);
    }

    #endregion
    
}
