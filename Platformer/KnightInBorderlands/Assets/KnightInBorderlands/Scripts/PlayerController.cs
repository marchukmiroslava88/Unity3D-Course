using System;
using System.Collections;
using DragonBones;
using KnightInBorderlands.Scripts.Components;
using KnightInBorderlands.Scripts.LevelManager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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
        [SerializeField] private float _wallJumpTime;
        [SerializeField] private float _wallJumpTimeCounter;
        [SerializeField] private float _slidingSpeed;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _checkPointLayer;
        [SerializeField] private LayerMask _wallLayer; 
        [SerializeField] private UnityArmatureComponent _armature;
        [SerializeField] private PlayerInput _playerInputActions;
        [SerializeField] private SpawnEvent _spawnEvent;
        
        private Vector2 _direction;
        private bool _isJump;
        private bool _isGrounded = true;
        private bool _isMoving;
        private bool _isHit;
        private bool _isCheckpoint;
        private bool _isWallSlide;
        private bool _isWallJump;
        private bool _isHurt;
        private float _wallJumpingDirection;
        private InputAction _inputActionHit;
        
        private void Start()
        {
            _inputActionHit = _playerInputActions.actions.FindAction("Hit");
            _armature.animation.Play("idle a", 1);
        }

        private void FixedUpdate()
        {
            _isGrounded = IsGrounded();
            _isWallSlide = IsWallSlide();

            if (_isJump)
            {
                Jump();
            }
            
            if (_isWallJump)
            {
                WallJump();
            }
            
            if (_isWallSlide)
            {
                WallSliding();
            }
            
            if (!_isWallJump)
            {
                Move();
            }
        }
        
        public void TakeDamage()
        {
            _playerInputActions.actions.Disable();
            var direction = _armature.armature.flipX ? 1f : -1f;
            _rigidbody2D.AddForce(new Vector2(direction * 22f, 22f), ForceMode2D.Impulse);
            _armature.animation.Play("hurt a", 1);
            StartCoroutine(Wait(0.5f, () => { }));
        }

        public void Die()
        {
            _playerInputActions.actions.Disable();
            _armature.animation.Play("die a", 1);
            StartCoroutine(Wait(0.5f, () =>
            {
                _spawnEvent.Invoke(gameObject);
                HealthComponent.Instance.RestoreHealth();
                _armature.animation.Play("idle a", 1);
            }));
        }
        
        public void onMove(InputAction.CallbackContext context)
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

            if (context.started || context.performed && !_isGrounded)
            {
                _isMoving = true;
                _armature.animation.Play("jump b");
            }
        
            if (context.canceled)
            { 
                _isMoving = false;
                StartCoroutine(MoveFinallyAnimation());
            }
        }

        public void onJump(InputAction.CallbackContext context)
        {
            if (context.started && _isWallSlide) 
            {
                _wallJumpTimeCounter = _wallJumpTime;
                _isWallJump = true;
                _armature.animation.Play("jump a");
            }
             
            if (context.started && _isGrounded && !_isWallSlide)
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

        public void onHit(InputAction.CallbackContext context)
        {
            if (context.started && !_isWallSlide)
            {
                _isHit = true;
                _armature.animation.Play(_isGrounded ? "attack a" : "attack c", 1);
                _inputActionHit.Disable();
                StartCoroutine(EnableAttack()); 
            }
        }
        
        public void onInteract(InputAction.CallbackContext context)
        {
            if (context.started && _isCheckpoint && _isGrounded)
            {
                TooltipManager.Instance.HideToolTip();
                CheckPoint.Instance.Check(transform.position);
                _armature.animation.Play("sit", 1);
            }
        }
        
        private bool IsGrounded()
        {
            var bounds = _capsuleCollider2d.bounds;
            var raycastHit2d = Physics2D.CapsuleCast(
                bounds.center, 
                bounds.size, _capsuleCollider2d.direction, 0.4f, 
                Vector2.down * .1f, 0.2f, _groundLayer);
            return raycastHit2d.collider != null;
        }
        
        private bool IsWallSlide()
        {
            return Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0,_wallLayer);
        }

        private void Move()
        {
            _rigidbody2D.velocity = new Vector2(_direction.x * _speedMovement, _rigidbody2D.velocity.y);
        } 
        
        private void Jump()
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
        
        private void WallSliding()
        {
            if (_isWallSlide && !_isGrounded && !_isWallJump)
            {
                _rigidbody2D.velocity = new Vector2(
                    _direction.x, 
                    Math.Clamp( _rigidbody2D.velocity.y, -_slidingSpeed, float.MaxValue)
                );
            }
        }
        
        private void WallJump()
        {
            if (_wallJumpTimeCounter > 0)
            {
                _wallJumpTimeCounter -= Time.deltaTime;
                _wallJumpingDirection = _armature.armature.flipX ? 1f : -1f;
                _rigidbody2D.AddForce(new Vector2(_wallJumpingDirection * 1.25f, 2.15f), ForceMode2D.Impulse);
            }
            else
            {
                _wallJumpTimeCounter = 0f;
                _isWallJump = false;
            }
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
        
        private IEnumerator Wait(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            _playerInputActions.actions.Enable();
            callback?.Invoke();
        }

        public void SetCheckPoint(bool isCheckpoint)
        {
            _isCheckpoint = isCheckpoint;
        }
    }
   
   [Serializable]
   public class SpawnEvent : UnityEvent<GameObject>
   {
   }
}


