using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    private bool CanSwapGravity;
    [SerializeField] private float MaxHP;
    private float CurrentHP;
    private float Score;

    private void Start()
    {
        CurrentHP = MaxHP;
        CanSwapGravity = false;
    }

    public void IncreaseScore(float i)
    {
        Score += i;
    }

    public float GetScore()
    {
        return Score;
    }

    public void GiveGravitySwap()
    {
        CanSwapGravity = true;
    }

    public void DamageNaMorde(float damage)
    {
        CurrentHP -= damage;
        
        if (CurrentHP <= 0)
        {
            Destroy(gameObject);
            //GameOver
        }
    }
}
