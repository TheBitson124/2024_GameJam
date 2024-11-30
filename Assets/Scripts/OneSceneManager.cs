using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class OneSceneManager : MonoBehaviour
    {
        private static OneSceneManager instance;
        private int lvl;

        void Awake()
        {
            // Sprawdź, czy już istnieje instancja PersistentVolume
            if (instance == null)
            {
                instance = this;
                lvl = 0;
                DontDestroyOnLoad(gameObject); // Utrzymaj obiekt między scenami
            }
            else
            {
                Destroy(gameObject); // Usuń duplikaty
            }
        }

        public void NextScene()
        {
            if (SceneManager.GetActiveScene().name == "Transition")
            {
                if (lvl == 0)
                {
                    SceneManager.LoadScene("Tutorial");
                }
                lvl++;
                SceneManager.LoadScene("Level" + (lvl-1));
            }
            else
            {
                SceneManager.LoadScene("Transition");
            }
        }
    }
}