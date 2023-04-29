using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private void Start()
    {
        _startButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("Scenes/Level1");
        });
    }
}