using Tower;
using UnityEngine;

namespace Shop
{
    [System.Serializable]
    public class ShopData
    {
        [SerializeField] private TowerType _type;
        [SerializeField] private int _price;
        [SerializeField] private int _modifyPrice;
        [SerializeField] private int _sellPrice;
        
        public TowerType Type => _type;
        public int Price => _price;
        public int ModifyPrice => _modifyPrice;
        public int SellPrice => _sellPrice;
    }
}