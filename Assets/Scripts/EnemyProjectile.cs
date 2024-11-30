using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float speed;
    

    private Transform playerTransform;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            _rb.velocity = direction * speed;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player_Stats>().DamageNaMorde(Damage);
            }
            Destroy(gameObject);
        }
    }
    
}
