using System;
using UnityEngine;

[Serializable]
public class Wave
{
    public float TimeBetweenEnemySpawn;
    [SerializeField] public GameObject[] Enemies;
}