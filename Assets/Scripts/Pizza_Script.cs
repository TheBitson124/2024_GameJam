using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player_Stats>().IncreaseScore(30);
            Destroy(gameObject);
        }
    }
}
