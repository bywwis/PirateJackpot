using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("level1");
    }
    
    //public void ContinueGame()
    //{
    //    UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    //}
    
    public void LoadLevel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("level1");
    }
    
    public void LoadLevel2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("level2");
    }
    
    public void ExitToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu");
    }
}