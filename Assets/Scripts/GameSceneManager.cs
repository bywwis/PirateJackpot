using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameSceneManager : MonoBehaviour, IPointerClickHandler
{
    public static GameSceneManager Instance;
    
    [Header("Scene Slots")]
    public Transform backgroundSlot;
    public Transform characterSlotsParent;
    public Transform itemSlotsParent;
    
    [Header("Prefabs")]
    public GameObject characterPrefab;
    public GameObject itemPrefab;
    public GameObject backgroundPrefab;
    
    [Header("Sprites")]
    public Sprite tavernBackground;
    public Sprite heroSprite;
    public Sprite informantSprite;
    public Sprite aleSprite;
    
    private ItemButtonController currentlySelectedButton;
    private string currentItemType;
    private string currentItemId;
    
    void Awake()
    {
        Instance = this;
    }
    
    // Вызывается при выборе предмета из нижней панели
    public void OnItemSelected(string itemType, string itemId, ItemButtonController button)
    {
        // Если уже был выбран другой предмет - отменяем его выбор
        if (currentlySelectedButton != null && currentlySelectedButton != button)
        {
            currentlySelectedButton.ForceDeselect();
        }
        
        currentlySelectedButton = button;
        currentItemType = itemType;
        currentItemId = itemId;
        
        Debug.Log($"Selected: {itemType} - {itemId}");
    }
    
    // Вызывается при отмене выбора предмета
    public void OnItemDeselected()
    {
        currentlySelectedButton = null;
        currentItemType = null;
        currentItemId = null;
    }
    
    // Обработка клика по сцене для размещения предмета
    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentlySelectedButton == null) return;
        
        // Размещаем предмет на сцене
        PlaceItemOnScene(eventData.position);
        
        // После размещения отменяем выбор
        currentlySelectedButton.ForceDeselect();
        OnItemDeselected();
    }
    
    private void PlaceItemOnScene(Vector2 screenPosition)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            backgroundSlot.GetComponent<RectTransform>(), 
            screenPosition, 
            null, 
            out localPoint);
        
        GameObject newItem = null;
        Transform parentSlot = null;
        
        switch (currentItemType)
        {
            case "Background":
                // Фон можно разместить только один раз
                if (backgroundSlot.childCount == 0)
                {
                    newItem = Instantiate(backgroundPrefab, backgroundSlot);
                    newItem.GetComponent<Image>().sprite = GetBackgroundSprite(currentItemId);
                }
                break;
                
            case "Character":
                parentSlot = GetAvailableCharacterSlot();
                if (parentSlot != null && !IsCharacterAlreadyPlaced(currentItemId))
                {
                    newItem = Instantiate(characterPrefab, parentSlot);
                    newItem.GetComponent<Image>().sprite = GetCharacterSprite(currentItemId);
                    newItem.name = currentItemId;
                }
                break;
                
            case "Item":
                parentSlot = GetAvailableItemSlot();
                if (parentSlot != null && !IsItemAlreadyPlaced(currentItemId))
                {
                    newItem = Instantiate(itemPrefab, parentSlot);
                    newItem.GetComponent<Image>().sprite = GetItemSprite(currentItemId);
                    newItem.name = currentItemId;
                }
                break;
        }
        
        if (newItem != null)
        {
            // Настраиваем позицию
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = localPoint;
            rectTransform.localScale = Vector3.one;
            
            // Добавляем функционал перетаскивания, если нужно
            // AddDragComponent(newItem);
            
            Debug.Log($"Placed {currentItemId} on scene");
            
            // Проверяем условия уровня после размещения предмета
            LevelManager.Instance.CheckSceneState();
        }
    }
    
    // Вспомогательные методы
    private Transform GetAvailableCharacterSlot()
    {
        foreach (Transform slot in characterSlotsParent)
        {
            if (slot.childCount == 0) return slot;
        }
        return null;
    }
    
    private Transform GetAvailableItemSlot()
    {
        foreach (Transform slot in itemSlotsParent)
        {
            if (slot.childCount == 0) return slot;
        }
        return null;
    }
    
    public bool IsCharacterAlreadyPlaced(string characterId)
    {
        foreach (Transform slot in characterSlotsParent)
        {
            if (slot.childCount > 0 && slot.GetChild(0).name == characterId)
                return true;
        }
        return false;
    }
    
    public bool IsItemAlreadyPlaced(string itemId)
    {
        foreach (Transform slot in itemSlotsParent)
        {
            if (slot.childCount > 0 && slot.GetChild(0).name == itemId)
                return true;
        }
        return false;
    }
    
    // Методы для получения спрайтов (настройте в инспекторе)
    private Sprite GetBackgroundSprite(string id)
    {
        switch (id)
        {
            case "Tavern":
                return tavernBackground;
            default:
                return null; // Всегда возвращайте значение
        }
    }
    
    private Sprite GetCharacterSprite(string id)
    {
        switch (id)
        {
            case "Hero":
                return heroSprite;
            case "Informant":
                return informantSprite;
            default:
                return null; // Всегда возвращайте значение
        }
    }
    
    private Sprite GetItemSprite(string id)
    {
        switch (id)
        {
            case "Ale":
                return aleSprite;
            default:
                return null; // Всегда возвращайте значение
        }
    }
}