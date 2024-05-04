using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 velocity;

    public float distance = 0;
    
    [Header("Movement")]
    [SerializeField] private float acceleration = 10;
    [SerializeField] private float maxAcceleration = 10;
    [SerializeField] private float maxVelocity = 100;
    
    [Header("Jump")]
    [SerializeField] private float gravity;
    [SerializeField] private float jumpPower = 20;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float groundHeight = 0;
    [SerializeField] private bool isHoldingJump = false;
    [SerializeField] private float maxHoldJumpTime = 0.2f;
    [SerializeField] private float maxMaxHoldJumpTime = 0.2f;
    [SerializeField] private float holdJumpTimer = 0.0f;
    [SerializeField] private float jumpGroundThreshold = 5;

    private void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        
        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpPower;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }
            
            pos.y += velocity.y * Time.fixedDeltaTime;
            
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
            velocity.y = 0;
            
            float velocityRatio = velocity.x / maxVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);
            maxHoldJumpTime = maxMaxHoldJumpTime * velocityRatio;
            
            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x >= maxVelocity)
            {
                velocity.x = maxVelocity;
            }
        }

        transform.position = pos;
    }
}
