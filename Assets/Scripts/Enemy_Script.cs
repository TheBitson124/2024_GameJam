using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    [SerializeField] private int MaxHP;
    protected float CurrentHP;

    [SerializeField] private int Score;
    [SerializeField] protected int Damage;
    

    private void Start()
    {
        CurrentHP = MaxHP;
    }
    public IEnumerator ShowRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void DamageNaMorde(float dmg)
    {
        CurrentHP -= dmg;
        StartCoroutine(ShowRed());
        if (CurrentHP <=0)
        {
            Destroy(gameObject);
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<Player_Stats>().IncreaseScore(Score);
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
