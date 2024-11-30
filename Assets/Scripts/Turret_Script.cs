using System.Collections;
using UnityEngine;

public class Turret_Script : Enemy_Script
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private float Cooldown;
    
    private bool isShooting = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isShooting)
        {
            StartCoroutine(ShootingCoroutine());
        }
    }

    private IEnumerator ShootingCoroutine()
    {
        isShooting = true;
        while (true)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(Cooldown);
        }
    }

    private void SpawnProjectile()
    {
        GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            isShooting = false;
        }
    }
}