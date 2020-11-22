using UnityEngine;
using UnityEngine.SceneManagement;


namespace EscapeRoom {

    public class MainMenu : MonoBehaviour {

        public void LoadGameScene(int index) {
            SceneManager.LoadScene(index);
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
