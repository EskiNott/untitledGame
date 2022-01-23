using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public ItemType Type { get; set; }
    public string Sprite { get; set; }

    public Item() { }
    public Item(int id,string name,string description,int capacity,ItemType type,string sprite)
    {
        ID = id;
        Name = name;
        Description = description;
        Capacity = capacity;
        Type = type;
        Sprite = sprite;
    }
}

public enum ItemType
{
    Weapon,
    Consumable,
    Gems,
    Material
}