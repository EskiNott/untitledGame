using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable: Item
{
    public int HealthPoint { get; set; }
    public int MagicPoint { get; set; }
    public bool[] SpecialEffect { get; set; }
    public Consumable(int id, string name, string description, int capacity, ItemType type, string sprite, int healthpoint, int magicpoint, bool[] speacialeffect)
    : base(id, name, description, capacity, type, sprite)
    {
        HealthPoint = healthpoint;
        MagicPoint = magicpoint;
        SpecialEffect = speacialeffect;
    }
}
