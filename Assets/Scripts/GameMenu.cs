using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TAHL.Transmission
{
    public class GameMenu : MonoBehaviour
    {

        public GameObject GameMenuDialog;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameMenuDialog.SetActive(true);
                Time.timeScale = 0;
            }
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene((int)Constants.SceneIndex.MainMenu);
        }

        public void ResumeGame()
        {
            GameMenuDialog.SetActive(false);
            Time.timeScale = 1;
        }
    }

}