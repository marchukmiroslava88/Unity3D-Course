using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

namespace KnightInBorderlands.Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        public static HealthComponent Instance;
        [SerializeField] private float _startingHealth;
        [SerializeField] private float _currentHealth;
        [SerializeField] private TakeDamageEvent _onTakeDamageEvent;
        [SerializeField] private DieEvent _onDieEvent;
        private const float _cooldownTime = 0.5f;
        private float _cooldownTimeCounter = 0.5f;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            _currentHealth = _startingHealth;
        }

        public void TakeDamage(float _damage)
        {
            if (Time.time > _cooldownTimeCounter)
            {
                _currentHealth = Math.Clamp(_currentHealth - _damage, 0, _startingHealth);
                _cooldownTimeCounter = Time.time + _cooldownTime;
                
                if (_currentHealth > 0)
                {
                    _onTakeDamageEvent?.Invoke(gameObject);
                }
                else
                {
                    _onDieEvent?.Invoke(gameObject);
                }
            }
        }

        public void AddHealth(float value)
        {
            if (_currentHealth < _startingHealth)
            {
                _currentHealth = Math.Clamp(_currentHealth + value, 0, _startingHealth);
            }
        }
        
        public void RestoreHealth() => _currentHealth = _startingHealth;

        public float CurrentHealth => _currentHealth;
    }
    
    [Serializable]
    public class TakeDamageEvent : UnityEvent<GameObject>
    {
    }
    
    [Serializable]
    public class DieEvent : UnityEvent<GameObject>
    {
    }
}