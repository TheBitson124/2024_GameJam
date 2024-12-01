using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Kot_Kauton : Enemy_Script
{
    
    enum State
    {
        MovingTowardsPlayer,
        CircularAttack,
        SweepShoot,
        SpinToWin
    }

    [SerializeField] private List<Vector2> SpinBoundaries = new List<Vector2>() { new(0f, 0f), new(0f, 0f) };
    [SerializeField] private GameObject Projectile;
    
    [SerializeField] private float DefaultSpeed;
    private float Speed;
    [SerializeField] private float SpinSpeed;
    [SerializeField] private GameObject Player;
    [SerializeField] private float SpinDuration;
    [SerializeField] private float ActionInterval = 2f;
    
    private float actionTimer = 0f;
    private SpriteRenderer Renderer;
    private float Counter = 0;
    private Animator _animator;
    private float horizontal;
    private Random random;
    private bool canAttack = true;
    private bool isAttacking = false;
    private bool isCoroutineRunning = false;
    private State currentState = State.MovingTowardsPlayer;
    private bool isFacingRight = true;
    private float moveDirection = 1f;


    private void Start()
    {
        random = new Random();
        Speed = DefaultSpeed;
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {

        actionTimer -= Time.deltaTime;

        if (actionTimer <= 0f)
        {
            Speed = DefaultSpeed;
            _animator.SetTrigger("StopSpin");
            actionTimer = ActionInterval;
            currentState = RollAttack(); 
            Debug.Log(currentState);
        }
        switch (currentState)
        {
            case State.MovingTowardsPlayer:
                MoveTowardsPlayer();
                break;
            case State.CircularAttack:
                CircularAttack();
                break;
            case State.SweepShoot:
                SweepShoot();
                break;
            case State.SpinToWin:
                SpinToWin();
                break;
        }
    }
    
    private void MoveTowardsPlayer()
    {
        if (Player == null) return;

        float step = Speed * Time.deltaTime;
        Vector2 targetPosition = Player.transform.position;
        Vector2 currentPosition = transform.position;

        float direction = targetPosition.x - currentPosition.x;
        
        FlipSprite(direction);

        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, step);
    }

    private void CircularAttack()
    {
        Debug.Log("Circular Attack");

        int numberOfProjectiles = 12;
        float angleStep = 360f / numberOfProjectiles;
        float currentAngle = 0f;

        Vector3 spawnPosition = transform.position;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float projectileDirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            Vector2 projectileDirection = new Vector2(projectileDirX, projectileDirY).normalized;

            GameObject newProjectile = Instantiate(Projectile, spawnPosition, Quaternion.identity);

            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float projectileSpeed = 5f;
                rb.velocity = projectileDirection * projectileSpeed;
            }

            currentAngle += angleStep;
        }
        actionTimer = 0f;
    }

    private void SweepShoot()
    {
        Debug.Log("SweepShoot");

        int numberOfProjectiles = 3; 
        float spreadAngle = 30f; 
        float startAngle = -spreadAngle / 2f; 
        Vector3 spawnPosition = transform.position; 

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = startAngle + (spreadAngle / (numberOfProjectiles - 1)) * i;
            
            float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 projectileDirection = new Vector2(projectileDirX, projectileDirY).normalized;
            
            GameObject newProjectile = Instantiate(Projectile, spawnPosition, Quaternion.identity);
            
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float projectileSpeed = 5f; 
                rb.velocity = projectileDirection * projectileSpeed;
            }
        }
        actionTimer = 0f;
    }

    
    private void SpinToWin()
    {
        _animator.SetTrigger("Spin");
        Debug.Log("SpinToWin");
        Speed = 20;
        Vector2 velocity = new Vector2(moveDirection * Speed, 0f);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = velocity;
        }
    }
    
    
    private State RollAttack()
    {
        double randomValue = random.NextDouble();
        if (randomValue > 0.8f) return State.CircularAttack;
        else if (randomValue > 0.7f) return State.SweepShoot;
        else if (randomValue > 0.6f) return State.SpinToWin;
        return State.MovingTowardsPlayer;
    }
    
   

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")){return;}
        if (Counter <= 0f)
        {
            other.gameObject.GetComponent<Player_Stats>().DamageNaMorde(Damage);
            Counter = 1;
        }else Counter -= Time.deltaTime;
        moveDirection *= -1f;
    }
    
    private void FlipSprite(float direction)
    {
        if ((direction < 0 && isFacingRight) || (direction > 0 && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}