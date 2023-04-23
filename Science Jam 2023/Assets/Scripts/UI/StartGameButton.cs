using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GameManager.Instance.StartGame);
    }
}
