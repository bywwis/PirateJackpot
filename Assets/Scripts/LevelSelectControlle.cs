using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    public Button level1, level2, level3_1, level3_2;

    void Start()
    {
        level2.interactable = PlayerPrefs.GetInt("Level1Good", 0) == 1;
        level3_1.interactable = PlayerPrefs.GetInt("Level2Good", 0) == 1;
        level3_2.interactable = PlayerPrefs.GetInt("Level2Bad", 0) == 1;
    }

    public void OpenLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenLevel2()
    {
        SceneManager.LoadScene("level2");
    }

    public void OpenLevel31()
    {
        SceneManager.LoadScene("level31");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}