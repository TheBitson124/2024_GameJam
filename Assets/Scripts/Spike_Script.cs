using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Script : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player_Stats>().DamageNaMorde(3);
        }
    }
}
