using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Animator _animator;
    
    private const float GroundHeight = 0;
    private const float MaxHoldJumpTime = 0.2f;

    public Vector2 velocity;
    public float distance;
    
    private Vector2 _position;

    [SerializeField] private float gravity = 9.81f * 2;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float jumpHeightThreshold = 3;
    private float _holdJumpTimer;
    private bool _isHoldingJump;
    private bool _isGrounded;
    private static readonly int IsJumping = Animator.StringToHash("isJumping");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _position = transform.position;

        if (!_isGrounded)
        {
            _position.y += velocity.y * Time.deltaTime; //Jump :)

            if (_isHoldingJump) //Limits hold jump time
            {
                _holdJumpTimer += Time.deltaTime;
                _isHoldingJump = !(_holdJumpTimer >= MaxHoldJumpTime);
            }
            
            if (!_isHoldingJump) //Makes gravity affect height if not holding jump
            {
                velocity.y += gravity * Time.deltaTime;
            }
            
            if (_position.y <= GroundHeight) //Checks if player touched ground
            {
                _position.y = GroundHeight;
                _isGrounded = true;
                _animator.SetBool(IsJumping, false);
            }
        }

        transform.position = _position;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded || _position.y <= jumpHeightThreshold && context.started)
        {
            _isGrounded = false;
            _animator.SetBool(IsJumping, true);
            _isHoldingJump = true;
            _holdJumpTimer = 0;
            velocity.y = jumpForce;
        } else if (context.canceled)
        {
            _isHoldingJump = false;
        }
    }
    
}
