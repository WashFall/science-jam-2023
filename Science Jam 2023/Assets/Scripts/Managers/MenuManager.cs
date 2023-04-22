using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu, resultMenu;
    
    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        
        if(!pauseMenu.activeSelf) 
            GameManager.Instance.UpdateGameState(GameState.playing);
        else if(pauseMenu.activeSelf)
            GameManager.Instance.UpdateGameState(GameState.pauses);

    }

    public void ToggleResultMenu()
    {
        resultMenu.SetActive(!resultMenu.activeSelf);
    }
}
