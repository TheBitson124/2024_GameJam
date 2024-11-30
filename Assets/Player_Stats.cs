using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [SerializeField] private float MaxHP;
    private float CurrentHP;
    private float Score;

    private void Start()
    {
        CurrentHP = MaxHP;
    }

    public void IncreaseScore(int i)
    {
        Score += i;
    }
}
