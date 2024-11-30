using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBox : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mainTheme;
    public static float vol = 1f;
    private static MusicBox instance;
    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;

        PlayMusicForCurrentScene();
        DontDestroyOnLoad(this.gameObject);
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        audioSource.volume = vol;
    }

    public static void ChangeVolume(float volume)
    {
        vol = volume;
    }
    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        PlayMusicForCurrentScene();
    }

    public void PlayMusicForCurrentScene()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            default:
                PlayMusic(mainTheme);
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        audioSource.volume = vol;
        if (audioSource.clip == clip) return;
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    
/*    public void PlayBossMusic(string bossName)
    {
        switch (bossName)
        {
            case "Jelly":
                PlayMusic(jellyTheme);
                break;
            case "Champion":
                PlayMusic(championTheme);
                break;
        }
    }
    */
}
