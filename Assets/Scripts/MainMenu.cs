using UnityEngine;
using UnityEngine.SceneManagement;

namespace TAHL.Transmission
{
    public class MainMenu : MonoBehaviour
    {

        public void LoadSurvivalScene()
        {
            SceneManager.LoadScene((int)Globals.SceneIndex.SurvivalScene);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene((int)Globals.SceneIndex.Game1);
        }

        public void LoadHowTo()
        {
            SceneManager.LoadScene((int)Globals.SceneIndex.HowToPlay);
        }

        public void LoadCredits()
        {
            SceneManager.LoadScene((int)Globals.SceneIndex.Credits);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene((int)Globals.SceneIndex.MainMenu);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }

}