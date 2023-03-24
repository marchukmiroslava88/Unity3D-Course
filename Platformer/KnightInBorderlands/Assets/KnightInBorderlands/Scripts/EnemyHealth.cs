using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace KnightInBorderlands.Scripts
{
    public class EnemyHealth : MonoBehaviour
    {
        public float Health;
        public float CurrentHealth;
        [SerializeField] private Animator _animator;
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private static readonly int Attacked = Animator.StringToHash("Attacked");

        private void Start()
        {
            CurrentHealth = Health;
        }

        private void Update()
        {
            if (Health < CurrentHealth)
            {
                CurrentHealth = Health;
                _animator.SetTrigger(Attacked);
            }
            
            if (Health <= 0)
            {
                _animator.SetBool(IsDead, true);
            }
        }
    }
}