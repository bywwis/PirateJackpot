using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    public Button level1, level2, level3_1, level3_2;
    public Button resetProgressButton;

    void Start()
    {
        Debug.Log("=== LevelSelectController Start ===");
        Debug.Log("Путь к сохранениям: " + Application.persistentDataPath);
        
        // Проверяем сохранение
        GameProgress progress = SaveLoadManager.LoadProgress();
        if (progress != null)
        {
            Debug.Log("Загружен прогресс: " + progress.completedLevels.Count + " уровней пройдено");
            foreach (string level in progress.completedLevels)
            {
                Debug.Log("Пройден уровень: " + level);
            }
            
            // Проверяем концовки
            foreach (var ending in progress.levelEndings)
            {
                Debug.Log($"Концовка уровня {ending.Key}: {ending.Value}");
            }
        }
        else
        {
            Debug.Log("Сохранение не найдено");
        }

        // Проверяем доступность каждого уровня отдельно
        Debug.Log("=== ПРОВЕРКА ДОСТУПНОСТИ УРОВНЕЙ ===");
        
        bool level2Unlocked = SaveLoadManager.IsLevelUnlocked("Level2");
        bool level3_1Unlocked = SaveLoadManager.IsLevelUnlocked("Level3_1");
        bool level3_2Unlocked = SaveLoadManager.IsLevelUnlocked("Level3_2");
        
        Debug.Log($"Level2 доступен: {level2Unlocked}");
        Debug.Log($"Level3_1 доступен: {level3_1Unlocked}");
        Debug.Log($"Level3_2 доступен: {level3_2Unlocked}");

        // Устанавливаем доступность кнопок
        level2.interactable = level2Unlocked;
        level3_1.interactable = level3_1Unlocked;
        level3_2.interactable = level3_2Unlocked;

        // Назначаем метод на кнопку сброса прогресса
        if (resetProgressButton != null)
        {
            resetProgressButton.onClick.AddListener(ResetProgress);
        }
        
        Debug.Log("=== НАСТРОЙКА КНОПОК ЗАВЕРШЕНА ===");
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
    
    public void ResetProgress()
    {
        SaveLoadManager.ResetProgress();
        // Перезагружаем сцену для обновления UI
        SceneManager.LoadScene("LevelSelect");
    }
}