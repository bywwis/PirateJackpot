using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3_1Controller : MonoBehaviour
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
            Debug.Log("Вы выкопали песок лопатой и нашли сундук!");
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
            Debug.Log("Вы открыли сундук и нашли сокровища! Уровень 3.1 пройден!");
            
            // Сохраняем результат прохождения
            SaveLoadManager.SaveLevelProgress("Level3_1", 1);
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