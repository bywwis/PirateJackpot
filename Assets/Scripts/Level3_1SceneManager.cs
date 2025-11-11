using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Level3_1SceneManager : MonoBehaviour
{
    [Header("Сцены")]
    public GameObject scene1; 
    public GameObject scene2; 
    public GameObject scene3; 

    [Header("Объекты сцены 1 - Осмотр пляжа")]
    public GameObject beach1;
    public GameObject beachCross1;
    public GameObject mainHero1;
    public GameObject mainHeroExploreMap;

    [Header("Объекты сцены 2 - Копание песка")]
    public GameObject beachCross2;
    public GameObject beachPit2;
    public GameObject chest2;
    public GameObject mainHero2;
    public GameObject mainHeroDig2;

    [Header("Объекты сцены 3 - Открытие сундука")]
    public GameObject beachPit3;
    public GameObject mainHero3;
    public GameObject chest3;
    public GameObject chestOpen3;

    [Header("Кнопки действий")]
    public Button buttonExplore;
    public Button buttonHero;
    public Button buttonBeach;
    public Button buttonDig;
    public Button buttonOpen;
    public Button backToMenuButton;

    public GameObject goodEnd;

    private Level3_1Controller levelController;
    private int currentScene = 1;

    void Start()
    {
        // Проверяем, что уровень 2 пройден с хорошей концовкой
        if (!SaveLoadManager.IsLevelUnlocked("Level3_1"))
        {
            SceneManager.LoadScene("LevelSelect");
            return;
        }

        levelController = FindAnyObjectByType<Level3_1Controller>();
        if (levelController == null)
        {
            GameObject controllerObj = new GameObject("Level3_1Controller");
            levelController = controllerObj.AddComponent<Level3_1Controller>();
        }

        // Назначаем методы на кнопки
        buttonHero.onClick.AddListener(ShowHero);
        buttonBeach.onClick.AddListener(ShowBeach);
        buttonExplore.onClick.AddListener(ExploreBeach);
        buttonDig.onClick.AddListener(DigSand);
        buttonOpen.onClick.AddListener(OpenChest);
        backToMenuButton.onClick.AddListener(BackToMenu);

        InitializeScenes();
        UpdateUI();
    }

    void InitializeScenes()
    {
        currentScene = 1;

        // Активируем все сцены
        scene1.SetActive(true);
        scene2.SetActive(true);
        scene3.SetActive(true);

        ResetAllScenes();
        UpdateUI();
    }

    void ResetAllScenes()
    {
        // Сцена 1 - Пляж с крестиком
        beach1.SetActive(false);
        beachCross1.SetActive(false);
        mainHero1.SetActive(false);
        mainHeroExploreMap.SetActive(false);

        // Сцена 2 - Пляж с ямой и сундуком
        beachCross2.SetActive(false);
        beachPit2.SetActive(false);
        chest2.SetActive(false);
        mainHero2.SetActive(false);
        mainHeroDig2.SetActive(false);

        // Сцена 3 - Открытие сундука
        beachPit3.SetActive(false);
        mainHero3.SetActive(false);
        chest3.SetActive(false);
        chestOpen3.SetActive(false);
    }

    void UpdateUI()
    {
        switch (currentScene)
        {
            case 1:
                UpdateScene1UI();
                break;
            case 2:
                UpdateScene2UI();
                break;
            case 3:
                UpdateScene3UI();
                break;
        }
    }

    void UpdateScene1UI()
    {
        bool heroVisible = mainHero1.activeSelf;
        bool beachVisible = beach1.activeSelf;
        bool beachExplored = mainHeroExploreMap.activeSelf;

        buttonBeach.interactable = !beachVisible;
        buttonHero.interactable = beachVisible && !heroVisible && !beachExplored;
        buttonExplore.interactable = heroVisible && beachVisible && !beachExplored;
        buttonDig.interactable = false;
        buttonOpen.interactable = false;
    }

    void UpdateScene2UI()
    {
        bool heroVisible = mainHero2.activeSelf;
        bool beachVisible = beachCross2.activeSelf || beachPit2.activeSelf;
        bool beachExplored = levelController.ExploredBeach;
        bool sandDug = mainHeroDig2.activeSelf;

        buttonBeach.interactable = !beachVisible;
        buttonHero.interactable = beachVisible && !heroVisible && !sandDug;
        buttonExplore.interactable = false;
        buttonDig.interactable = heroVisible && beachVisible && beachExplored && !sandDug;
        buttonOpen.interactable = false;
    }

    void UpdateScene3UI()
    {
        bool heroVisible = mainHero3.activeSelf;
        bool beachVisible = beachPit3.activeSelf;
        bool chestOpened = chestOpen3.activeSelf;

        buttonBeach.interactable = !beachVisible;
        buttonHero.interactable = beachVisible && !heroVisible && !chestOpened;
        buttonExplore.interactable = false;
        buttonDig.interactable = false;
        buttonOpen.interactable = heroVisible && beachVisible && levelController.DugSand && !chestOpened;
    }

    // === МЕТОДЫ ДЛЯ КНОПОК ===

    public void ShowHero()
    {
        switch (currentScene)
        {
            case 1:
                mainHero1.SetActive(true);
                break;
            case 2:
                mainHero2.SetActive(true);
                break;
            case 3:
                mainHero3.SetActive(true);
                break;
        }
        UpdateUI();
    }

    public void ShowBeach()
    {
        switch (currentScene)
        {
            case 1:
                beach1.SetActive(true);
                beachCross1.SetActive(true);
                break;
            case 2:
                if (!levelController.DugSand)
                {
                    beachCross2.SetActive(true);
                }
                else
                {
                    beachPit2.SetActive(true);
                    chest2.SetActive(true);
                }
                break;
            case 3:
                beachPit3.SetActive(true);
                chest3.SetActive(true);
                break;
        }
        UpdateUI();
    }

    public void ExploreBeach()
    {
        if (currentScene == 1 && mainHero1.activeSelf && beach1.activeSelf)
        {
            levelController.ExploreBeach();

            mainHero1.SetActive(false);
            mainHeroExploreMap.SetActive(true);

            UpdateUI();
            Debug.Log("Пляж изучен! Найдено место с крестиком.");

            StartCoroutine(AutoTransitionToScene2());
        }
    }

    public void DigSand()
    {
        if (currentScene == 2 && mainHero2.activeSelf && beachCross2.activeSelf && levelController.ExploredBeach)
        {
            levelController.DigSand();

            mainHero2.SetActive(false);
            mainHeroDig2.SetActive(true);

            beachCross2.SetActive(false);
            beachPit2.SetActive(true);
            chest2.SetActive(true);

            UpdateUI();
            Debug.Log("Сундук выкопан лопатой! Теперь можно его открыть.");

            StartCoroutine(AutoTransitionToScene3());
        }
    }

    public void OpenChest()
    {
        if (currentScene == 3 && mainHero3.activeSelf && beachPit3.activeSelf && levelController.DugSand)
        {
            levelController.OpenChest();

            mainHero3.SetActive(false);
            chest3.SetActive(false);
            chestOpen3.SetActive(true);

            UpdateUI();

            goodEnd.SetActive(true);
        }
    }

    // === АВТОМАТИЧЕСКИЕ ПЕРЕХОДЫ ===

    private IEnumerator AutoTransitionToScene2()
    {
        yield return new WaitForSeconds(2f);
        TransitionToScene2();
    }

    private IEnumerator AutoTransitionToScene3()
    {
        yield return new WaitForSeconds(2f);
        TransitionToScene3();
    }

    private void TransitionToScene2()
    {
        currentScene = 2;
        UpdateUI();
        Debug.Log("Переход ко второй сцене: Можно выкопать сундук лопатой");
    }

    private void TransitionToScene3()
    {
        currentScene = 3;
        UpdateUI();
        Debug.Log("Переход к третьей сцене: Можно открыть сундук");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}