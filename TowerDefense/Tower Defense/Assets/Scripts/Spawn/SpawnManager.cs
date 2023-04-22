using System.Collections.Generic;
using UnityEngine;

namespace Spawn
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "ScriptableObjects/SpawnManager", order = 1)]
    public class SpawnManager : ScriptableObject
    {
        public float TimeToFirstWaveStart;
        public float TimeBetweenWaves;
        public Vector3[] SpawnPoints;
        public List<Wave> Waves;
    }
}