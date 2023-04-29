using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tower
{
    public class TowerUI : MonoBehaviour
    {
        public static TowerUI Instance;
        [SerializeField] private Button _modifyButton;
        [SerializeField] private TextMeshProUGUI _modifyPrice;
        [SerializeField] private Button _sellButton;
        [SerializeField] private TextMeshProUGUI _sellPrice;
        [SerializeField] private LayerMask _layer;
        private Tower _tower;
        private int _towerModifyPrice;
        private int _towerSellPrice;
        
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
                        PlayerStats.Instance.RemoveMoney(_towerModifyPrice);
                    }
                    else
                    {
                        _modifyButton.interactable = false;
                    }
                }
            });
            
            _sellButton.onClick.AddListener(delegate
            {
                HideUI();
                Destroy(_tower.gameObject);
                PlayerStats.Instance.AddMoney(_towerSellPrice);
            });
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(EventSystem.current.IsPointerOverGameObject()) return;
                
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out var raycastHit))
                {
                    if (((1 << raycastHit.collider.gameObject.layer) & _layer) == 0)
                    {
                        HideUI();
                    }
                }
            }
        }

        public void SetCurrentTower(GameObject tower)
        {
            _tower = tower.GetComponent<Tower>();
            _towerModifyPrice = ShopItems.Instance.ShopData[_tower.Type].ModifyPrice;
            _towerSellPrice = ShopItems.Instance.ShopData[_tower.Type].SellPrice;
            _modifyPrice.text = _towerModifyPrice.ToString();
            _sellPrice.text = _towerSellPrice.ToString();
            _modifyButton.interactable = _tower.Level != _tower.TowerLevelData.Count - 1;
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
