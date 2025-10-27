using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    void Awake()
    {
        Instance = this;
    }
    
    public void SetActionInteractable(string actionName, bool interactable)
    {
        // Здесь реализуйте логику активации/деактивации кнопок действий
        // Например: 
        // GameObject button = GameObject.Find(actionName + "Button");
        // if (button != null) button.GetComponent<Button>().interactable = interactable;
        
        Debug.Log($"Action {actionName} interactable: {interactable}");
    }
}