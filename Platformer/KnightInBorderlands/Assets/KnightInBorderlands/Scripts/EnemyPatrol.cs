using System;
using UnityEngine;

namespace KnightInBorderlands.Scripts
{
    public class EnemyPatrol : MonoBehaviour
    {
        public GameObject pointA;
        public GameObject pointB;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        private Transform _currentPoint;
        public float _speed;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        private void Start()
        {
            _currentPoint = pointB.transform;
            _animator.SetBool(IsWalking, true);
        }

        private void Update()
        {
            Vector2 point = _currentPoint.position - transform.position;
            
            if (_currentPoint == pointB.transform)
            {
                _rigidbody.velocity = new Vector2(_speed, 0);
            }
            
            if (_currentPoint == pointA.transform)
            {
                _rigidbody.velocity = new Vector2(-_speed, 0);
            }

            if (Vector2.Distance(transform.position, _currentPoint.position) < 1f && _currentPoint == pointB.transform)
            {
                _currentPoint = pointA.transform;
                flip();
            }
            if (Vector2.Distance(transform.position, _currentPoint.position) < 1f && _currentPoint == pointA.transform)
            {
                _currentPoint = pointB.transform;
                flip();
            }
        }

        private void flip()
        {
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        
        private void OnDrawGizmos()
        {
            var position = pointA.transform.position;
            var position1 = pointB.transform.position;
            Gizmos.DrawWireSphere(position, 0.5f);
            Gizmos.DrawWireSphere(position1, 0.5f);
            Gizmos.DrawLine(position, position1);
        }
    }
}