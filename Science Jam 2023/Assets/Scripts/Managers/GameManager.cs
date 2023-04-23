using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

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
    public int right, wrong;
    public List<string> wrongTypes = new();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        inputManager.keyPressed += HandleInput;
        UpdateGameState(GameState.pauses);
    }

    public async void StartGame()
    {
        menuManager.ToggleStartMenu();
        UpdateGameState(GameState.playing);
        await Buffer(2);
        DisplayImagePair();
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
                Time.timeScale = 0;
                string wrongType = "perfect";
                if(wrongTypes.Count > 0)
                    wrongType = wrongTypes[0];
                menuManager.ToggleResultMenu(right, wrongType);
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
        }
    }

    public async void DisplayImagePair()
    {
        if (currentImages[0] is not null)
        {
            foreach (var image in currentImages)
            {
                image.gameObject.SetActive(false);
            }
        }
        currentImages = imageManager.LoadImagePair();

        // currentImages[0].transform.position = new Vector3(leftImagePosition.x, 0, leftImagePosition.z);
        // currentImages[1].transform.position = new Vector3(rightImagePosition.x, 0, rightImagePosition.z);
        currentImages[0].transform.position = leftImagePosition;
        currentImages[1].transform.position = rightImagePosition;

        // foreach (var image in currentImages)
        // {
        //     image.SetActive(true);
        // }
        currentImages[0].SetActive(true);
        await Buffer(0.8f);
        currentImages[1].SetActive(true);
    }

    public async Task Buffer(float buffer)
    {
        float endTime = Time.time + buffer;
        while (Time.time < endTime)
        {
            await Task.Yield();
        }
    }

    public async void SubmitAnswer(GameObject chosenImage, float leftOrRight)
    {
        foreach (var image in currentImages)
        {
            if (image != chosenImage)
            {
                image.GetComponent<Floater>().canFloat = false;
                image.GetComponent<Rigidbody>().useGravity = true;
                image.GetComponent<Move3D>().canMove = false;
            }
        }

        if (chosenImage.GetComponent<ImageData>().isReal)
            right++;
        else
        {
            wrongTypes.Add(chosenImage.GetComponent<ImageData>().type);
            wrong++;
        }

        chosenImage.GetComponent<Floater>().canFloat = false;
        chosenImage.GetComponent<Rigidbody>().useGravity = true;
        chosenImage.GetComponent<Move3D>().canMove = false;
        float force = 10;
        if (leftOrRight < 0) force = -4;
        else if (leftOrRight > 0) force = 4;
        chosenImage.GetComponent<Rigidbody>().AddForce(force, 0, 0, ForceMode.Impulse);

        await Buffer(1.3f);
        if (right + wrong == 10)
        {
            Destroy(currentImages[0]);
            Destroy(currentImages[1]);
            UpdateGameState(GameState.result);
        }
        else
        {
            Destroy(currentImages[0]);
            Destroy(currentImages[1]);
            DisplayImagePair();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

public enum GameState
{
    playing,
    result,
    pauses
}
