using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Controller : MonoBehaviour
{
    public bool studiedMap = false;
    public bool studiedLegend = false;
    public bool comparedWithSeaMap = false;

    public bool StudiedMap => studiedMap;
    public bool StudiedLegend => studiedLegend;
    public bool ComparedWithSeaMap => comparedWithSeaMap;

    public void StudyMap()
    {
        studiedMap = true;
        Debug.Log("Вы изучили карту сокровищ.");
    }

    public void StudyLegend()
    {
        if (studiedMap)
        {
            studiedLegend = true;
            Debug.Log("Вы изучили текст легенды.");
        }
        else
        {
            Debug.Log("Сначала нужно изучить карту.");
        }
    }

    public void CompareWithSeaMap()
    {
        if (studiedMap)
        {
            comparedWithSeaMap = true;

            int endingType = 2; // По умолчанию плохая концовка
            
            if (studiedLegend && studiedMap)
            {
                Debug.Log("Вы правильно расшифровали карту! Концовка для уровня 3.1");
                endingType = 1; // Хорошая концовка
            }
            else
            {
                Debug.Log("Вы неправильно расшифровали карту! Концовка для уровня 3.2");
                endingType = 2; // Плохая концовка
            }

            // Сохраняем прогресс
            SaveLoadManager.SaveLevelProgress("Level2", endingType);

            // Переход к выбору уровней через 3 секунды
            Invoke("LoadLevelSelect", 3f);
        }
        else
        {
            Debug.Log("Сначала нужно изучить карту.");
        }
    }

    private void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}