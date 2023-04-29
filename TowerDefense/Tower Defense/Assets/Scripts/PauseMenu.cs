using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _menuButton;
    
    private void Start()
    {
        _settingButton.onClick.AddListener(delegate
        {
            Time.timeScale = 0;
        });
        
        _resumeButton.onClick.AddListener(delegate
        {
            Time.timeScale = 1;
        });
        
        _menuButton.onClick.AddListener(delegate
        {
             SceneManager.LoadScene("Scenes/Menu");
        });
    }
}
