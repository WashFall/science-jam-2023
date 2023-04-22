using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void KeyPressed(KeyCode keyCode);

    public KeyPressed keyPressed;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            keyPressed(KeyCode.Escape);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            keyPressed(KeyCode.Space);
        }
    }
}
