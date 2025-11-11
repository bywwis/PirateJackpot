using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameProgress
{
    public List<string> completedLevels = new List<string>();
    public Dictionary<string, int> levelEndings = new Dictionary<string, int>();
    
    // Для сериализации Dictionary в JsonUtility
    [System.Serializable]
    public struct LevelEndingPair
    {
        public string levelName;
        public int endingType;
    }
    
    public List<LevelEndingPair> levelEndingsList = new List<LevelEndingPair>();
    
    // Конвертируем Dictionary в List для сериализации
    public void PrepareForSave()
    {
        levelEndingsList.Clear();
        foreach (var pair in levelEndings)
        {
            levelEndingsList.Add(new LevelEndingPair { levelName = pair.Key, endingType = pair.Value });
            Debug.Log($"Подготовка к сохранению: {pair.Key} -> {pair.Value}");
        }
    }
    
    // Конвертируем List обратно в Dictionary после загрузки
    public void PrepareAfterLoad()
    {
        levelEndings.Clear();
        foreach (var pair in levelEndingsList)
        {
            levelEndings[pair.levelName] = pair.endingType;
            Debug.Log($"Восстановление после загрузки: {pair.levelName} -> {pair.endingType}");
        }
    }
}