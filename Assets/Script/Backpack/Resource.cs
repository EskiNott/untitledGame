using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource:MonoBehaviour
{
    public uint id;
    public string Name;
    public string prefabName;
    public uint amount;
    public enum resourceType{food,drink,medicine,equipment,gun,meeleWeapon,questItem,tool}
    public resourceType type;

}
