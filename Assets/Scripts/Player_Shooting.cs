using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject Bullet1Prefab;
    [SerializeField] private float Bullet1CD;
    private AudioSource audioSource;
    public AudioClip shoot;
    
    private bool isOnCD = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Input.GetButton("Fire1") && !isOnCD)
        {
            Shoot();
            StartCoroutine(Cooldown(Bullet1CD));
        }
    }
    
    private void Shoot()
    {
        audioSource.PlayOneShot(shoot, 0.7F);
        Instantiate(Bullet1Prefab, firePoint.position, firePoint.rotation);
    }
    
    
    private IEnumerator Cooldown(float secs)
    {
        isOnCD = true;
        yield return new WaitForSeconds(secs);
        isOnCD = false;
    }
}
