using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
    private static string savePath = Application.persistentDataPath + "/progress.json";

    // Сохранение прогресса уровня
    public static void SaveLevelProgress(string levelName, int endingType = 1)
    {
        GameProgress progress = LoadProgress() ?? new GameProgress();

        Debug.Log($"Сохранение прогресса для уровня: {levelName}, концовка: {endingType}");

        // Добавляем уровень в список пройденных, если его там еще нет
        if (!progress.completedLevels.Contains(levelName))
        {
            progress.completedLevels.Add(levelName);
            Debug.Log($"Уровень {levelName} добавлен в список пройденных");
        }

        // Сохраняем тип концовки для уровня
        progress.levelEndings[levelName] = endingType;
        Debug.Log($"Для уровня {levelName} установлена концовка: {endingType}");

        // Подготавливаем данные для сохранения
        progress.PrepareForSave();

        // Сохраняем в JSON
        string json = JsonUtility.ToJson(progress);
        File.WriteAllText(savePath, json);

        Debug.Log("Progress saved to: " + savePath);
        Debug.Log("Progress data: " + json);
    }

    // Загрузка прогресса
    public static GameProgress LoadProgress()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string json = File.ReadAllText(savePath);
                GameProgress progress = JsonUtility.FromJson<GameProgress>(json);
                progress.PrepareAfterLoad(); // Восстанавливаем Dictionary
                Debug.Log("Прогресс загружен успешно");
                return progress;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Ошибка загрузки сохранения: " + e.Message);
                return null;
            }
        }
        Debug.Log("Файл сохранения не найден: " + savePath);
        return null;
    }

    // Проверка, пройден ли уровень
    public static bool IsLevelCompleted(string levelName)
    {
        GameProgress progress = LoadProgress();
        bool completed = progress != null && progress.completedLevels.Contains(levelName);
        Debug.Log($"Уровень {levelName} пройден: {completed}");
        return completed;
    }

    // Получение типа концовки для уровня
    public static int GetLevelEnding(string levelName)
    {
        GameProgress progress = LoadProgress();
        if (progress != null && progress.levelEndings.ContainsKey(levelName))
        {
            int ending = progress.levelEndings[levelName];
            Debug.Log($"Для уровня {levelName} концовка: {ending}");
            return ending;
        }
        Debug.Log($"Для уровня {levelName} концовка не найдена (0)");
        return 0; // 0 = не пройден
    }

    // Проверка доступности уровня
    public static bool IsLevelUnlocked(string levelName)
    {
        Debug.Log($"=== Проверка доступности уровня: {levelName} ===");

        // Уровень 1 всегда доступен
        if (levelName == "Level1") 
        {
            Debug.Log("Level1 всегда доступен");
            return true;
        }
        
        // Уровень 2 доступен после хорошей концовки Level1
        if (levelName == "Level2") 
        {
            bool unlocked = GetLevelEnding("Level1") == 1;
            Debug.Log($"Level2 доступен (Level1 хорошая концовка): {unlocked}");
            return unlocked;
        }
        
        // Level3_1 доступен после хорошей концовки Level2
        if (levelName == "Level3_1") 
        {
            bool unlocked = GetLevelEnding("Level2") == 1;
            Debug.Log($"Level3_1 доступен (Level2 хорошая концовка): {unlocked}");
            return unlocked;
        }
        
        // Level3_2 доступен после плохой концовки Level2
        if (levelName == "Level3_2") 
        {
            bool unlocked = GetLevelEnding("Level2") == 2;
            Debug.Log($"Level3_2 доступен (Level2 плохая концовка): {unlocked}");
            return unlocked;
        }
        
        Debug.Log($"Уровень {levelName} не распознан, доступен: false");
        return false;
    }

    public static void ResetProgress()
    {
        try
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                Debug.Log("Прогресс сброшен: файл сохранения удален.");
            }
            else
            {
                Debug.Log("Файл сохранения не найден, сбрасывать нечего.");
            }
            
            // Также сбрасываем PlayerPrefs для совместимости
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Ошибка при сбросе прогресса: " + e.Message);
        }
    }
    
    // Проверка существования сохранения
    public static bool SaveExists()
    {
        return File.Exists(savePath);
    }
}