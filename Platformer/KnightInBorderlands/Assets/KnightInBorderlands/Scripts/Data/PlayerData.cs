using KnightInBorderlands.Scripts.Components;
using KnightInBorderlands.Scripts.LevelManager;
using UnityEngine;

namespace KnightInBorderlands.Scripts.Data
{
    [System.Serializable]
    public class PlayerData
    {
        public float _hp;
        public string _position;
        public CollectableType _inventoryItem;
        
        public string CheckPointData = JsonUtility.ToJson(CheckPoint.Instance.CheckPointPosition);
        public float Health = HealthComponent.Instance.CurrentHealth;
        public CollectableType inventoryItemData = InventoryComponent.Instance.InventoryItem;
        
        public PlayerData()
        {
            _hp = Health; 
            _position = CheckPointData;
            _inventoryItem = inventoryItemData;
        }
    }
}