using UnityEngine;

namespace Tower
{
    public class TowerHealth : MonoBehaviour
    {
        public int HealthPoint;
        
        public void TakeDamage(int damage)
        {
            HealthPoint -= damage;
        
            if (HealthPoint <= 0)
            {
                TowerUI.Instance.HideUI();
                Destroy(gameObject);
            }
        }
    }
}