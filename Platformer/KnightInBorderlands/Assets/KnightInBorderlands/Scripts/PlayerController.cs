using System;
using System.Collections;
using DragonBones;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KnightInBorderlands.Scripts
{
   public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private CapsuleCollider2D _capsuleCollider2d;
        [SerializeField] private float _speedMovement;
        [SerializeField] private float _jumpDistance;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _jumpTimeCounter;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private UnityArmatureComponent _armature;
        [SerializeField] private PlayerInput _playerInputActions;
        
        private Vector2 _direction;
        private bool _isJump;
        private bool _isGrounded = true;
        private bool _isMoving;
        private bool _isHit;
        private InputAction _inputActionHit;
        
        private void Start()
        {    
            _inputActionHit = _playerInputActions.actions.FindAction("Hit");
            _armature.animation.Play("sit");
        }

        private void FixedUpdate()
        { 
            _isGrounded = IsGrounded();

            if (_isJump)
            {
                if (_jumpTimeCounter > 0)
                {
                    _rigidbody2D.velocity = new Vector2(_direction.x, _jumpDistance);
                    _jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    _isJump = false;
                }
            }
         
            _rigidbody2D.velocity = new Vector2(_direction.x * _speedMovement, _rigidbody2D.velocity.y);
        }

        public void Movement(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();

            _armature.armature.flipX = _direction.x switch
            {
                > 0 => false,
                < 0 => true,
                _ => _armature.armature.flipX
            };
            
            if (context.performed && _isGrounded)
            {
                _isMoving = true;
                _armature.animation.Play("run");
            }
        
            if (context.started && !_isGrounded)
            {
                _isMoving = true;
            }
        
            if (context.canceled)
            { 
                _isMoving = false;
                StartCoroutine(MoveFinallyAnimation());
            }
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.started && _isGrounded)
            {
                _jumpTimeCounter = _jumpTime;
                _isJump = true;
                _armature.animation.Play("jump a");
            }

            if (context.canceled || context.performed)
            {
                _isJump = false;
                if (!_isHit)
                {
                    _armature.animation.Play("jump b");
                    StartCoroutine(JumpFinallyAnimation());  
                }
            }
        }

        public void Hit(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _isHit = true;
                _armature.animation.Play(_isGrounded ? "attack a" : "attack c", 1);
                _inputActionHit.Disable();
                StartCoroutine(EnableAttack()); 
            }
        }
        
        private bool IsGrounded()
        {
            var bounds = _capsuleCollider2d.bounds;
            var raycastHit2d = Physics2D.CapsuleCast(
                bounds.center, 
                bounds.size, _capsuleCollider2d.direction, 0.4f, 
                Vector2.down * .1f, 0.2f, _layer);
            return raycastHit2d.collider != null;
        }
        
        private IEnumerator MoveFinallyAnimation()
        {
            while (!_isGrounded && !_isJump)
            {
                yield return null;
            }
        
            _armature.animation.Play("idle a");
        }

        private IEnumerator JumpFinallyAnimation()
        {
            while (!_isGrounded && !_isJump)
            {
                yield return null;
            }
            
            _armature.animation.Play("jump c",1);
            _armature.animation.Play(_isMoving ? "run" : "idle a");
        }
        
        private IEnumerator EnableAttack()
        {
            yield return new WaitForSeconds(0.5f);
            _isHit = false;
            _inputActionHit.Enable();
            _armature.animation.Play(_isMoving ? "run" : "idle a");  
        }
    }
}


