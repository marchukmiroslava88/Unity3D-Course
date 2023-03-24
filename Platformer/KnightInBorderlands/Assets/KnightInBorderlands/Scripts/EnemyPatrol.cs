using System;
using System.Collections;
using UnityEngine;

namespace KnightInBorderlands.Scripts
{
    public class EnemyPatrol : MonoBehaviour
    {
        public GameObject PointA;
        public GameObject PointB;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        private Transform _currentPoint;
        public float Speed;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsDead = Animator.StringToHash("isDead");
        
        private void Start()
        {
            _currentPoint = PointB.transform;
            _animator.SetBool(IsWalking, true);
        }

        private void Update()
        {
            if (!_animator.GetBool(IsDead))
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("hit")) return;
                if (_currentPoint == PointB.transform)
                {
                    _rigidbody.velocity = new Vector2(Speed, 0);
                }
            
                if (_currentPoint == PointA.transform)
                {
                    _rigidbody.velocity = new Vector2(-Speed, 0);
                }
                if (Vector2.Distance(transform.position, _currentPoint.position) < 1f && _currentPoint == PointB.transform)
                {
                    _currentPoint = PointA.transform;
                    flip();
                }
                if (Vector2.Distance(transform.position, _currentPoint.position) < 1f && _currentPoint == PointA.transform)
                {
                    _currentPoint = PointB.transform;
                    flip();
                }
            }  
            else
            {
                StartCoroutine(Destroy());
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
            var position = PointA.transform.position;
            var position1 = PointB.transform.position;
            Gizmos.DrawWireSphere(position, 0.5f);
            Gizmos.DrawWireSphere(position1, 0.5f);
            Gizmos.DrawLine(position, position1);
        }
        
        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}