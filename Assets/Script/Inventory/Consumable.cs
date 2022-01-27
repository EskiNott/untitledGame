using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable: Item
{
    public bool Effects { get; set; }
    public Consumable(int id, string name, string description, int capacity, ItemType type, double buyprice, string sprite, bool effects)
    : base(id, name, description, capacity, type, buyprice, sprite)
    {
        Effects = effects;
    }
}
