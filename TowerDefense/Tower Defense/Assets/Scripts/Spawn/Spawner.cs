using System.Collections;
using Helpers;
using TMPro;
using UnityEngine;

namespace Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _enemyValue;
        [SerializeField] private GameObject _winUI;
        public SpawnManager SpawnManagerValues;
        private int CurrentWave;
        public static int EnemiesAlive;

        private void Awake()
        {
            EventAggregator.Subscribe<EnemiesAliveChangeEvent>(EnemiesAliveChangeHandler);
        }

        private void EnemiesAliveChangeHandler(object sender, EnemiesAliveChangeEvent eventData)
        {
            EnemiesAlive = eventData.EnemiesAlive;
            _enemyValue.text = EnemiesAlive.ToString();
            if (WaveTimer.IsFinalWave && EnemiesAlive == 0)
            {
                _winUI.gameObject.SetActive(true);
            }
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            var currentSpawnPointIndex = 0;
            var wave = SpawnManagerValues.Waves[CurrentWave];
            WaveTimer.WaveNumber = CurrentWave + 1;
            EnemiesAliveChangeHandler(this, new EnemiesAliveChangeEvent{EnemiesAlive = wave.Enemies.Length});

            if (CurrentWave == 0)
            {
                WaveTimer.TimeRemaining = SpawnManagerValues.TimeToFirstWaveStart;
                WaveTimer.TimerIsRunning = true;
                yield return new WaitForSeconds(SpawnManagerValues.TimeToFirstWaveStart);   
            }
        
            foreach (var enemy in wave.Enemies)
            {
                var enemyPrefab = Resources.Load(enemy.ToString()) as GameObject;

                if (!enemyPrefab) continue;
                Instantiate(enemyPrefab, SpawnManagerValues.SpawnPoints[currentSpawnPointIndex],  Quaternion.Euler(0,-90,0));
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
}
