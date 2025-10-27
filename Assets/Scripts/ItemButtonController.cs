using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ItemButtonController : MonoBehaviour, IPointerClickHandler
{
    [Header("Settings")]
    public string itemType; // "Background", "Character", "Item", "Action"
    public string itemId; // "Tavern", "Hero", "Informant", "Ale", "DiscussRumors", "AskMap"
    
    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite selectedSprite;
    
    private Image buttonImage;
    private bool isSelected = false;
    
    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSelected)
        {
            // Выбор предмета для размещения
            SelectItem();
        }
        else
        {
            // Отмена выбора
            DeselectItem();
        }
    }
    
    private void SelectItem()
    {
        isSelected = true;
        buttonImage.sprite = selectedSprite;
        
        // Сообщаем менеджеру сцены о выбранном предмете
        GameSceneManager.Instance.OnItemSelected(itemType, itemId, this);
    }
    
    private void DeselectItem()
    {
        isSelected = false;
        buttonImage.sprite = normalSprite;
        
        // Сообщаем менеджеру сцены об отмене выбора
        GameSceneManager.Instance.OnItemDeselected();
    }
    
    // Вызывается извне при размещении предмета или отмене
    public void ForceDeselect()
    {
        isSelected = false;
        buttonImage.sprite = normalSprite;
    }
}