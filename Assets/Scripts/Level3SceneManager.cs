using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Level3SceneManager : MonoBehaviour
{
    [Header("Сцены")]
    public GameObject scene1; 
    public GameObject scene2; 
    public GameObject scene3; 

    [Header("Объекты сцены 1")]
    public GameObject beach1;
    public GameObject beachCross1;
    public GameObject mainHero1;
    public GameObject mainHeroExploreBeach;

    [Header("Объекты сцены 2")]
    public GameObject beach2;
    public GameObject beachChest2;
    public GameObject mainHero2;
    public GameObject mainHeroDig;

    [Header("Объекты сцены 3")]
    public GameObject beachOpenChest3;
    public GameObject mainHero3;
    public GameObject mainHeroOpenChest;

    [Header("Кнопки действий")]
    public Button buttonHero;
    public Button buttonBeach;
    public Button buttonExplore;
    public Button buttonDig;
    public Button buttonOpenChest;
    public Button backToMenuButton;

    public GameObject badEnd;

    private Level3Controller levelController;
    private int currentScene = 1;

    void Start()
    {
        // Проверяем, что уровень 2 пройден с хорошей концовкой
        if (!SaveLoadManager.IsLevelUnlocked("Level3_2"))
        {
            SceneManager.LoadScene("LevelSelect");
            return;
        }

        levelController = FindAnyObjectByType<Level3Controller>();
        if (levelController == null)
        {
            GameObject controllerObj = new GameObject("Level3Controller");
            levelController = controllerObj.AddComponent<Level3Controller>();
        }

        // Назначаем методы на кнопки
        buttonHero.onClick.AddListener(ShowHero);
        buttonBeach.onClick.AddListener(ShowBeach);
        buttonExplore.onClick.AddListener(ExploreBeach);
        buttonDig.onClick.AddListener(DigSand);
        buttonOpenChest.onClick.AddListener(OpenChest);
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
        mainHero1.SetActive(false);
        mainHeroExploreBeach.SetActive(false);

        // Сцена 2 - Пляж с сундуком
        beach2.SetActive(false);
        beachChest2.SetActive(false);
        mainHero2.SetActive(false);
        mainHeroDig.SetActive(false);

        // Сцена 3 - Пляж с открытым сундуком
        beachOpenChest3.SetActive(false);
        mainHero3.SetActive(false);
        mainHeroOpenChest.SetActive(false);
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
        bool beachExplored = mainHeroExploreBeach.activeSelf;

        buttonBeach.interactable = !beachVisible;
        buttonHero.interactable = beachVisible && !heroVisible && !beachExplored;
        buttonExplore.interactable = heroVisible && beachVisible && !beachExplored;
        buttonDig.interactable = false;
        buttonOpenChest.interactable = false;
    }

    void UpdateScene2UI()
    {
        bool heroVisible = mainHero2.activeSelf;
        bool beachVisible = beach2.activeSelf || beachChest2.activeSelf;
        bool beachExplored = levelController.ExploredBeach;
        bool sandDug = mainHeroDig.activeSelf;

        buttonBeach.interactable = !beachVisible;
        buttonHero.interactable = beachVisible && !heroVisible && !sandDug;
        buttonExplore.interactable = false;
        buttonDig.interactable = heroVisible && beachVisible && beachExplored && !sandDug;
        buttonOpenChest.interactable = false;
    }

    void UpdateScene3UI()
    {
        bool heroVisible = mainHero3.activeSelf;
        bool beachVisible = beachOpenChest3.activeSelf;
        bool chestOpened = mainHeroOpenChest.activeSelf;

        buttonBeach.interactable = !beachVisible;
        buttonHero.interactable = beachVisible && !heroVisible && !chestOpened;
        buttonExplore.interactable = false;
        buttonDig.interactable = false;
        buttonOpenChest.interactable = heroVisible && beachVisible && levelController.DugSand && !chestOpened;
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
                break;
            case 2:
            // На 2 сцене показываем обычный пляж, если сундук еще не выкопан
            if (!levelController.DugSand)
            {
                beach2.SetActive(true);
            }
            else
            {
                // Если сундук уже выкопан, показываем пляж с сундуком
                beachChest2.SetActive(true);
            }
            break;
            case 3:
                beachOpenChest3.SetActive(true);
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
            mainHeroExploreBeach.SetActive(true);

            UpdateUI();
            Debug.Log("Пляж изучен! Найдено место с крестиком.");

            StartCoroutine(AutoTransitionToScene2());
        }
    }

    public void DigSand()
    {
        if (currentScene == 2 && mainHero2.activeSelf && beach2.activeSelf && levelController.ExploredBeach)
        {
            levelController.DigSand();

            mainHero2.SetActive(false);
            mainHeroDig.SetActive(true);

            beach2.SetActive(false);
            beachChest2.SetActive(true);

            UpdateUI();
            Debug.Log("Сундук выкопан! Теперь можно его открыть.");

            StartCoroutine(AutoTransitionToScene3());
        }
    }

    public void OpenChest()
    {
        if (currentScene == 3 && mainHero3.activeSelf && beachOpenChest3.activeSelf && levelController.DugSand)
        {
            levelController.OpenChest();

            mainHero3.SetActive(false);
            mainHeroOpenChest.SetActive(true);

            UpdateUI();
            Debug.Log("ПОЗДРАВЛЯЕМ! Вы нашли сокровища!");

            badEnd.SetActive(true);
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
        Debug.Log("Переход ко второй сцене: Можно выкопать сундук");
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