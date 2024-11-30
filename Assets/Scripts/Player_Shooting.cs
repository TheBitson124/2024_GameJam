using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject Bullet1Prefab;
    [SerializeField] private float Bullet1CD;

    private bool isOnCD = false;
    
    void Update()
    {
        while (Input.GetButtonDown("Fire1"))
        {
            if (!isOnCD)
            {
                Shoot();
                StartCoroutine(Cooldown(Bullet1CD));
            }
        }
    }
    
    private void Shoot()
    {
        Instantiate(Bullet1Prefab, firePoint.position, firePoint.rotation);
    }
    
    
    private IEnumerator Cooldown(float secs)
    {
        isOnCD = true;
        yield return new WaitForSeconds(secs);
        isOnCD = false;
    }
}
