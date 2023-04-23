using UnityEngine;
using UnityEngine.UI;

public class PlayAgainButton : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(GameManager.Instance.RestartGame);
    }
}
