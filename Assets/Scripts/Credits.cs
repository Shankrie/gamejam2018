using UnityEngine;
using UnityEngine.SceneManagement;
namespace TAHL.Transmission
{
    public class Credits : MonoBehaviour
    {

        // Use this for initialization
        public Texture BackgText;
        string FourPotatoesTeam = "Made by 4Potatoes Productions";
        string Programming_credits = "\tProgramming\n\nHenrikas Jasiūnas\nAurimas Mikėnas\nLukas Tutkus";
        string Sound_credits = "\n\tSound design\n\nAurimas Mikėnas\nThanks to free sound libraries:\nhttps://freesound.org/";
        string Art_credits = "\n\tArt and animation\n\nTomas Pabiržis\nHenrikas Jasiūnas";
        //string Animation_credits = "\n\tAnimation\n\nTomas Pabiržis\n";
        //string 
        GUIStyle Credits_style = new GUIStyle();
        void Update()
        {
            Cursor.visible = true;
        }

        void OnGUI()
        {

            Credits_style.fontSize = 18;
            Credits_style.normal.textColor = Color.white;


            GUI.Label(new Rect(Screen.width * .375f, Screen.height * .10f, Screen.width * .25f, Screen.height * .25f), FourPotatoesTeam, Credits_style);
            GUI.Label(new Rect(Screen.width * .375f, Screen.height * .25f, Screen.width * .25f, Screen.height * .25f), Programming_credits, Credits_style);
            GUI.Label(new Rect(Screen.width * .375f, Screen.height * .45f, Screen.width * .25f, Screen.height * .25f), Sound_credits, Credits_style);
            GUI.Label(new Rect(Screen.width * .375f, Screen.height * .65f, Screen.width * .25f, Screen.height * .25f), Art_credits, Credits_style);
            //GUI.Label(new Rect(Screen.width * .375f, Screen.height * .85f, Screen.width * .25f, Screen.height * .25f), Animation_credits, Credits_style);

            if (GUI.Button(new Rect(Screen.width * .375f, Screen.height * 0.9f, Screen.width * .25f, Screen.height * .05f), "Back"))
            {
                SceneManager.LoadScene((int)Globals.SceneIndex.MainMenu);
            }


        }
    }
}