using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    private bool isPaused;

    private GameObject pausemenu;

    private void Start() {
        pausemenu = this.gameObject;
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
    void Pause()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void ReturnToMainMenu()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
    }
}
