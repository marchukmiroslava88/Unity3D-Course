using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    private Rigidbody _rigidbody;
    private Vector3 _direction;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_direction == Vector3.zero)
        {
            return;
        }
        
        var targetRotation = Quaternion.RotateTowards(
            transform.rotation, 
            Quaternion.LookRotation(_direction), 
            _rotationSpeed * Time.deltaTime
        );
        
        _rigidbody.MovePosition(_rigidbody.position + _direction * (_speed * Time.deltaTime));
        _rigidbody.MoveRotation(targetRotation);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector3>();
    }
}
