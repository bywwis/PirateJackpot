using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level1SceneManager : MonoBehaviour
{
    [Header("Сцены")]
    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;

    [Header("Плохая концовка")]
    public GameObject badEnd;

    [Header("Объекты сцены 1")]
    public GameObject tavern1;
    public GameObject mainHero1;
    public GameObject inform1;
    public GameObject chatChest1;

    [Header("Объекты сцены 2")]
    public GameObject tavern2;
    public GameObject mainHero2;
    public GameObject inform2;
    public GameObject inform2Norm;
    public GameObject inform2BadEnd;
    public GameObject ale2;
    public GameObject askMap2;

    [Header("Объекты сцены 3")]
    public GameObject tavern3;
    public GameObject mainHero3;
    public GameObject inform3;
    public GameObject askMap3;
    public GameObject inform3GoodEnd;

    [Header("Кнопки действий")]
    public Button buttonHero;
    public Button buttonInfo;
    public Button buttonTavern;
    public Button buttonChat;
    public Button buttonAle;
    public Button buttonGet;
    public Button backButton;

    private Level1Controller levelController;
    private int currentScene = 1;

    void Start()
    {
        levelController = FindAnyObjectByType<Level1Controller>();
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

        // Активируем все сцены и оставляем их активными
        scene1.SetActive(true);
        scene2.SetActive(true);
        scene3.SetActive(true);

        // Настраиваем начальное состояние всех объектов
        ResetAllScenes();

        // Обновляем UI для первой сцены
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
        inform2Norm.SetActive(false);
        inform2BadEnd.SetActive(false);
        ale2.SetActive(false);
        askMap2.SetActive(false); // добавляем
        askMap2.SetActive(false); // добавляем

        // Сцена 3
        tavern3.SetActive(false);
        mainHero3.SetActive(false);
        inform3.SetActive(false);
        askMap3.SetActive(false); // добавляем
        inform3GoodEnd.SetActive(false);
    }

    // Обновление интерфейса в зависимости от текущей сцены
    void UpdateUI()
    {
        // Просто обновляем UI для текущей сцены
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
        bool informantVisible = inform1.activeSelf;
        bool tavernVisible = tavern1.activeSelf;
        bool chatVisible = chatChest1.activeSelf;

        buttonTavern.interactable = !tavernVisible;
        buttonInfo.interactable = tavernVisible && !informantVisible;
        buttonHero.interactable = tavernVisible && !heroVisible;
        buttonChat.interactable = heroVisible && informantVisible && tavernVisible && !chatVisible;
        buttonAle.interactable = false;
        buttonGet.interactable = false;

        // Делаем кнопки навигации всегда доступными
        // backButton.interactable = currentScene > 1;
    }

    void UpdateScene2UI()
    {
        bool heroVisible = mainHero2.activeSelf;
        bool informantVisible = inform2.activeSelf || inform2Norm.activeSelf || inform2BadEnd.activeSelf;
        bool tavernVisible = tavern2.activeSelf;
        bool aleVisible = ale2.activeSelf;
        bool getVisible = askMap2.activeSelf;

        buttonTavern.interactable = !tavernVisible;
        buttonInfo.interactable = tavernVisible && !informantVisible;
        buttonHero.interactable = tavernVisible && !heroVisible;
        buttonChat.interactable = false;
        buttonAle.interactable = heroVisible && informantVisible && tavernVisible && !aleVisible && !getVisible;
        buttonGet.interactable = heroVisible && informantVisible && tavernVisible && !getVisible && !aleVisible;

        // Делаем кнопки навигации всегда доступными
        //backButton.interactable = currentScene > 1;
    }

    void UpdateScene3UI()
    {
        bool heroVisible = mainHero3.activeSelf;
        bool informantVisible = inform3.activeSelf || inform3GoodEnd.activeSelf;
        bool tavernVisible = tavern3.activeSelf;
        bool getVisible = askMap3.activeSelf;

        buttonTavern.interactable = !tavernVisible;
        buttonInfo.interactable = tavernVisible && !informantVisible;
        buttonHero.interactable = tavernVisible && !heroVisible;
        buttonChat.interactable = false;
        buttonAle.interactable = false;
        buttonGet.interactable = heroVisible && informantVisible && tavernVisible && !getVisible;

        // Делаем кнопки навигации всегда доступными
        //backButton.interactable = currentScene > 1;
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
                // В сцене 2 показываем соответствующий спрайт информатора
                if (levelController.DiscussedRumors && levelController.GaveAle)
                {
                    // Если напоили элем - показываем довольного информатора
                    inform2Norm.SetActive(true);
                }
                else if (levelController.DiscussedRumors)
                {
                    // Если только обсудили слухи - показываем обычного информатора
                    inform2.SetActive(true);
                }
                else
                {
                    // Если ничего не сделали - показываем обычного информатора
                    inform2.SetActive(true);
                }
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

            // Меняем спрайт информатора на "напоенного"
            inform2.SetActive(false);
            inform2Norm.SetActive(true);

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
        if (currentScene == 2 && mainHero2.activeSelf && (inform2.activeSelf || inform2Norm.activeSelf) && tavern2.activeSelf)
        {
            // Активируем объект askMap2 для визуального отображения
            askMap2.SetActive(true);

            // Плохая концовка - просим карту без эля
            if (!ale2.activeSelf)
            {
                Debug.Log("Плохая концовка: Вы получили фальшивую карту!");

                // Меняем спрайт на плохую концовку
                inform2.SetActive(false);
                inform2Norm.SetActive(false);
                inform2BadEnd.SetActive(true);

                PlayerPrefs.SetInt("Level1Good", 0);

                // Показываем результат перед переходом
                StartCoroutine(ShowBadEndingAndTransition());
            }
            else
            {
                // Хорошая концовка
                Debug.Log("Хорошая концовка: Вы получили настоящую карту!");
                PlayerPrefs.SetInt("Level1Good", 1);

                // Показываем результат перед переходом
                StartCoroutine(ShowGoodEndingAndTransition());
            }
            UpdateUI();
        }
        else if (currentScene == 3 && mainHero3.activeSelf && inform3.activeSelf && tavern3.activeSelf)
        {
            // Активируем объект askMap3 для визуального отображения
            askMap3.SetActive(true);

            // Альтернативный путь - через сцену 3
            // Проверяем, был ли подан эль через Level1Controller
            if (levelController.GaveAle)
            {
                Debug.Log("Хорошая концовка: Вы получили настоящую карту!");
                PlayerPrefs.SetInt("Level1Good", 1);
                StartCoroutine(ShowGoodEndingAndTransition());
            }
            else
            {
                Debug.Log("Плохая концовка: Вы получили фальшивую карту!");
                PlayerPrefs.SetInt("Level1Good", 0);
                StartCoroutine(ShowBadEndingAndTransition());
            }
            UpdateUI();
        }
        else
        {
            Debug.Log("Для запроса карты нужно: герой, информатор и таверна на сцене!");
        }
    }

    // Корутина для показа плохой концовки с задержкой
    private IEnumerator ShowBadEndingAndTransition()
    {
        // Активируем спрайт плохой концовки
        if (inform2BadEnd != null)
        {
            inform2.SetActive(false); // Скрываем обычного информатора
            inform2BadEnd.SetActive(true); // Показываем информатора для плохой концовки
        }

        Debug.Log("Герой получает фальшивую карту...");

        // Ждем 2 секунды перед переходом
        yield return new WaitForSeconds(2f);

        // Показываем анимацию или сообщение о том, что героя съела акула

        Debug.Log("Героя съедает акула!");

        badEnd.SetActive(true);
        
    }

    // Корутина для показа хорошей концовки с задержкой
    private IEnumerator ShowGoodEndingAndTransition()
    {
        if (inform3GoodEnd != null)
        {
            inform3.SetActive(false);
            inform3GoodEnd.SetActive(true);
        }
        // Здесь можно добавить визуальные эффекты для хорошей концовки
        // Например, изменить спрайты, показать сообщение и т.д.

        Debug.Log("Герой получает настоящую карту!");

        // Ждем 2 секунды перед переходом
        yield return new WaitForSeconds(2f);

        // Переходим к выбору уровней
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }

    // === АВТОМАТИЧЕСКИЕ ПЕРЕХОДЫ МЕЖДУ СЦЕНАМИ ===

    private IEnumerator AutoTransitionToScene2()
    {
        Debug.Log("Автоматический переход к сцене 2 через 2 секунды...");

        // Можно добавить анимацию или эффект перехода
        yield return new WaitForSeconds(2f);

        TransitionToScene2();

        // Показываем подсказку для игрока
        Debug.Log("Теперь вы можете подать эль информатору или сразу попросить карту");
    }

    private IEnumerator AutoTransitionToScene3()
    {
        Debug.Log("Автоматический переход к сцене 3 через 2 секунды...");

        // Можно добавить анимацию или эффект перехода
        yield return new WaitForSeconds(2f);

        TransitionToScene3();

        // Показываем подсказку для игрока
        Debug.Log("Теперь вы можете попросить карту у информатора");
    }

    private void TransitionToScene2()
    {
        currentScene = 2;
        // НЕ скрываем предыдущую сцену - scene1 остается активной
        UpdateUI();
        Debug.Log("Переход ко второй сцене: Можно подать эль или сразу попросить карту");
    }

    private void TransitionToScene3()
    {
        currentScene = 3;
        // НЕ скрываем предыдущие сцены - scene1 и scene2 остаются активными
        UpdateUI();
        Debug.Log("Переход к третьей сцене: Можно попросить карту");
    }

    private void TransitionToScene1()
    {
        currentScene = 1;
        UpdateUI();
        Debug.Log("Возврат к первой сцене");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("LevelSelect");
        badEnd.SetActive(false);
    }
}