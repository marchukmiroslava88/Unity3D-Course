using TMPro;
using UnityEngine;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerValue;
    [SerializeField] private TextMeshProUGUI _timerLabel;
    public static float TimeRemaining;
    public static bool TimerIsRunning;
    public static bool IsFinalWave;
    public static int WaveNumber;
    
    private void Update()
    {
        if (IsFinalWave)
        {
            TimerIsRunning = false;
            _timerValue.alignment = TextAlignmentOptions.Center;
            _timerValue.text = "Final wave";
            _timerLabel.text = "";
        }
        else
        {
            if (!TimerIsRunning) return;
            if (TimeRemaining > 0)
            {
                _timerLabel.alignment = TextAlignmentOptions.Left;
                _timerLabel.text = "Next wave in";
                DisplayTime(TimeRemaining);
                TimeRemaining -= Time.deltaTime;
            }
            else
            {
                _timerLabel.alignment = TextAlignmentOptions.Center;
                _timerLabel.text = $"Wave {WaveNumber}";
                _timerValue.text = "";
                TimerIsRunning = false;
            }
        }
    }
    
    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        _timerValue.text = $"{minutes:00}:{seconds:00}";
    }
}
