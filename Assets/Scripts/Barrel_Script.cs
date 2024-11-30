using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Script : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    public AudioClip explosion;
    [SerializeField] private ParticleSystem ps;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground"))
        {
            audioSource.PlayOneShot(explosion, 0.7F);
            
            ps.Play();

            StartCoroutine(Cooldown());
        }
    }
    
    
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
