using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Level2SceneManager : MonoBehaviour
{
    [Header("Сцены")]
    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;

    [Header("Объекты сцены 1")]
    public GameObject cabinet1;
    public GameObject mainHero1;
    public GameObject mainHeroExploreMap;

    [Header("Объекты сцены 2")]
    public GameObject cabinet2;
    public GameObject mainHero2;
    public GameObject mainHeroExploreLegend;
    public GameObject mainHeroCompare;

    [Header("Объекты сцены 3")]
    public GameObject cabinet3;
    public GameObject mainHero3;
    public GameObject mainHeroCompareScene3;

    [Header("Кнопки действий")]
    public Button buttonHero;
    public Button buttonCabinet;
    public Button buttonExploreMap;
    public Button buttonExploreLegend;
    public Button buttonCompare;
    public Button backToMenuButton;

    private Level2Controller levelController;
    private int currentScene = 1;

    void Start()
    {
        if (PlayerPrefs.GetInt("Level1Good", 0) != 1)
        {
            SceneManager.LoadScene("LevelSelect");
            return;
        }

        levelController = FindAnyObjectByType<Level2Controller>();
        if (levelController == null)
        {
            GameObject controllerObj = new GameObject("Level2Controller");
            levelController = controllerObj.AddComponent<Level2Controller>();
        }

        buttonHero.onClick.AddListener(ShowHero);
        buttonCabinet.onClick.AddListener(ShowCabinet);
        buttonExploreMap.onClick.AddListener(ExploreMap);
        buttonExploreLegend.onClick.AddListener(ExploreLegend);
        buttonCompare.onClick.AddListener(CompareWithSeaMap);
        backToMenuButton.onClick.AddListener(BackToMenu);

        InitializeScenes();
        UpdateUI();
    }

    void InitializeScenes()
    {
        currentScene = 1;

        scene1.SetActive(true);
        scene2.SetActive(true);
        scene3.SetActive(true);

        ResetAllScenes();
        UpdateUI();
    }

    void ResetAllScenes()
    {
        // Сцена 1
        cabinet1.SetActive(false);
        mainHero1.SetActive(false);
        mainHeroExploreMap.SetActive(false);

        // Сцена 2
        cabinet2.SetActive(false);
        mainHero2.SetActive(false);
        mainHeroExploreLegend.SetActive(false);
        mainHeroCompare.SetActive(false);

        // Сцена 3
        cabinet3.SetActive(false);
        mainHero3.SetActive(false);
        mainHeroCompareScene3.SetActive(false);
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
        bool cabinetVisible = cabinet1.activeSelf;
        bool mapExplored = mainHeroExploreMap.activeSelf;

        buttonCabinet.interactable = !cabinetVisible;
        buttonHero.interactable = cabinetVisible && !heroVisible && !mapExplored;
        buttonExploreMap.interactable = heroVisible && cabinetVisible && !mapExplored;
        buttonExploreLegend.interactable = false;
        buttonCompare.interactable = false;
    }

    void UpdateScene2UI()
    {
        bool heroVisible = mainHero2.activeSelf;
        bool cabinetVisible = cabinet2.activeSelf;
        bool mapExplored = levelController.StudiedMap;
        bool legendExplored = mainHeroExploreLegend.activeSelf;
        bool compared = mainHeroCompare.activeSelf;

        buttonCabinet.interactable = !cabinetVisible;
        buttonHero.interactable = cabinetVisible && !heroVisible && !legendExplored;
        buttonExploreMap.interactable = false;
        buttonExploreLegend.interactable = heroVisible && cabinetVisible && mapExplored && !legendExplored && !compared;
        buttonCompare.interactable = heroVisible && cabinetVisible && mapExplored && !compared;
    }

    void UpdateScene3UI()
    {
        bool heroVisible = mainHero3.activeSelf;
        bool cabinetVisible = cabinet3.activeSelf;
        bool compared = mainHeroCompareScene3.activeSelf;

        buttonCabinet.interactable = !cabinetVisible;
        buttonHero.interactable = cabinetVisible && !heroVisible && !compared;
        buttonExploreMap.interactable = false;
        buttonExploreLegend.interactable = false;
        buttonCompare.interactable = heroVisible && cabinetVisible && !compared;
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

    public void ShowCabinet()
    {
        switch (currentScene)
        {
            case 1:
                cabinet1.SetActive(true);
                break;
            case 2:
                cabinet2.SetActive(true);
                break;
            case 3:
                cabinet3.SetActive(true);
                break;
        }
        UpdateUI();
    }

    public void ExploreMap()
    {
        if (currentScene == 1 && mainHero1.activeSelf && cabinet1.activeSelf)
        {
            levelController.StudyMap();

            mainHero1.SetActive(false);
            mainHeroExploreMap.SetActive(true);

            UpdateUI();
            Debug.Log("Карта изучена! Теперь можно переходить к следующей сцене");

            StartCoroutine(AutoTransitionToScene2());
        }
    }

    public void ExploreLegend()
    {
        if (currentScene == 2 && mainHero2.activeSelf && cabinet2.activeSelf && levelController.StudiedMap)
        {
            levelController.StudyLegend();

            mainHero2.SetActive(false);
            mainHeroExploreLegend.SetActive(true);

            UpdateUI();
            Debug.Log("Легенда изучена! Теперь можно переходить к следующей сцене");

            StartCoroutine(AutoTransitionToScene3());
        }
    }

    public void CompareWithSeaMap()
    {
        if (currentScene == 2 && mainHero2.activeSelf && cabinet2.activeSelf && levelController.StudiedMap)
        {
            levelController.CompareWithSeaMap();

            mainHero2.SetActive(false);
            mainHeroExploreLegend.SetActive(false);
            mainHeroCompare.SetActive(true);

            UpdateUI();
            Debug.Log("ПЛОХАЯ КОНЦОВКА: Без знания легенды вы неправильно расшифровали карту!");
            PlayerPrefs.SetInt("Level2Ending", 2);

            StartCoroutine(ReturnToLevelSelect());
        }
        else if (currentScene == 3 && mainHero3.activeSelf && cabinet3.activeSelf && levelController.StudiedMap && levelController.StudiedLegend)
        {
            levelController.CompareWithSeaMap();

            mainHero3.SetActive(false);
            mainHeroCompareScene3.SetActive(true);

            UpdateUI();
            Debug.Log("ХОРОШАЯ КОНЦОВКА: Вы правильно расшифровали карту с помощью легенды!");
            PlayerPrefs.SetInt("Level2Ending", 1);

            StartCoroutine(ReturnToLevelSelect());
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

    private IEnumerator ReturnToLevelSelect()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("LevelSelect");
    }

    private void TransitionToScene2()
    {
        currentScene = 2;
        UpdateUI();
        Debug.Log("Переход ко второй сцене: Можно изучить легнеду или сразу сопоставить карту");
    }

    private void TransitionToScene3()
    {
        currentScene = 3;
        UpdateUI();
        Debug.Log("Переход к третьей сцене: Можно соспоставить карту");
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}