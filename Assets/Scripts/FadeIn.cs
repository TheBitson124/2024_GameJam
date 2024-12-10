using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;
    private AudioSource audioSource;
    public AudioClip gameover;

    public void StartAction()
    {
        StartCoroutine(StartFadeIn());
    }

    private IEnumerator StartFadeIn()
    {
        audioSource = GetComponent<AudioSource>();
        float elapsedTime = 0f;
        audioSource.PlayOneShot(gameover, 0.7F);
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}
