using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{
    public int id;
    public string Name;
    public string prefabName;
    public uint amount;
    public enum resourceType{food,drink,medicine,equipment,gun,meeleWeapon,questItem,tool}
    public resourceType type;

}
