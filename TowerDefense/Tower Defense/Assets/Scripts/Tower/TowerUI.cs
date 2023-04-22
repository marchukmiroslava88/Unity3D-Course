using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tower
{
    public class TowerUI : MonoBehaviour
    {
        public static TowerUI Instance;
        private Tower _tower;
        private GameObject _towerObj;
        private int _towerModifyPrice;
        private int _towerSellPrice;
        private TowerType _towerType;
        [SerializeField] private Button _modifyButton;
        [SerializeField] private TextMeshProUGUI _modifyPrice;
        [SerializeField] private Button _sellButton;
        [SerializeField] private TextMeshProUGUI _sellPrice;
        
        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            gameObject.SetActive(false);
        }
        
        private void Start()
        {
            _modifyButton.onClick.AddListener(delegate
            {
                var towerLevelData = _tower.TowerLevelData;
                
                if (PlayerStats.Instance.Money >= _towerModifyPrice)
                {
                    if (_tower.Level != towerLevelData.Count - 1)
                    {
                        towerLevelData[_tower.Level].TowerHead.SetActive(false);
                        _tower.Level += 1; 
                        towerLevelData[_tower.Level].TowerHead.SetActive(true);
                    }

                    PlayerStats.Instance.RemoveMoney(_towerModifyPrice);
                }
            });
            
            _sellButton.onClick.AddListener(delegate
            {
                HideUI();
                Destroy(_towerObj);
                PlayerStats.Instance.AddMoney(_towerSellPrice);
            });
        }

        public void SetCurrentTower(GameObject tower)
        {
            _towerObj = tower;
            _tower = tower.GetComponent<Tower>();
            _towerModifyPrice = ShopItems.Instance.ShopDataDictionary[_tower.Type].ModifyPrice;
            _towerSellPrice = ShopItems.Instance.ShopDataDictionary[_tower.Type].SellPrice;
            _modifyPrice.text = _towerModifyPrice.ToString();
            _sellPrice.text = _towerSellPrice.ToString();
        }
        
        public void ShowUI()
        {
            transform.position = new Vector3(_tower.transform.position.x, 1.8f, _tower.transform.position.z);
            gameObject.SetActive(true);
        }

        public void HideUI()
        {
            gameObject.SetActive(false);
        }
    }
}
