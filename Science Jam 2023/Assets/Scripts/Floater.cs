using UnityEngine;

public class Floater : MonoBehaviour 
{
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float offset = 0;
    
    Vector3 posOffset;
    Vector3 tempPos;

    void Start () 
    {
        posOffset = transform.position;
    }

    void Update () 
    {
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency + offset) * amplitude;

        transform.position = tempPos;
    }
}