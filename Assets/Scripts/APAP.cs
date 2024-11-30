using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class APAP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Player_Stats>().GetHealth() < 3)
            {
                other.gameObject.GetComponent<Player_Stats>().Heal();
                Destroy(gameObject);
            }
        }
    }
}
