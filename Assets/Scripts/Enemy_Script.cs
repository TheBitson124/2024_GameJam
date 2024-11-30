using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    [SerializeField] private float MaxHP;
    private float CurrentHP;
    [SerializeField] private float Damage;

    private void Start()
    {
        CurrentHP = MaxHP;
    }

    public void DamageNaMorde(float dmg)
    {
        CurrentHP -= dmg;
        if (CurrentHP <=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player_Stats>().DamageNaMorde(Damage);
        }
    }
}
