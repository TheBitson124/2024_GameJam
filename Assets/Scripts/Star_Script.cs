using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player_Stats>().IncreaseScore(10);
            Destroy(gameObject);
        }
    }
}
