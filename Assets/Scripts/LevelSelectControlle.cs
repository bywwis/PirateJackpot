using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    public Button level1, level2, level3_1, level3_2;

    void Start()
    {
        level2.interactable = PlayerPrefs.GetInt("Level1Good", 0) == 1;
        
        int level2Ending = PlayerPrefs.GetInt("Level2Ending", 0);
        level3_1.interactable = level2Ending == 1; // хорошая концовка
        level3_2.interactable = level2Ending == 2; // плохая концовка
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
        SceneManager.LoadScene("Level31");
    }

    public void OpenLevel32()
    {
        SceneManager.LoadScene("Level32");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}