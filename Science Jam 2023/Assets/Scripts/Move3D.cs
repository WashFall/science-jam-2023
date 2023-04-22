using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Move3D : MonoBehaviour
{
    private Camera mainCamera;
    private float CameraZDistance;
    private Floater floater;

    void Start()
    {
        floater = GetComponent<Floater>();
        mainCamera = Camera.main;
        CameraZDistance =
            mainCamera.WorldToScreenPoint(transform.position).z; //z axis of the game object for screen view
    }

    private float counter = 0;
    public bool canMove = true;

    void OnMouseDrag()
    {
        if(canMove)
        {
            floater.canFloat = false;
            Vector3 ScreenPosition =
                new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                    CameraZDistance); //z axis added to screen point 
            Vector3 NewWorldPosition =
                mainCamera.ScreenToWorldPoint(ScreenPosition); //Screen point converted to world point

            transform.position = NewWorldPosition;
        }
    }

    private void OnMouseUp()
    {
        if(canMove)
        {
            if (transform.position.x <= -6 || transform.position.x >= 6)
            {
                GameManager.Instance.SubmitAnswer(gameObject, transform.position.x);
            }
            else
            {
                floater.canFloat = true;
            }
        }
    }
}