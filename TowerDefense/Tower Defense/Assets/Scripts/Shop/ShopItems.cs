using System.Collections.Generic;
using Tower;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopItems : MonoBehaviour
    {
        public static ShopItems Instance;
        [SerializeField] private ToggleGroup _shopItemsToggleGroup;
        [SerializeField] private List<ShopData> _shopData;
        public Dictionary<TowerType, ShopData> ShopData;
        
        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            ShopData = new Dictionary<TowerType, ShopData>();
            foreach (var data in _shopData)
            {
                ShopData[data.Type] = data;
            }
        }

        public void SetAllShopItemsOff()
        {
            _shopItemsToggleGroup.SetAllTogglesOff();
        }
    }
}