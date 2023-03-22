using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace KnightInBorderlands.Scripts
{
    public class EnemyHealth : MonoBehaviour
    {
        public float _health;
        public float _currentHealth;
        [SerializeField] private Animator _animator;
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private static readonly int Attacked = Animator.StringToHash("Attacked");

        private void Start()
        {
            _currentHealth = _health;
        }

        private void Update()
        {
            if (_health < _currentHealth)
            {
                _currentHealth = _health;
                _animator.SetTrigger(Attacked);
            }
            
            if (_health <= 0)
            {
                _animator.SetBool(IsDead, true);
            }
        }
    }
}