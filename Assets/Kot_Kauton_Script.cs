using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
    
    [SerializeField] private float Speed;
    [SerializeField] private float SpinSpeed;
    [SerializeField] private GameObject Player;
    [SerializeField] private float SpinDuration;
    
    private SpriteRenderer Renderer;
    private float Counter = 0;
    private Animator _animator;
    private float horizontal;
    private Random random;


    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
        random = new Random();
        
    }

    private void Update()
    {
        if (CurrentHP > 0)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        State atk = State.SpinToWin;

        switch (atk)
        {
            case State.CircularAttack:
                yield return StartCoroutine(CircularAttackCoroutine());
                break;
            case State.SweepShoot:
                yield return StartCoroutine(SweepShootCoroutine());
                break;
            case State.SpinToWin:
                yield return StartCoroutine(SpinToWinCoroutine());
                break;
            case State.MovingTowardsPlayer:
                yield return StartCoroutine(MoveTowardsPlayerCoroutine());
                break;
        }
    }

    private IEnumerator MoveTowardsPlayerCoroutine()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        _animator.SetInteger("Speed", Math.Abs((int)horizontal));

        var multi = Player.gameObject.transform.position.x > transform.position.x ? 1 : -1;
        transform.position += new Vector3(multi * Speed * Time.deltaTime, 0, 0);
        Renderer.flipX = multi < 0;
        yield return new WaitForSeconds(4);
    }

    private IEnumerator SpinToWinCoroutine()
    {
        float counter = SpinDuration;
        bool movingTowardsBoundary1 = (random.NextDouble() > 0.5);
        float boundary1 = SpinBoundaries[0].x;
        float boundary2 = SpinBoundaries[1].x;
        
        Vector3 velocity = new Vector3(movingTowardsBoundary1 ? -SpinSpeed : SpinSpeed, 0, 0); 

        while (counter >= 0)
        {
            transform.position += velocity * Time.deltaTime;
            
            if (movingTowardsBoundary1 && transform.position.x <= boundary1)
            {
                movingTowardsBoundary1 = false;
                velocity = new Vector3(SpinSpeed, 0, 0);
            }
            else if (!movingTowardsBoundary1 && transform.position.x >= boundary2)
            {
                movingTowardsBoundary1 = true;
                velocity = new Vector3(-SpinSpeed, 0, 0);
            }

            counter -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(SpinDuration);
    }



    private IEnumerator SweepShootCoroutine()
    {
        throw new NotImplementedException();
    }

    private IEnumerator CircularAttackCoroutine()
    {
        bool shot = false;
        while (!shot)
        {
            ShootProjectile(0);
            ShootProjectile(4);
            
            
            yield return new WaitForSeconds(1);
            
            

            shot = true;
        }
    }

    private void ShootProjectile(int pos)
    {
        throw new NotImplementedException();
    }

    private State RollAttack()
    {
        double randomValue = random.NextDouble();

        if (randomValue > 0.80)
        {
            return State.CircularAttack;
        }
        else if (randomValue > 0.60)
        {
            return State.SpinToWin;
        }
        else if (randomValue > 0.30)
        {
            return State.SweepShoot;
        }

        return State.MovingTowardsPlayer;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        _animator.SetInteger("Speed", Math.Abs((int)horizontal));
        if (!other.CompareTag("Player")) { return; }
        var multi = other.gameObject.transform.position.x > transform.position.x ? 1 : -1;
        transform.position += new Vector3(multi * Speed * Time.deltaTime, 0, 0);
        Renderer.flipX = multi < 0;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")){return;}
        if (Counter <= 0f)
        {
            other.gameObject.GetComponent<Player_Stats>().DamageNaMorde(Damage);
            Counter = 1;
        }else Counter -= Time.deltaTime;
    }
}