using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUnlock : MonoBehaviour
{
    [SerializeField]
    private Player_Stats playerStats;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player_Stats>().setWeaponUnlock(true);
            Destroy(gameObject);
        }
    }
}
