using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadScene("LevelSelect");
    public void ContinueGame()
    {
        if (PlayerPrefs.GetInt("Level1Good", 0) == 1)
            SceneManager.LoadScene("LevelSelect");
    }
    public void ExitGame() => Application.Quit();
}
