using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable: Item
{
    public ushort Effects { get; set; }
    public Consumable
        (int id, string name, string description, int capacity, ItemType type, int buyprice, string sprite, ushort effects)
    : base(id, name, description, capacity, type, buyprice, sprite)
    {
        Effects = effects;
    }
}
