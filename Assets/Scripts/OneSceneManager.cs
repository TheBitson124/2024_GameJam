using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class OneSceneManager : MonoBehaviour
    {
        private static OneSceneManager instance;
        private static int lvl;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                lvl = 0;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void resetLevel()
        {
            lvl = 0;
        }

        public void NextScene()
        {
            if (SceneManager.GetActiveScene().name == "Transition" || SceneManager.GetActiveScene().name == "Main Menu")
            {
                if (lvl == 0)
                {
                    SceneManager.LoadScene("Tutorial");
                }
                else if(lvl >= 3)
                {
                    SceneManager.LoadScene("Boss");
                }
                lvl++;
                Debug.Log(lvl);
                SceneManager.LoadScene("Level" + lvl);
            }
            else
            {
                SceneManager.LoadScene("Transition");
            }
        }
    }
}