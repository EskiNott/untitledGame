using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item item { get; set; }
    private TextMesh ItemText;
    private Image ItemImage;
    private void Awake()
    {
        ItemText = GetComponentInChildren<TextMesh>();
        ItemImage = GetComponent<Image>();
    }
    private void Update()
    {
        
    }
    private void Start()
    {
        
    }
    public void SetItem(Item item)
    {
        this.item = item;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
    }
    public void ExchangeItemUI(ItemUI itemUI)
    {

    }
}
