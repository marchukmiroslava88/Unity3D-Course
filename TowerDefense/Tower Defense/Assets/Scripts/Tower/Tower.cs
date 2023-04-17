using System.Collections;
using UnityEngine;

namespace Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private LayerMask _raycastLayers;
        [SerializeField] private int _rayDistance;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private TowerScriptableObject _towerValues;
        [SerializeField] private int _modifyLevel;
        private float fireCountdown;
        private TowerModify modifyTower;
            
        private void Awake()
        {
            modifyTower = TowerModify.Instance;
        }

        private void Update()
        {
            // Test Tower Modify
            // if (Input.GetKeyDown(KeyCode.A))
            // {
            //     if (_modifyLevel != _towerValues.TowerModifyLevels.Count - 1)
            //     {
            //         _modifyLevel += 1;
            //     }
            // }
        
            var ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out var hitResult, _rayDistance, _raycastLayers)) {
                // Debug.Log($"Raycast hit: {hitResult.collider.name}");
 
                if (fireCountdown <= 0f)
                {
                    StartCoroutine(Shoot());
                    fireCountdown = 1f / _towerValues.TowerModifyLevels[_modifyLevel].FireRate; 
                }

                fireCountdown -= Time.deltaTime;
            }
        }

        private void OnMouseDown()
        {
            Vector2 screenPoint =
                RectTransformUtility.WorldToScreenPoint(Camera.main, gameObject.transform.position);
            modifyTower.Button.anchoredPosition = screenPoint - modifyTower.CanvasRectT.sizeDelta / 2f;
            modifyTower.Button.gameObject.SetActive(true);
            // _modifyButton.gameObject.SetActive(!_modifyButton.gameObject.activeSelf);
        }
        
        private IEnumerator Shoot()
        {
            var tower = _towerValues.TowerModifyLevels[_modifyLevel];
        
            for (int i = 0; i < tower.Bullets; i++)
            {
                yield return new WaitForSeconds(tower.TimeBetweenShots);
                Instantiate(tower.BulletPrefab, _firePoint.position, _firePoint.rotation);
            }
        }
    }
}
