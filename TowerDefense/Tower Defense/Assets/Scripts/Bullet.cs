using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private LayerMask _layerEnemy;
    
    private void Update()
    {
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _layerEnemy) != 0)
        {
            Destroy(gameObject);
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);
        }
    }
}
