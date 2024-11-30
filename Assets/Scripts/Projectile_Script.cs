using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Script : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float speed;
    private void Awake()
    {
        _rb.velocity = new Vector2(transform.right.x * speed, _rb.velocity.y);
        StartCoroutine(ProjectileDestroy());
    }
    
    private IEnumerator ProjectileDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
