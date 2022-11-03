using UnityEngine;
using System.Collections;

public class Equipment : Item
{
    public float durability;
    public GameObject equipmentPrefab;
    public EquipmentType equipmentType;
}
public enum EquipmentType { Weapon, Headgear, Torso, Legs, Boots }