using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Turret_Projectile : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float speed;
    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        _rb.velocity = direction * speed;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player_Stats>().DamageNaMorde(Damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}