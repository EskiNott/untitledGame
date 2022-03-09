using UnityEngine;
using UnityEngine.EventSystems;

public class OutlinePointerEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Outline outlineEffect;
    private void Start()
    {
        outlineEffect = this.GetComponent<Outline>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        outlineEffect.enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        outlineEffect.enabled = false;
    }
}
