using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Stats : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dmgAudio;
    private bool CanSwapGravity;
    private bool Weapon2Unlock;

    [SerializeField]
    private GameObject OSM;
    [SerializeField] private int MaxHP;
    [SerializeField] private FadeIn _fadeIn;
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private TextMeshProUGUI invertText;
    private int CurrentHP;
    private int Score;

    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnHPChanged;
    public static event Action OnGameOver;
    
    private void Start()
    {
        Score = 0;
        CurrentHP = MaxHP;
        CanSwapGravity = false;
        Weapon2Unlock = false;
        Score = PlayerPrefs.GetInt("Score");
        OnScoreChanged?.Invoke(GetScore());
    }

    public bool getWeaponUnlock()
    {
        PlayerPrefs.SetInt("Weapon2", 1);
        return Weapon2Unlock;
    }

    public void setWeaponUnlock(bool setValue)
    {
        weaponText.gameObject.SetActive(setValue);
        Weapon2Unlock = setValue;
    }
    public void IncreaseScore(int i)
    {
        Score += i;
        OnScoreChanged?.Invoke(GetScore());
        PlayerPrefs.SetInt("Score", GetScore());
    }

    public int GetScore()
    {
        return Score;
    }

    public void GiveGravitySwap()
    {
        CanSwapGravity = true;
        if (SceneManager.GetActiveScene().name != "Transition")
        {
            invertText.gameObject.SetActive(true);
        }
    }
    
    public bool getSwap()
    {
        return CanSwapGravity;
    }

    public int GetHealth()
    {
        return CurrentHP;
    }

    public IEnumerator ShowRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void DamageNaMorde(int damage)
    {
        CurrentHP -= damage;
        audioSource.clip = dmgAudio;
        audioSource.Play();
        OnHPChanged?.Invoke(CurrentHP);
        StartCoroutine(ShowRed());
        if (CurrentHP <= 0)
        {
            PlayerPrefs.SetInt("Weapon2", 0);
            OnHPChanged?.Invoke(0);
            OnGameOver?.Invoke();
            
            //_fadeIn.StartAction();
            Destroy(gameObject);
            OSM.GetComponent<OneSceneManager>().resetLevel();
            PlayerPrefs.SetInt("Score", 0);
            SceneManager.LoadScene("DeathScreen");
            //GameOver

        }

        //Debug.Log(CurrentHP);
    }

    public void Heal()
    {
        CurrentHP += 1;
        OnHPChanged?.Invoke(CurrentHP);
    }
}
