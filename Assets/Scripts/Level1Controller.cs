using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Controller : MonoBehaviour
{
    public bool discussedRumors = false;
    public bool gaveAle = false;

    public bool DiscussedRumors => discussedRumors;
    public bool GaveAle => gaveAle;

    public void DiscussRumors()
    {
        discussedRumors = true;
        Debug.Log("Вы обсудили слухи с информатором.");
    }

    public void GiveAle()
    {
        if (discussedRumors)
        {
            gaveAle = true;
            Debug.Log("Информатор выпил эль и готов говорить.");
        }
        else Debug.Log("Сначала нужно обсудить слухи.");
    }

    public void AskForMap()
    {
        if (discussedRumors && gaveAle)
        {
            Debug.Log("Вы получили настоящую карту!");
            PlayerPrefs.SetInt("Level1Good", 1);
        }
        else if (discussedRumors)
        {
            Debug.Log("Информатор дал фальшивую карту...");
            PlayerPrefs.SetInt("Level1Good", 0);
        }

        SceneManager.LoadScene("LevelSelect");
    }
}