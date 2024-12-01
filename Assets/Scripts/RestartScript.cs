using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour {
    private bool _isDead = false;
    private TextMeshProUGUI _textMeshProUGUI;
    void Start()
    {
        Player_Stats.OnGameOver += GameOver;
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _textMeshProUGUI.enabled = false;
    }
    
    void GameOver() {
        _isDead = true;
        _textMeshProUGUI.enabled = true;
    }

    private void Update() {
        if (_isDead) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadMenu();
            }
        }
    }

    private void OnDestroy() {
        Player_Stats.OnGameOver -= GameOver;
    }

    void LoadMenu() {
        Player_Stats.OnGameOver -= GameOver;
        SceneManager.LoadScene("MainMenu");
    }
}
