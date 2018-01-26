using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void LoadGame()
    {
        SceneManager.LoadScene((int)Constants.SceneIndex.Game1);
    }

    public void LoadHowTo()
    {
        SceneManager.LoadScene((int)Constants.SceneIndex.HowToPlay);
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene((int)Constants.SceneIndex.Options);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene((int)Constants.SceneIndex.Credits);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
