using UnityEngine;
using UnityEngine.UI;

public class PlaybackSpeed : MonoBehaviour
{
    [SerializeField] private Button _playbackSpeedButton;
    private bool playFaster;

    private void Awake()
    {
        playFaster = false;
    }

    private void Start()
    {
        _playbackSpeedButton.onClick.AddListener(delegate
        {
            playFaster = !playFaster;
            Time.timeScale = playFaster ? 2 : 1;
        });
    }
}
