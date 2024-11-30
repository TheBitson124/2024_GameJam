using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Script : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip explosion;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private ExplosionRadiusScript ers;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground"))
        {
            audioSource.PlayOneShot(explosion, 0.7F);
            
            ps.Play();
            ers.DealDamage();
            StartCoroutine(Cooldown());
        }
    }
    
    
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
