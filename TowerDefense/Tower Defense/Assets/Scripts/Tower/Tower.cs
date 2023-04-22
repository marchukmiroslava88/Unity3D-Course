using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private LayerMask _raycastLayers;
        [SerializeField] private int _rayDistance;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private TowerType _type;
        
        private float _fireCountdown;
        private TowerUI _towerUI;
        private bool _isUIOpen;
        
        public int Level;
        public TowerType Type => _type;
        public List<TowerLevelData> TowerLevelData;

        private void Awake()
        {
            _towerUI = TowerUI.Instance;
            TowerLevelData[Level].TowerHead.SetActive(true);
        }

        private void Update()
        {
            var ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out var hitResult, _rayDistance, _raycastLayers)) {
                if (_fireCountdown <= 0f)
                {
                    StartCoroutine(Shoot());
                    _fireCountdown = 1f / TowerLevelData[Level].FireRate; 
                }
                _fireCountdown -= Time.deltaTime;
            }
        }
  
        private void OnMouseDown()
        {
            if(EventSystem.current.IsPointerOverGameObject()) return;
            
            _towerUI.SetCurrentTower(gameObject);
            _towerUI.ShowUI();
        }

        private IEnumerator Shoot()
        {
            var tower = TowerLevelData[Level];
            for (int i = 0; i < tower.Bullets; i++)
            {
                yield return new WaitForSeconds(tower.TimeBetweenShots);
                Instantiate(tower.BulletPrefab, _firePoint.position, _firePoint.rotation);
            }
        }
    }
}
