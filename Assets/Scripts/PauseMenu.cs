using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    private bool isPaused;

    private TextMeshProUGUI wieksze;

    private void Start() {
        wieksze = GetComponent<TextMeshProUGUI>();

        wieksze.enabled = false;
    }

    void Update()
    {
        // pauzowania/wznawianie
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        //main menu
        if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }
    void Pause() {
        wieksze.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        wieksze.enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void ReturnToMainMenu()
    {
        wieksze.enabled = false;
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
    }
}
