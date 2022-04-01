using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource:MonoBehaviour
{
    public uint id;
    public string Name;
    public enum resourceType{food,drink,medicine,equipment,gun,meeleWeapon,questItem}
    public resourceType type;
}
