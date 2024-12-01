using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;
    private AudioSource audioSource;
    public AudioClip gameover;

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canvasGroup.alpha = 0f;
    }

    public void StartAction()
    {
        StartCoroutine(StartFadeIn());
    }

    private IEnumerator StartFadeIn()
    {
        fadeDuration = gameover.length;
        float elapsedTime = 0f;
        audioSource.PlayOneShot(gameover, 0.5F);
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        
        canvasGroup.alpha = 1f;
        canvasGroup.alpha = 0f;
    }
}
