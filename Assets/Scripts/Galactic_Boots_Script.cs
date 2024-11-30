using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Galactic_Boots_Script : MonoBehaviour
{
    [SerializeField]
    private Player_Stats playerStats;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player_Stats>().GiveGravitySwap();
            playerStats.GiveGravitySwap();
            Destroy(gameObject);
        }
    }
}
