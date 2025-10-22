using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Button continueButton;
    public AudioSource clickSound;
    public GameObject guidePage;

    void Start()
    {
        // Проверяем, есть ли сохраненный прогресс
        //continueButton.interactable = SaveLoadManager.LoadProgress() != null;
    }

    // Метод для кнопки "Продолжить"
    public void ContinueGame()
    {
        //clickSound.Play();
        //Invoke(nameof(LoadContinue), clickSound.clip.length);
    }

    // Метод для кнопки "Начать игру"
    public void StartGame()
    {
        //clickSound.Play();
        //Invoke(nameof(LoadNew), clickSound.clip.length);
    }

    //private void LoadContinue()
    //{
    //    GameProgress progress = SaveLoadManager.LoadProgress();
    //    if (progress != null)
    //    {
            // Загружаем последний пройденный уровень
    //        string lastCompletedLevel = progress.completedLevels[progress.completedLevels.Count - 1];
    //        int nextLevelIndex = SceneUtility.GetBuildIndexByScenePath(lastCompletedLevel) + 1;

    //        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings - 1)
    //        {
    //            SceneManager.LoadScene(nextLevelIndex);
    //        }
    //        else
    //        {
    //            SceneManager.LoadScene(lastCompletedLevel);
    //        }
    //    }
    //}

    public void LoadNew()
    {
        //SaveLoadManager.ResetProgress(); // Сбрасываем прогресс
        SceneManager.LoadScene("levelMenu"); // Загружаем первый уровень
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("level1");

    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("level2");

    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("level3");

    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Игра закрыта");
    }

}

