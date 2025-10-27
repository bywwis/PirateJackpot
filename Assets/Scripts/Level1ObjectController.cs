using UnityEngine;

public class Level1ObjectController : MonoBehaviour
{
    // Ссылки на объекты в сцене
    public GameObject hero;
    public GameObject informant;
    public GameObject ale;
    public GameObject tavern;
    public GameObject chat;
    public GameObject get;

    // Методы для кнопок
    public void ShowHero()
    {
        hero.SetActive(true);
        Debug.Log("Герой появился на сцене");
    }

    public void ShowInformant()
    {
        informant.SetActive(true);
        Debug.Log("Информатор появился на сцене");
    }

    public void ShowAle()
    {
        if (informant.activeSelf)
        {
            ale.SetActive(true);
            Debug.Log("Эль поставлен на стол");
        }
        else
        {
            Debug.Log("Сначала нужно вызвать информатора!");
        }
    }
}
