using System;
using KnightInBorderlands.Scripts.Components;
using UnityEngine;
using UnityEngine.UI;

namespace KnightInBorderlands.Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private HealthComponent _playerHealth;
        [SerializeField] private Image _totalHealthBar;
        [SerializeField] private Image _currentHealthBar;

        private void Start()
        {
            _totalHealthBar.fillAmount = _playerHealth.CurrentHealth / 5;
        }
        
        private void Update()
        {
            _currentHealthBar.fillAmount = _playerHealth.CurrentHealth / 5;
        }
    }
}