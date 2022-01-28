using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int Damage { get; set; }
    public double CriticalChance { get; set; }
    public ushort Slot { get; set; }
    public WeaponType wpType { get; set; }
    public Weapon(int id, string name, string description, int capacity, ItemType type, int buyprice, string sprite, int damage, double criticalchance, ushort slot, WeaponType wptype)
        : base(id, name, description, capacity, type, buyprice, sprite)
    {
        Damage = damage;
        CriticalChance = criticalchance;
        Slot = slot;
        wpType = wptype;
    }
    public enum WeaponType
    {
        Dagger,
        Axe,
        Sword
    }
}