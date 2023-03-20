using System;
using System.Collections;
using DragonBones;
using KnightInBorderlands.Scripts.LevelManager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KnightInBorderlands.Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        public static HealthComponent Instance;
        [SerializeField] private float _startingHealth;
        public float _currentHealth { get; private set; }
        [SerializeField] private UnityArmatureComponent _armature;
        [SerializeField] private PlayerInput _playerInputActions;

        private bool _isWaitForReload = true;
        
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
            _currentHealth = Math.Clamp(_currentHealth - _damage, 0, _startingHealth);

            if (_currentHealth > 0)
            {
                _armature.animation.Play("hurt a", 1);
            }
            else
            {
                _armature.animation.Play("die a", 1);
                _playerInputActions.actions.Disable();
                StartCoroutine(Reload());
            }
        }
        
        public void TakeDamageAndReload(float _damage)
        {
            if (_isWaitForReload)
            {
                _isWaitForReload = false;
                _currentHealth = Math.Clamp(_currentHealth - _damage, 0, _startingHealth); 
                _playerInputActions.actions.Disable();
                if (_currentHealth > 0)
                {
                    _armature.animation.Play("hurt a", 1);
                }
                else
                {
                    _armature.animation.Play("die a", 1);
                }
                StartCoroutine(Reload());
            }
        }

        public void AddHealth(float value)
        {
            if (_currentHealth < _startingHealth)
            {
                _currentHealth = Math.Clamp(_currentHealth + value, 0, _startingHealth);
            }
        }
         
        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(0.4f);
            if (_currentHealth == 0)
            {
                _currentHealth = _startingHealth;
            }
            CheckPoint.Instance.SpawnHero();
            yield return new WaitForSeconds(0.35f);
            _playerInputActions.actions.Enable();
            _isWaitForReload = true; 
        }
        
        public void RestoreHealth()
        {
            _currentHealth = _startingHealth;
        }
    }
}