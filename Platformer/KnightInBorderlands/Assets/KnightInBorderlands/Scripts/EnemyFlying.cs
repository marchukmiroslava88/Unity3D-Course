using System;
using UnityEngine;

namespace KnightInBorderlands.Scripts
{
    public class EnemyFlying : MonoBehaviour
    {
        public float speed;
        [SerializeField] private GameObject _player;
        [SerializeField] private bool _isChase;
        public Transform startingPoint;
        
        private void Update()
        {
            if (_player == null)
            {
                return;
            }

            if (_isChase)
            {
                Chase();
            }
            else
            {
                ReturnToStartPoint();
            }
            Flip();
        }

        private void ReturnToStartPoint()
        {
            transform.position =
                Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
        }
        private void Chase()
        {
            transform.position =
                Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
        }

        private void Flip()
        {
            if (transform.position.x > _player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0,0,0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        public void isChase(bool chase) => _isChase = chase;
    }
}