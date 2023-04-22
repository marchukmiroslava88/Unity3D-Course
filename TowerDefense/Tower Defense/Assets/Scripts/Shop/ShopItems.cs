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
        [SerializeField] private List<ShopData> _ShopDatas;
        public Dictionary<TowerType, ShopData> ShopDataDictionary;
        
        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            ShopDataDictionary = new Dictionary<TowerType, ShopData>();
            PrepareDictionary();
        }

        private void PrepareDictionary()
        {
            foreach (var data in _ShopDatas)
            {
                ShopDataDictionary[data.Type] = data;
            }
        }

        public void SetAllShopItemsOff()
        {
            _shopItemsToggleGroup.SetAllTogglesOff();
        }
    }
}