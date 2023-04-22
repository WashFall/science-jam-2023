using System;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    [HideInInspector]
    public InputManager inputManager;
    public MenuManager menuManager;

    public GameState gameState;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        inputManager.keyPressed += HandleInput;

        UpdateGameState(GameState.playing);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;
        CheckGameState();
    }

    private void CheckGameState()
    {
        switch (gameState)
        {
            case GameState.playing:
                Time.timeScale = 1;
                break;
            case GameState.pauses:
                Time.timeScale = 0;
                break;
            case GameState.result:
                break;
        }
    }

    private void HandleInput(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.Escape:
                try
                {
                    menuManager.TogglePauseMenu();
                }
                catch(Exception e)
                {
                    Debug.LogError(e);
                }
                print(Time.timeScale);
                break;
        }
    }
}

public enum GameState
{
    playing,
    result,
    pauses
}
