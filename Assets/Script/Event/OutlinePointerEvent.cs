using UnityEngine;
using UnityEngine.EventSystems;

public class OutlinePointerEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Outline outlineEffect;
    GameObject globalManager;
    public bool forceOn;
    private void Start()
    {
        outlineEffect = GetComponent<Outline>();
        globalManager = GameObject.Find("GlobalManager");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!globalManager.GetComponent<GlobalManager>().isInvestigate || forceOn)
        {
            outlineEffect.enabled = true;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        outlineEffect.enabled = false;
    }
}
