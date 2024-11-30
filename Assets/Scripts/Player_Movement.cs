using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip changeGravity;
    //public AudioClip hurt;
    private float horizontal;
    private int speed = 10;
    private bool isFacingRight = true;
    private bool gravitySwapUnlocked = false;
    private Animator _animator;

    [SerializeField] private bool enableGravity;
    [SerializeField] private bool defaultGravity;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheckDown;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Player_Stats playerStats;
    [SerializeField] private Transform firepointTransform;
    


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        if (defaultGravity)
        {
            _rb.gravityScale = -4;
        }
    }

    void Update()
    {
        if (playerStats.getSwap() || enableGravity)
        {
            unlockGravitySwap();
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        _animator.SetInteger("speed", Math.Abs((int)horizontal));
        if (Input.GetButtonDown("Jump") && IsGrounded() && gravitySwapUnlocked)
        {

            FlipY();
            audioSource.PlayOneShot(changeGravity, 0.5F);
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
        _rb.velocity = new Vector2(horizontal * speed,Mathf.Clamp(_rb.velocity.y,-100,100));
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
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            isFacingRight = !isFacingRight;
            firepointTransform.Rotate(0f, 180f, 0f);
        }
    }
    
    private void FlipY()
    {
        Vector3 localScale = transform.localScale;
        localScale.y *= -1f;
        transform.localScale = localScale;
    }
}
