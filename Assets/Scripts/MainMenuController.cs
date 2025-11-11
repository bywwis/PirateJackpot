using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartNewGame()
    {
        // Сбрасываем прогресс и начинаем новую игру
        SaveLoadManager.ResetProgress();
        SceneManager.LoadScene("LevelSelect");
    }
    
    public void ContinueGame()
    {
        // Проверяем, есть ли сохраненный прогресс
        GameProgress progress = SaveLoadManager.LoadProgress();
        if (progress != null && progress.completedLevels.Count > 0)
        {
            SceneManager.LoadScene("LevelSelect");
        }
        else
        {
            // Если сохранений нет, начинаем с начала
            StartNewGame();
        }
    }
    
    public void ExitGame() => Application.Quit();
}