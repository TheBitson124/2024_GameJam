using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    
    private AudioSource audioSource;
    private float horizontal;
    private float speed = 7f;
    private bool isFacingRight = true;
    private bool gravitySwapUnlocked = false;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheckDown;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Player_Stats playerStats;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerStats.getSwap())
        {
            unlockGravitySwap();
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded() && gravitySwapUnlocked)
        {

            FlipY();
            if (_rb.gravityScale > 0)
            {
                _rb.gravityScale = -4;
            }
            else
            {
                _rb.gravityScale = 4;
            }
        }
        FlipX();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(horizontal * speed, _rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheckDown.position, 0.2f, _groundLayer);
    }

    private void unlockGravitySwap()
    {
        gravitySwapUnlocked = true;
    }
    private void FlipX()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    
    private void FlipY()
    {
        Vector3 localScale = transform.localScale;
        localScale.y *= -1f;
        transform.localScale = localScale;
    }
}
