using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu, resultMenu, startMenu;
    public TMP_Text scoreText, feedbackText;

    private Dictionary<string, string> feedback;

    private void Start()
    {
        feedback = new Dictionary<string, string>();
        feedback.Add("baby","You need to work on recognizing humanoids.");
        feedback.Add("bicycle", "You can't ride a fake bike, try to find the real one next time.");
        feedback.Add("building", "Gothenburg is a lovely city, but the view you chose is not a real one.");
        feedback.Add("car1", "Maybe it's hard to see, but look closer on the black cars next time.");
        feedback.Add("car2", "Those red cars looked cool, but you need to pay attention to the surroundings.");
        feedback.Add("dog", "AI is pretty good at creating dogs. It's hard, but find the strange details.");
        feedback.Add("nature1", "Nature can look very magical, but don't let the strange shade fool you.");
        feedback.Add("nature2", "The sun shining through the woods looks convincing, but focus on the trees next time.");
        feedback.Add("nature4", "Sunflowers rarely grows very orderly. Try to look for strange patterns.");
        feedback.Add("tiger",
            "The tigers may look similar, but a few key differences stand out if you focus on their faces.");
        feedback.Add("perfect", "Perfect score! Hope you understand the importance of critical judgement in this age of technology.");
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        
        if(!pauseMenu.activeSelf) 
            GameManager.Instance.UpdateGameState(GameState.playing);
        else if(pauseMenu.activeSelf)
            GameManager.Instance.UpdateGameState(GameState.pauses);

    }

    public void ToggleResultMenu(float rightAnswerCount, string feedbackType)
    {
        scoreText.text = string.Format("{0}/10", rightAnswerCount);
        feedbackText.text = feedback[feedbackType];
        resultMenu.SetActive(!resultMenu.activeSelf);
    }
    
    public void ToggleStartMenu()
    {
        startMenu.SetActive(!startMenu.activeSelf);
    }
}
