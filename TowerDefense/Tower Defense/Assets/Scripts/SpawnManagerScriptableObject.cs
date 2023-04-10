using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SpawnManagerScriptableObject : ScriptableObject
{
    public float TimeToFirstWaveStart;
    public float TimeBetweenWaves;
    public Vector3[] SpawnPoints;
    public List<Wave> Waves = new();
}