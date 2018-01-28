using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TAHL.Transmission
{
    public class GameMenu : MonoBehaviour
    {
        public GameObject GameWinDialog;
        public GameObject GameMenuDialog;
        public Text TimeLabel;
        
        private int gameTime = 60;
        private float startTime;

        private void Start()
        {
            TimeLabel.text = "00:" + gameTime.ToString();
            startTime = Time.time;
            Time.timeScale = 1;
        }

        private void Update()
        {
            int secondsSinceStart = (int)(Time.time - startTime);
            int timeLeftNum = gameTime - secondsSinceStart;
            if (timeLeftNum <= 0)
            {
                timeLeftNum = 0;
                GameWinDialog.SetActive(true);
                Time.timeScale = 0;
            } 

            string timeLeft = timeLeftNum.ToString();

            if (timeLeft.Length > 1)
            {
                TimeLabel.text = "00:" + timeLeft.ToString();
            }
            else
            {
                TimeLabel.text = "00:0" + timeLeft.ToString();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            { 
                if (GameMenuDialog.activeSelf)
                {
                    ResumeGame();
                }
                else if (Time.timeScale != 0)
                {
                    GameMenuDialog.SetActive(true);
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
            GameMenuDialog.SetActive(false);
            Time.timeScale = 1;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}