using System;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    [HideInInspector]
    public InputManager inputManager;
    [Header("Managers")]
    public MenuManager menuManager;
    public ImageManager imageManager;

    [Header("Variables")]
    public GameState gameState;

    public Vector3 leftImagePosition, rightImagePosition;
    
    private GameObject[] currentImages = new GameObject[2];

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
                break;
            case KeyCode.Space:
                DisplayImagePair();
                break;
        }
    }

    public void DisplayImagePair()
    {
        currentImages = imageManager.LoadImagePair();

        // currentImages[0].transform.position = new Vector3(leftImagePosition.x, 0, leftImagePosition.z);
        // currentImages[1].transform.position = new Vector3(rightImagePosition.x, 0, rightImagePosition.z);
        currentImages[0].transform.position = leftImagePosition;
        currentImages[1].transform.position = rightImagePosition;
        currentImages[1].GetComponent<Floater>().offset = 0.5f;

        foreach (var image in currentImages)
        {
            image.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position * -1);
            
            image.SetActive(true);
        }
    }
}

public enum GameState
{
    playing,
    result,
    pauses
}
