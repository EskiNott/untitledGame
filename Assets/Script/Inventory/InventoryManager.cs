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
        foreach(JSONObject temp in j.list)
        {
            int ID = temp["id"].intValue;
            string Name = temp["name"].ToString();
            string Description = temp["description"].ToString();
            int Capacity = temp["capacity"].intValue;
            int buyPrice = temp["buyprice"].intValue;
            string Sprite = temp["sprite"].ToString();
            Item.ItemType tempType = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), temp["type"].ToString());
            Item item = null;
            switch (tempType)
            {
                case Item.ItemType.Weapon:
                    int Damage = temp["damage"].intValue;
                    double CriticalChance = temp["criticalchance"].doubleValue;
                    ushort Slot = (ushort)temp["slot"].intValue;
                    Weapon.WeaponType weaponTempType = (Weapon.WeaponType)System.Enum.Parse(typeof(Weapon.WeaponType), temp["gtype"].ToString());
                    item = new Weapon
                        (ID, Name, Description, Capacity, tempType, buyPrice, Sprite, Damage, CriticalChance, Slot, weaponTempType);
                    break;
                case Item.ItemType.Consumable:
                    ushort Effect = (ushort)temp["effect"].intValue;
                    item = new Consumable
                        (ID, Name, Description, Capacity, tempType, buyPrice, Sprite, Effect);
                    break;
                case Item.ItemType.Gems:
                    Gems.GemType gemTempType = (Gems.GemType)System.Enum.Parse(typeof(Gems.GemType),temp["gtype"].ToString());
                    break;
                case Item.ItemType.Materials:
                    break;
            }
        }
    }
}
