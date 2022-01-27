using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : Item
{
    public GemType gType { get; set; }
    public ushort Skill { get; set; }
    public Gems(int id, string name, string description, int capacity, ItemType type, double buyprice, string sprite, GemType gtype, ushort skill)
        : base(id, name, description, capacity, type, buyprice, sprite)
    {
        gType = gtype;
        Skill = skill;
    }
    public enum GemType
    {
        Head,
        Dagger,
        Axe,
        Sword,
        Helper
    }
}