using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Button _menuButton;
    
    private void Start()
    {
        _menuButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("Scenes/Menu");
        });
    }
}
