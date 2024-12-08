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
            PlayerPrefs.SetInt("Weapon2", 1);
            other.GetComponent<Player_Stats>().setWeaponUnlock(true);
            Destroy(gameObject);
        }
    }
}
