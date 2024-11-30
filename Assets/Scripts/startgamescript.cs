using UnityEngine;
using UnityEngine.SceneManagement;

public class startgamescript : MonoBehaviour {
    [SerializeField] private string firstLevelName = "";
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadFirstLevel();
        }
    }
    
    private void LoadFirstLevel()
    {
        // Przejście do określonej sceny
        SceneManager.LoadScene(firstLevelName);
    }

    
}
