using TMPro;
using UnityEngine;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerValue;
    [SerializeField] private TextMeshProUGUI TimerLabel;
    public static float TimeRemaining;
    public static bool TimerIsRunning;
    public static bool IsFinalWave;
    public static int WaveNumber;
    
    private void Update()
    {
        if (IsFinalWave)
        {
            TimerIsRunning = false;
            TimerValue.alignment = TextAlignmentOptions.Center;
            TimerValue.text = "Final wave";
            TimerLabel.text = "";
        }
        else
        {
            if (!TimerIsRunning) return;
            if (TimeRemaining > 0)
            {
                TimerLabel.alignment = TextAlignmentOptions.Left;
                TimerLabel.text = "Next wave in";
                DisplayTime(TimeRemaining);
                TimeRemaining -= Time.deltaTime;
            }
            else
            {
                TimerLabel.alignment = TextAlignmentOptions.Center;
                TimerLabel.text = $"Wave {WaveNumber}";
                TimerValue.text = "";
                TimerIsRunning = false;
            }
        }
    }
    
    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimerValue.text = $"{minutes:00}:{seconds:00}";
    }
}
