using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int Damage { get; set; }
    public double CriticalChance { get; set; }
    public bool[] Skill { get; set; }
    public WeaponType wpType { get; set; }
    public Weapon(int id, string name, string description, int capacity, ItemType type, string sprite,int damage,double criticalchance,bool[] skill,WeaponType wptype)
        :base(id,name,description,capacity,type,sprite)
    {
        Damage = damage;
        CriticalChance = criticalchance;
        Skill = skill;
        wpType = wptype;
    }
    public enum WeaponType
    {
        Dagger,
        Axe,
        Sword
    }
}