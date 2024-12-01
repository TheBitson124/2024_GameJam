using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ourplegay : Enemy_Script
{
    [SerializeField] private float Speed;
    [SerializeField] private GameObject Bullet;
    private SpriteRenderer Renderer;
    private float Counter = 0;
    private Animator _animator;
    private float horizontal;
    private bool facingLeft = true;
    private bool canShoot = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
    }
    

    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) { return; }
        
        var multi = other.gameObject.transform.position.x > transform.position.x ? 1 : -1;
        transform.position += new Vector3(multi * Speed * Time.deltaTime, 0, 0);
        Renderer.flipX = multi > 0;

        if (canShoot)
        {
            _animator.SetTrigger("attack");
            canShoot = false;
            if (multi > 0)
            {
                Shoot(0);
            }
            else
            {
                Shoot(1);
            }

            StartCoroutine(ShootCooldown());
        }
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(2f);
        canShoot = true;
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
    
    private void Shoot(int dir)
    {
        
        switch (dir)
        {
            case 0:
                Instantiate(Bullet, new Vector3(transform.position.x+1,transform.position.y,transform.position.z), transform.rotation);
                break;
            case 1:
                Instantiate(Bullet, new Vector3(transform.position.x-1,transform.position.y,transform.position.z), Quaternion.Euler(0, 0, 180));
                break;
        }

    }
}
