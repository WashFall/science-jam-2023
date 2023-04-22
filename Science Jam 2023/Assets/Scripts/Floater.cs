using UnityEngine;

public class Floater : MonoBehaviour 
{
    public float amplitude = 0.35f;
    public float frequency = 0.3f;
    public bool canFloat, canMove;
    
    private Vector3 posOffset;
    private Vector3 tempPos;
    private float timeLastFrame;
    private float timer;

    void OnEnable ()
    {
        canMove = true;
    }

    void Update () 
    {
        // Float up/down with a Sin()
        if (canMove)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, 1.7f, transform.position.z), 5 * Time.deltaTime);
            if (transform.position.y <= (1.7f + 0.1f))
            {
                canMove = false;
                canFloat = true;
                posOffset = transform.position;
                timeLastFrame = Time.fixedTime;
            }
        }
        if(canFloat)
        {
            timer += timeLastFrame - Time.fixedTime;
            timeLastFrame = Time.fixedTime;
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(timer * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
    }
}