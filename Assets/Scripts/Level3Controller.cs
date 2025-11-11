using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Controller : MonoBehaviour
{
    public bool exploredBeach = false;
    public bool dugSand = false;
    public bool openedChest = false;

    public bool ExploredBeach => exploredBeach;
    public bool DugSand => dugSand;
    public bool OpenedChest => openedChest;

    public void ExploreBeach()
    {
        exploredBeach = true;
        Debug.Log("Вы осмотрели пляж и нашли подозрительное место с крестиком.");
    }

    public void DigSand()
    {
        if (exploredBeach)
        {
            dugSand = true;
            Debug.Log("Вы выкопали песок и нашли сундук!");
        }
        else
        {
            Debug.Log("Сначала нужно осмотреть пляж.");
        }
    }

    public void OpenChest()
    {
        if (dugSand)
        {
            openedChest = true;
            Debug.Log("Вы открыли сундук и нашли сокровища! Уровень пройден!");
            
            // Сохраняем результат прохождения
            SaveLoadManager.SaveLevelProgress("Level3_2", 1);
            
            // Переход к выбору уровней через 3 секунды
            Invoke("LoadLevelSelect", 3f);
        }
        else
        {
            Debug.Log("Сначала нужно выкопать сундук из песка.");
        }
    }

    private void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}