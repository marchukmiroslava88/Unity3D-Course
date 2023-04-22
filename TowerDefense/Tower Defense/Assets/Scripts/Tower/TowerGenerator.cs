using System;
using UnityEngine;

namespace Tower
{
    public class TowerGenerator : MonoBehaviour
    {
        [SerializeField] private int _generateMoneyTimer = 20;
        [SerializeField] private int _generateMoneyValue = 50;
        private float _generateMoneyCountdown;

        private void Start()
        {
            _generateMoneyCountdown = _generateMoneyTimer;
        }

        private void Update()
        {
            if (_generateMoneyCountdown <= 0f)
            {
                PlayerStats.Instance.AddMoney(_generateMoneyValue);
                _generateMoneyCountdown = _generateMoneyTimer;
            }
            _generateMoneyCountdown -= Time.deltaTime;
        }
    }
}
