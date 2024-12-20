using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    [SerializeField] private bool InstaWeaponUnlock;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject Bullet1Prefab;
    [SerializeField] private GameObject Bullet2Prefab;
    [SerializeField] private float Bullet1CD;
    [SerializeField] private float Bullet2CD;
    [SerializeField] private Player_Stats playerStats;
    [SerializeField] private GameObject gun1;
    [SerializeField] private GameObject gun2;
    private AudioSource audioSource;
    public AudioClip shoot1;
    public AudioClip shoot2;
    
    private bool isOnCD = false;
    private bool isWeapon1 = true;
    
    void Start()
    {
        gun2.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun2.SetActive(false);
            isWeapon1 = true;
            gun1.SetActive(true);
        } 
        if (Input.GetKeyDown(KeyCode.Alpha2) && (PlayerPrefs.GetInt("Weapon2") == 1 || InstaWeaponUnlock))
        {
            gun1.SetActive(false);
            isWeapon1 = false;
            gun2.SetActive(true);
        } 
        
        
        if (Input.GetButton("Fire1") && !isOnCD)
        {
            Shoot();
            if (isWeapon1)
            {
                StartCoroutine(Cooldown(Bullet1CD));
            }
            else
            {
                StartCoroutine(Cooldown(Bullet2CD));
            }
        }
    }
    
    private void Shoot()
    {
        if (isWeapon1)
        {
            audioSource.PlayOneShot(shoot1, 0.7F);
            Instantiate(Bullet1Prefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            audioSource.PlayOneShot(shoot2, 0.3F);
            Instantiate(Bullet2Prefab, firePoint.position, firePoint.rotation);
        }
    }
    
    private IEnumerator Cooldown(float secs)
    {
        isOnCD = true;
        yield return new WaitForSeconds(secs);
        isOnCD = false;
    }
}
