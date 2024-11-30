using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRadiusScript : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool playerInRadius;
    private Collider2D plr;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRadius = true;
            plr = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRadius = false;
            plr = null;
        }
    }

    public void DealDamage()
    {
        if (playerInRadius)
        {
            plr.gameObject.GetComponent<Player_Stats>().DamageNaMorde(damage);
        }
        else
        {
            Debug.Log("Player not in radius");
        }
        
    }
    
}
