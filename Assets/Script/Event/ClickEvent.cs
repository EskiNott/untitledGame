using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour, IPointerClickHandler
{
    public Camera Camera_Home;
    public Camera Camera_Door;
    public GameObject MainMenu;
    public void OnPointerClick(PointerEventData eventData)
    {
        Camera_Home.enabled = true;
        Camera_Door.enabled = false;
        MainMenu.SetActive(false);
    }
}