using UnityEngine;

public class hurtscript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Player_Stats playerStats = other.GetComponent<Player_Stats>();
            
            playerStats.DamageNaMorde(3);
        }
    }
}
