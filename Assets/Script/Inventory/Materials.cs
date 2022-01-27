using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : Item
{
    public MaterialType mType;
    public Materials(int id, string name, string description, int capacity, ItemType type, double buyprice, string sprite, MaterialType mtype)
        : base(id, name, description, capacity, type, buyprice, sprite)
    {
        mType = mtype;
    }
    public enum MaterialType
    {
        Herb,
        Useless
    }
}
