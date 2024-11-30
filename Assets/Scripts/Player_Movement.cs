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

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheckDown;
    [SerializeField] private LayerMask _groundLayer;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded())
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

    private void FlipX()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    
    private void FlipY()
    {
        Vector3 localScale = transform.localScale;
        localScale.y *= -1f;
        transform.localScale = localScale;
    }
}
