using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuWindow : MonoBehaviour
    {
        [SerializeField] private Slider _musicVolume;
        [SerializeField] private Slider _masterVolume;
        [SerializeField] private TMP_Dropdown _resolution;
        [SerializeField] private TMP_Dropdown _quality;
        [SerializeField] private Toggle _fullScreen;
        [SerializeField] private Toggle _backgroundSound;
        [SerializeField] private Toggle _allowFriends;
        [SerializeField] private Toggle _enableChallenging;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _cinematicButton;
        
        private void Start()
        {
            _musicVolume.onValueChanged.AddListener(delegate(float value)
            {
                Debug.Log($"MusicVolume: {value}");
            });
            _masterVolume.onValueChanged.AddListener(delegate(float value)
            {
                Debug.Log($"MasterVolume: {value}");
            });
            
            _resolution.onValueChanged.AddListener(delegate(int value)
            {
                Debug.Log($"_resolution: {value}");
            });
            _quality.onValueChanged.AddListener(delegate(int value)
            {
                Debug.Log($"_quality: {value}");
            });
            
            _fullScreen.onValueChanged.AddListener(delegate(bool value)
            {
                Debug.Log($"_fullScreenToggle: {value}");
            });
            
            _backgroundSound.onValueChanged.AddListener(delegate(bool value)
            {
                Debug.Log($"_backgroundSoundToggle: {value}");
            });
            
            _allowFriends.onValueChanged.AddListener(delegate(bool value)
            {
                Debug.Log($"_allowFriendsToggle: {value}");
            });
            
            _enableChallenging.onValueChanged.AddListener(delegate(bool value)
            {
                Debug.Log($"_enableChallengingToggle: {value}");
            });
            _creditsButton.onClick.AddListener(delegate()
            {
                Debug.Log($"CreditsButton Click");
            });
            _cinematicButton.onClick.AddListener(delegate()
            {
                Debug.Log($"CinematicButton Click");
            });
        }
    }
}