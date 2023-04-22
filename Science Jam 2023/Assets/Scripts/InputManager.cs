using System;
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
    }
}
