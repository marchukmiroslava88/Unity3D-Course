using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layerObstacle;
    [SerializeField] private Animator _animator;
    private bool isObstacle;
        
    private void Start()
    {
        _animator.Play("Idle");
    }

    private void Update()
    {
        if (!isObstacle) 
        {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
            _animator.Play("Walk");
        }
        else
        {
            _animator.Play("Attack"); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _layerObstacle) != 0)
        {
            isObstacle = true;
        }
    }
}
