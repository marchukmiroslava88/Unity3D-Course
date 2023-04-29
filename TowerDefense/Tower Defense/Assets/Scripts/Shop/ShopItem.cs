using Tower;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private TowerType _type;
        [SerializeField] private Toggle _toggle;
        private BuildManager _buildManager;
        private ShopItems _shopItems;
        private PlayerStats _playerStats;
        
        private void Start()
        {
            _buildManager = BuildManager.Instance;
            _shopItems = ShopItems.Instance;
            _playerStats = PlayerStats.Instance;
            
            var price = _shopItems.ShopData[_type].Price;
            
            _toggle.onValueChanged.AddListener(delegate(bool on)
            {
                TowerUI.Instance.HideUI();
                
                if (on)
                {
                    if (_playerStats.Money >= price)
                    {
                        _buildManager.SetTowerToBuild(_type);
                    }
                    else
                    {
                        _toggle.isOn = false;
                        _buildManager.SetTowerToBuild(TowerType.None);
                    }
                }
                else
                {
                    _buildManager.SetTowerToBuild(TowerType.None);
                }
            });
            
            _toggle.GetComponentInChildren<Text>().text = price.ToString();
        }
    }
}