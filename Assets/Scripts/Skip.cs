using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TAHL.Transmission {

    public class Skip : MonoBehaviour {

        // Update is called once per frame
        void Update() {
            if (Input.anyKey)
            {
                SceneManager.LoadScene((int)Globals.SceneIndex.MainMenu);
            }
        }
    }
}
