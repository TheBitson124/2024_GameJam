using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float horizontal;
    private float speed = 7f;
    private bool isFacingRight = true;
    private bool isGravityReversed = false;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheckUp;
    [SerializeField] private Transform _groundCheckDown;
    [SerializeField] private LayerMask _groundLayer;
    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            if (_rb.gravityScale > 0)
            {
                isGravityReversed = true;
                _rb.gravityScale = -4;
            }
            else
            {
                isGravityReversed = false;
                _rb.gravityScale = 4;
            }
        }
        Flip();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(horizontal * speed, _rb.velocity.y);
    }

    private bool IsGrounded()
    {
        if (isGravityReversed)
        {
            return Physics2D.OverlapCircle(_groundCheckUp.position, 0.2f, _groundLayer);
        }
        else
        {
            return Physics2D.OverlapCircle(_groundCheckDown.position, 0.2f, _groundLayer);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
