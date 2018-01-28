using UnityEngine;
using UnityEngine.SceneManagement;

namespace TAHL.Transmission
{
    public class GameMenu : MonoBehaviour
    {

        private GameObject _gameMenuDialog;

        private void Awake()
        {
            _gameMenuDialog = GameObject.FindGameObjectWithTag(Globals.Tags.MenuDialog);
            _gameMenuDialog.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            { 
                if (_gameMenuDialog.activeSelf)
                {
                    ResumeGame();
                }
                else
                {
                    _gameMenuDialog.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene((int)Globals.SceneIndex.MainMenu);
        }

        public void ResumeGame()
        {
            _gameMenuDialog.SetActive(false);
            Time.timeScale = 1;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}