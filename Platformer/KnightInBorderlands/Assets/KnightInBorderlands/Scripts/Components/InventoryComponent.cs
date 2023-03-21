using UnityEngine;

namespace KnightInBorderlands.Scripts.Components
{
    public class InventoryComponent : MonoBehaviour
    {
        public static InventoryComponent Instance;
        [SerializeField] private CollectableType _inventoryItem;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void AddToInventory(GameObject obj)
        {
            if (obj.TryGetComponent<Collectables>(out var collectable))
            {
                _inventoryItem = collectable.Type;
                obj.SetActive(false);
                SaveSystem.SavePlayer();
            }
        }

        public void RemoveFromInventory()
        {
            
        }

        public CollectableType InventoryItem => _inventoryItem;
    }
}