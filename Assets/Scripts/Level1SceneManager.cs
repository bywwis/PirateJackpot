using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level1SceneManager : MonoBehaviour
{
    [Header("Сцены")]
    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;
    
    [Header("Объекты сцены 1")]
    public GameObject tavern1;
    public GameObject mainHero1;
    public GameObject inform1;
    public GameObject chatChest1;
    
    [Header("Объекты сцены 2")]
    public GameObject tavern2;
    public GameObject mainHero2;
    public GameObject inform2;
    public GameObject ale2;
    public GameObject askMap2;
    
    [Header("Объекты сцены 3")]
    public GameObject tavern3;
    public GameObject mainHero3;
    public GameObject inform3;
    public GameObject askMap3;
    
    [Header("Кнопки действий")]
    public Button buttonHero;
    public Button buttonInfo;
    public Button buttonTavern;
    public Button buttonChat;
    public Button buttonAle;
    public Button buttonGet;
    public Button backButton;
    public Button cancelButton;

    private Level1Controller levelController;
    private int currentScene = 1;

    void Start()
    {
        levelController = FindObjectOfType<Level1Controller>();
        if (levelController == null)
        {
            GameObject controllerObj = new GameObject("Level1Controller");
            levelController = controllerObj.AddComponent<Level1Controller>();
        }
        
        InitializeScenes();
        UpdateUI();
    }

    // Инициализация всех сцен
    void InitializeScenes()
    {
        currentScene = 1;
        
        // Активируем все сцены, но показываем только первую
        scene1.SetActive(true);
        scene2.SetActive(true);
        scene3.SetActive(true);
        
        // Настраиваем начальное состояние всех объектов
        ResetAllScenes();
        
        // Показываем только первую сцену
        UpdateUI();
    }

    void ResetAllScenes()
    {
        // Сцена 1
        tavern1.SetActive(false);
        mainHero1.SetActive(false);
        inform1.SetActive(false);
        chatChest1.SetActive(false);
        
        // Сцена 2
        tavern2.SetActive(false);
        mainHero2.SetActive(false);
        inform2.SetActive(false);
        ale2.SetActive(false);
        askMap2.SetActive(false);
        
        // Сцена 3
        tavern3.SetActive(false);
        mainHero3.SetActive(false);
        inform3.SetActive(false);
        askMap3.SetActive(false);
    }

    // Обновление интерфейса в зависимости от текущей сцены
    void UpdateUI()
    {
        // Скрываем все сцены
        scene1.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(false);

        // Показываем только текущую сцену
        switch (currentScene)
        {
            case 1:
                scene1.SetActive(true);
                UpdateScene1UI();
                break;
            case 2:
                scene2.SetActive(true);
                UpdateScene2UI();
                break;
            case 3:
                scene3.SetActive(true);
                UpdateScene3UI();
                break;
        }
    }

    void UpdateScene1UI()
    {
        bool heroVisible = mainHero1.activeSelf;
        bool informantVisible = inform1.activeSelf;
        bool tavernVisible = tavern1.activeSelf;
        
        buttonHero.interactable = !heroVisible;
        buttonInfo.interactable = !informantVisible;
        buttonTavern.interactable = !tavernVisible;
        buttonChat.interactable = heroVisible && informantVisible && tavernVisible;
        buttonAle.interactable = false;
        buttonGet.interactable = false;
        
        backButton.interactable = false;
        cancelButton.interactable = heroVisible || informantVisible || tavernVisible;
    }

    void UpdateScene2UI()
    {
        bool heroVisible = mainHero2.activeSelf;
        bool informantVisible = inform2.activeSelf;
        bool tavernVisible = tavern2.activeSelf;
        bool aleVisible = ale2.activeSelf;
        
        buttonHero.interactable = !heroVisible;
        buttonInfo.interactable = !informantVisible;
        buttonTavern.interactable = !tavernVisible;
        buttonChat.interactable = false;
        buttonAle.interactable = heroVisible && informantVisible && tavernVisible && !aleVisible;
        buttonGet.interactable = heroVisible && informantVisible && tavernVisible; // Можно просить карту даже без эля!
        
        backButton.interactable = true;
        cancelButton.interactable = heroVisible || informantVisible || tavernVisible || aleVisible;
    }

    void UpdateScene3UI()
    {
        bool heroVisible = mainHero3.activeSelf;
        bool informantVisible = inform3.activeSelf;
        bool tavernVisible = tavern3.activeSelf;
        
        buttonHero.interactable = !heroVisible;
        buttonInfo.interactable = !informantVisible;
        buttonTavern.interactable = !tavernVisible;
        buttonChat.interactable = false;
        buttonAle.interactable = false;
        buttonGet.interactable = heroVisible && informantVisible && tavernVisible;
        
        backButton.interactable = true;
        cancelButton.interactable = heroVisible || informantVisible || tavernVisible;
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

    public void ShowInformant()
    {
        switch (currentScene)
        {
            case 1:
                inform1.SetActive(true);
                break;
            case 2:
                inform2.SetActive(true);
                break;
            case 3:
                inform3.SetActive(true);
                break;
        }
        UpdateUI();
    }

    public void ShowTavern()
    {
        switch (currentScene)
        {
            case 1:
                tavern1.SetActive(true);
                break;
            case 2:
                tavern2.SetActive(true);
                break;
            case 3:
                tavern3.SetActive(true);
                break;
        }
        UpdateUI();
        Debug.Log("Таверна добавлена на сцену " + currentScene);
    }

    public void DiscussRumors()
    {
        if (currentScene == 1 && mainHero1.activeSelf && inform1.activeSelf && tavern1.activeSelf)
        {
            levelController.DiscussRumors();
            chatChest1.SetActive(true);
            UpdateUI();
            Debug.Log("Слухи обсуждены! Теперь можно переходить к следующей сцене");
            
            // АВТОМАТИЧЕСКИЙ ПЕРЕХОД К СЦЕНЕ 2 через 2 секунды
            StartCoroutine(AutoTransitionToScene2());
        }
        else
        {
            Debug.Log("Для обсуждения слухов нужно: герой, информатор и таверна на сцене!");
        }
    }

    public void GiveAle()
    {
        if (currentScene == 2 && mainHero2.activeSelf && inform2.activeSelf && tavern2.activeSelf)
        {
            levelController.GiveAle();
            ale2.SetActive(true);
            UpdateUI();
            Debug.Log("Эль подан информатору - путь к хорошей концовке открыт!");
            
            // АВТОМАТИЧЕСКИЙ ПЕРЕХОД К СЦЕНЕ 3 через 2 секунды
            StartCoroutine(AutoTransitionToScene3());
        }
        else
        {
            Debug.Log("Для подачи эля нужно: герой, информатор и таверна на сцене!");
        }
    }

    public void AskForMap()
    {
        if (currentScene == 2 && mainHero2.activeSelf && inform2.activeSelf && tavern2.activeSelf)
        {
            // Плохая концовка - просим карту без эля
            if (!ale2.activeSelf)
            {
                Debug.Log("Плохая концовка: Вы получили фальшивую карту!");
                PlayerPrefs.SetInt("Level1Good", 0);
                UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
            }
            else
            {
                // Хорошая концовка
                Debug.Log("Хорошая концовка: Вы получили настоящую карту!");
                PlayerPrefs.SetInt("Level1Good", 1);
                UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
            }
        }
        else if (currentScene == 3 && mainHero3.activeSelf && inform3.activeSelf && tavern3.activeSelf)
        {
            // Альтернативный путь - через сцену 3
            // Проверяем, был ли подан эль через Level1Controller
            if (levelController.GaveAle)
            {
                Debug.Log("Хорошая концовка: Вы получили настоящую карту!");
                PlayerPrefs.SetInt("Level1Good", 1);
            }
            else
            {
                Debug.Log("Плохая концовка: Вы получили фальшивую карту!");
                PlayerPrefs.SetInt("Level1Good", 0);
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
        }
        else
        {
            Debug.Log("Для запроса карты нужно: герой, информатор и таверна на сцене!");
        }
    }

    public void PreviousScene()
    {
        if (currentScene == 2)
        {
            TransitionToScene1();
        }
        else if (currentScene == 3)
        {
            TransitionToScene2();
        }
    }

    // === АВТОМАТИЧЕСКИЕ ПЕРЕХОДЫ МЕЖДУ СЦЕНАМИ ===
    
    private IEnumerator AutoTransitionToScene2()
    {
        Debug.Log("Автоматический переход к сцене 2 через 2 секунды...");
        yield return new WaitForSeconds(2f);
        TransitionToScene2();
    }

    private IEnumerator AutoTransitionToScene3()
    {
        Debug.Log("Автоматический переход к сцене 3 через 2 секунды...");
        yield return new WaitForSeconds(2f);
        TransitionToScene3();
    }

    private void TransitionToScene2()
    {
        currentScene = 2;
        UpdateUI();
        Debug.Log("Переход ко второй сцене: Можно подать эль или сразу попросить карту");
    }

    private void TransitionToScene3()
    {
        currentScene = 3;
        UpdateUI();
        Debug.Log("Переход к третьей сцене: Можно попросить карту");
    }

    private void TransitionToScene1()
    {
        currentScene = 1;
        UpdateUI();
        Debug.Log("Возврат к первой сцене");
    }

    public void CancelAction()
    {
        switch (currentScene)
        {
            case 1:
                mainHero1.SetActive(false);
                inform1.SetActive(false);
                tavern1.SetActive(false);
                chatChest1.SetActive(false);
                break;

            case 2:
                mainHero2.SetActive(false);
                inform2.SetActive(false);
                tavern2.SetActive(false);
                ale2.SetActive(false);
                break;

            case 3:
                mainHero3.SetActive(false);
                inform3.SetActive(false);
                tavern3.SetActive(false);
                break;
        }

        UpdateUI();
        Debug.Log("Все объекты скрыты с текущей сцены");
    }
}