using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    private bool CanSwapGravity;
    private bool Weapon2Unlock;
    [SerializeField] private int MaxHP;
    [SerializeField] private FadeIn _fadeIn;
    private int CurrentHP;
    private int Score;

    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnHPChanged;
    public static event Action OnGameOver;
    
    private void Start()
    {
        CurrentHP = MaxHP;
        CanSwapGravity = false;
        Weapon2Unlock = false;
    }

    public bool getWeaponUnlock()
    {
        return Weapon2Unlock;
    }

    public void setWeaponUnlock(bool setValue)
    {
        Weapon2Unlock = setValue;
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

    public int GetHealth()
    {
        return CurrentHP;
    }

    public IEnumerator ShowRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void DamageNaMorde(int damage)
    {
        CurrentHP -= damage;
        OnHPChanged?.Invoke(CurrentHP);
        StartCoroutine(ShowRed());
        if (CurrentHP <= 0)
        {
            OnHPChanged?.Invoke(0);
            OnGameOver?.Invoke();
            _fadeIn.StartAction();
            Destroy(gameObject);
            //GameOver
            
        }

        //Debug.Log(CurrentHP);
    }

    public void Heal()
    {
        CurrentHP += 1;
        OnHPChanged?.Invoke(CurrentHP);
    }
}
