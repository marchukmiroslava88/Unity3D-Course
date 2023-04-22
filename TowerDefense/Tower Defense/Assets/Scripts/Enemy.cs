using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layerObstacle;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _health;
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _attackRate = 2;
    private bool isObstacle;
    private static readonly int IsHit = Animator.StringToHash("isHit");
    private static readonly int IsAttack = Animator.StringToHash("isAttack");
    private const float hitAnimationTime = 0.2f;
    private const float dieAnimationTime = 0.6f;
 
    private void Update()
    {
        if (!isObstacle)
        {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        
        if (_health > 0)
        {
            _animator.SetBool(IsHit, true);
            StartCoroutine(Hit()); 
        }
        else
        {
            StartCoroutine(Die());
        }
    } 

    private IEnumerator Hit()
    {
        _animator.Play("GetHit");
        yield return new WaitForSeconds(hitAnimationTime);
        _animator.SetBool(IsHit, false);
    }
    
    private IEnumerator Die()
    {
        _animator.Play("Die");
        yield return new WaitForSeconds(dieAnimationTime);
        Destroy(gameObject);
    }
    
    private IEnumerator Attack(Tower.TowerHealth tower)
    {
        while (tower.HealthPoint > 0)
        {
            tower.TakeDamage(_damage);
            yield return new WaitForSeconds(_attackRate);
        }
        
        _animator.SetBool(IsAttack, false);
        isObstacle = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _layerObstacle) != 0)
        { 
            isObstacle = true;
            _animator.SetBool(IsAttack, true);
            _animator.Play("Attack");

            if (other.gameObject.TryGetComponent<Tower.TowerHealth>(out var tower))
            {
                StartCoroutine(Attack(tower));
            }
        }
    }
}
