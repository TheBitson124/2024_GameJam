using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Script : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private int speed;
    private void Awake()
    {
        _rb.velocity = new Vector2(speed, _rb.velocity.y);
        StartCoroutine(ProjectileDestroy());
    }
    
    private IEnumerator ProjectileDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
