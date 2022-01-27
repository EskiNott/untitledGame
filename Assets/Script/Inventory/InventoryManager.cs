using Defective.JSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _Instance;
    private List<Item> itemList;
    public static InventoryManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _Instance;
        }
    }
    private void ParseItemsJSON()
    {
        itemList = new List<Item>();
        TextAsset ta = Resources.Load<TextAsset>("items");
        JSONObject j = new JSONObject(ta.text);
    }
}
