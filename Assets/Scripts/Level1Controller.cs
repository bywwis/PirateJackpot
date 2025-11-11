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
        int endingType = 0;
        
        if (discussedRumors && gaveAle)
        {
            Debug.Log("Вы получили настоящую карту! ХОРОШАЯ КОНЦОВКА");
            endingType = 1; // Хорошая концовка
        }
        else if (discussedRumors)
        {
            Debug.Log("Информатор дал фальшивую карту... ПЛОХАЯ КОНЦОВКА");
            endingType = 2; // Плохая концовка
        }
        else
        {
            Debug.Log("Вы даже не поговорили с информатором... ОЧЕНЬ ПЛОХАЯ КОНЦОВКА");
            endingType = 3; // Очень плохая концовка
        }

        // Сохраняем прогресс
        Debug.Log($"=== СОХРАНЕНИЕ ПРОГРЕССА Level1 ===");
        Debug.Log($"Концовка: {endingType} (1=хорошая, 2=плохая, 3=очень плохая)");
        
        SaveLoadManager.SaveLevelProgress("Level1", endingType);
        
        // Для совместимости со старым кодом также сохраняем в PlayerPrefs
        PlayerPrefs.SetInt("Level1Good", endingType == 1 ? 1 : 0);
        PlayerPrefs.Save();
        
        Debug.Log("Прогресс Level1 сохранен!");
        
        // НЕ загружаем сцену здесь - это делает Level1SceneManager
    }
}