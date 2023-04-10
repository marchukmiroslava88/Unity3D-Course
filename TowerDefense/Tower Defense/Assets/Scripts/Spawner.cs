using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnManagerScriptableObject SpawnManagerValues;
    private int CurrentWave;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    
    private IEnumerator SpawnEnemies()
    {
        var currentSpawnPointIndex = 0;
        var wave = SpawnManagerValues.Waves[CurrentWave];
        WaveTimer.WaveNumber = CurrentWave + 1;
        
        if (CurrentWave == 0)
        {
            WaveTimer.TimeRemaining = SpawnManagerValues.TimeToFirstWaveStart;
            WaveTimer.TimerIsRunning = true;
            yield return new WaitForSeconds(SpawnManagerValues.TimeToFirstWaveStart);   
        }
        
        foreach (var enemy in wave.Enemies)
        {
            Instantiate(enemy, SpawnManagerValues.SpawnPoints[currentSpawnPointIndex],  Quaternion.Euler(0,-90,0));
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % SpawnManagerValues.SpawnPoints.Length;

            yield return new WaitForSeconds(wave.TimeBetweenEnemySpawn);
        }

        if (!WaveTimer.IsFinalWave)
        {
            WaveTimer.TimeRemaining = SpawnManagerValues.TimeBetweenWaves;
            WaveTimer.TimerIsRunning = true;
        }
        yield return new WaitForSeconds(SpawnManagerValues.TimeBetweenWaves);
        
        CurrentWave += 1;
           
        if(CurrentWave == SpawnManagerValues.Waves.Count - 1)
        {
            WaveTimer.IsFinalWave = true;
        }
        
        if (CurrentWave != SpawnManagerValues.Waves.Count)
        {
            StartCoroutine(SpawnEnemies());
        }
    }
}
