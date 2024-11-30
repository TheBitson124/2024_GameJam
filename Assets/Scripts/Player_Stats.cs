using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    private bool CanSwapGravity;
    [SerializeField] private int MaxHP;
    private int CurrentHP;
    private int Score;

    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnHPChanged;
    public static event Action OnGameOver;
    
    private void Start()
    {
        CurrentHP = MaxHP;
        CanSwapGravity = false;
    }

    public void IncreaseScore(int i)
    {
        Score += i;
        OnScoreChanged?.Invoke(GetScore());
    }

    public int GetScore()
    {
        return Score;
    }

    public void GiveGravitySwap()
    {
        CanSwapGravity = true;
    }
    
    public bool getSwap()
    {
        return CanSwapGravity;
    }

    public void DamageNaMorde(int damage)
    {
        CurrentHP -= damage;
        OnHPChanged?.Invoke(CurrentHP);
        if (CurrentHP <= 0)
        {
            OnGameOver?.Invoke();
            Destroy(gameObject);
            //GameOver

        }
    }
}
