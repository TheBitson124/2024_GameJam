using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour {
    [SerializeField] private string firstLevelName = "";
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadFirstLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    
    private void LoadFirstLevel()
    {
        // Przejście do określonej sceny
        SceneManager.LoadScene(firstLevelName);
    }
    
}