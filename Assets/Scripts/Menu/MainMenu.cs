using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevelSelect() {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit() {
        Application.Quit();
    }
}
