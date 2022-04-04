using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvent_HomeDoor : MonoBehaviour, IPointerClickHandler
{
    public GameObject HomeScene;
    public GameObject camManager;
    public GameObject MainMenu;
    public void OnPointerClick(PointerEventData eventData)
    {
        camManager.GetComponent<CameraManager>().setCamTrans(HomeScene);
        MainMenu.SetActive(false);
    }
}