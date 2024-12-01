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
    private bool canAttack = true;
    private bool isAttacking = false;


    
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
        while (CurrentHP > 0)
        {
            if (isAttacking)
            {
                yield return null;
                continue;
            }

            State atk = RollAttack();

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
            
            //yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator WalkSlowly()
    {
        float walkDuration = 1f;
        float timed = 0f;
        while (timed < walkDuration)
        {
            if (isAttacking)
            {
                break;
            }
            var multi = Player.gameObject.transform.position.x > transform.position.x ? 1 : -1;
            transform.position += new Vector3(multi * Speed * Time.deltaTime, 0, 0);
            Renderer.flipX = multi < 0;
            timed += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator MoveTowardsPlayerCoroutine()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        _animator.SetInteger("Speed", Math.Abs((int)horizontal));
        
        float counter = 2;
        while (counter >0)
        {
            yield return StartCoroutine(WalkSlowly());
            counter -= Time.deltaTime;
        }
        canAttack = true;
        yield return null;
    } 

    private IEnumerator SpinToWinCoroutine()
    {
        isAttacking = true;
        _animator.SetTrigger("Spin");
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
        }
        yield return new WaitForSeconds(SpinDuration);
        _animator.SetTrigger("Sweep");
        isAttacking = false;
    }



    private IEnumerator SweepShootCoroutine()
    {
        _animator.SetTrigger("Sweep");
        isAttacking = true;
        ShootProjectileSweep();
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }


    private IEnumerator CircularAttackCoroutine()
    {
        isAttacking = true; 

        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    private void ShootProjectileCircular(int pos)
    {
        //throw new NotImplementedException();
    }

    private void ShootProjectileSweep()
    {
        var multi = Player.gameObject.transform.position.x > transform.position.x ? 1 : -1;

        switch (multi)
        {
            case >0:
                SpawnProjectiles(1);
                break;
            case <0:
                SpawnProjectiles(0);
                break;
        }
    }
    
    private void SpawnProjectiles(int direction)
    {
        if (direction == 0)
        {
            GameObject bullet1 = Instantiate(Projectile, new Vector3(transform.position.x-2.34f,transform.position.y-1,transform.position.z), transform.rotation);
            GameObject bullet2 = Instantiate(Projectile, new Vector3(transform.position.x-2.09f,transform.position.y,transform.position.z), transform.rotation);
            GameObject bullet3 = Instantiate(Projectile, new Vector3(transform.position.x-1.66f,transform.position.y+1,transform.position.z), transform.rotation);
        }
        else
        {
            GameObject bullet1 = Instantiate(Projectile, new Vector3(transform.position.x-2.34f,transform.position.y-1,transform.position.z), Quaternion.Euler(0, 0, 180));
            GameObject bullet2 = Instantiate(Projectile, new Vector3(transform.position.x-2.09f,transform.position.y,transform.position.z), Quaternion.Euler(0, 0, 180));
            GameObject bullet3 = Instantiate(Projectile, new Vector3(transform.position.x-1.66f,transform.position.y+1,transform.position.z), Quaternion.Euler(0, 0, 180));
        }
    }

    private State RollAttack()
    {
        double randomValue = random.NextDouble();
        if (randomValue > 0.8f) return State.SweepShoot;
        else if (randomValue > 0.7f) return State.SpinToWin;
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
    }
}