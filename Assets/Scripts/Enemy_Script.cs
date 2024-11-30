using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    [SerializeField] private int MaxHP;
    private float CurrentHP;

    [SerializeField] private int Score;
    [SerializeField] protected int Damage;


    private void Start()
    {
        CurrentHP = MaxHP;
    }

    public void DamageNaMorde(float dmg)
    {
        CurrentHP -= dmg;
        if (CurrentHP <=0)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.gameObject.GetComponent<Player_Stats>().IncreaseScore(Score);
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
