using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicLoop : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(1);
    }
}
