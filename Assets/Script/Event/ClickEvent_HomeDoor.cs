using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvent_HomeDoor : MonoBehaviour, IPointerClickHandler
{
    public GameObject HomeScene;
    public GameObject DoorScene;
    public GameObject MainMenu;
    public void OnPointerClick(PointerEventData eventData)
    {
        HomeScene.SetActive(true);
        DoorScene.SetActive(false);
        MainMenu.SetActive(false);
    }
}