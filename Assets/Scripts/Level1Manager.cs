using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    // Флаги состояния сцены
    public bool IsBackgroundSet { get; private set; }
    public bool IsHeroPlaced { get; private set; }
    public bool IsInformantPlaced { get; private set; }
    public bool IsAlePlaced { get; private set; }
    
    // ДОБАВЬТЕ ЭТИ ПРОПУЩЕННЫЕ ФЛАГИ:
    public bool AreRumorsDiscussed { get; private set; }
    public bool IsMapAsked { get; private set; }
    
    void Awake()
    {
        Instance = this;
    }
    
    // Вызывается после каждого изменения сцены
    public void CheckSceneState()
    {
        // Теперь методы доступны, так как они публичные в SceneManager
        IsBackgroundSet = GameSceneManager.Instance.backgroundSlot.childCount > 0;
        IsHeroPlaced = GameSceneManager.Instance.IsCharacterAlreadyPlaced("Hero");
        IsInformantPlaced = GameSceneManager.Instance.IsCharacterAlreadyPlaced("Informant");
        IsAlePlaced = GameSceneManager.Instance.IsItemAlreadyPlaced("Ale");
        
        // Активируем/деактивируем кнопки действий в зависимости от состояния
        UpdateActionButtons();
    }
    
    private void UpdateActionButtons()
    {
        // "Обсудить слухи" доступно только когда есть герой и информатор
        bool canDiscussRumors = IsHeroPlaced && IsInformantPlaced;
        
        // ИСПРАВЬТЕ ОПЕЧАТКУ: UlManager -> UIManager
        UIManager.Instance.SetActionInteractable("DiscussRumors", canDiscussRumors);
    }
    
    // Добавьте методы для установки флагов действий
    public void SetRumorsDiscussed()
    {
        AreRumorsDiscussed = true;
        UpdateActionButtons();
    }
    
    public void SetMapAsked()
    {
        IsMapAsked = true;
        CheckEndingConditions();
    }
    
    // Методы для проверки условий концовок
    public bool CheckGoodEndingConditions()
    {
        return IsAlePlaced && AreRumorsDiscussed && IsMapAsked;
    }
    
    public bool CheckBadEndingConditions()
    {
        return !IsAlePlaced && AreRumorsDiscussed && IsMapAsked;
    }
    
    private void CheckEndingConditions()
    {
        if (CheckGoodEndingConditions())
        {
            // Запуск хорошей концовки
            Debug.Log("Хорошая концовка! Карта получена.");
        }
        else if (CheckBadEndingConditions())
        {
            // Запуск плохой концовки
            Debug.Log("Плохая концовка! Фальшивая карта.");
        }
    }
}