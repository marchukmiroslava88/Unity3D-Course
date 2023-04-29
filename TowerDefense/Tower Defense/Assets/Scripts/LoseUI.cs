using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _menuButton;
    
    private void Start()
    {
        _retryButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("Scenes/Level1");
        });
        
        _menuButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("Scenes/Menu");
        });
    }
}
