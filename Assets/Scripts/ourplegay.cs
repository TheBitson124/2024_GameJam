using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ourplegay : Enemy_Script
{
    [SerializeField] private float Speed;
    private SpriteRenderer Renderer;
    private float Counter = 0;
    private Animator _animator;
    private float horizontal;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (!other.CompareTag("Player")) { return; }
        var multi = other.gameObject.transform.position.x > transform.position.x ? 1 : -1;
        transform.position += new Vector3(multi * Speed * Time.deltaTime, 0, 0);
        Renderer.flipX = multi > 0;
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
