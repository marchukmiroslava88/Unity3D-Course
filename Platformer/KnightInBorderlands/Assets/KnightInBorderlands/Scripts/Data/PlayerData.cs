using KnightInBorderlands.Scripts.Components;
using KnightInBorderlands.Scripts.LevelManager;
using UnityEngine;

namespace KnightInBorderlands.Scripts.Data
{
    [System.Serializable]
    public class PlayerData
    {
        public float Hp;
        public string Position;
        
        public string CheckPointData = JsonUtility.ToJson(CheckPoint.Instance.CheckPointPosition);
        public float Health = HealthComponent.Instance.CurrentHealth;
        
        public PlayerData()
        {
            Hp = Health; 
            Position = CheckPointData;
        }
    }
}