using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewArmour", menuName = "Item/Equipment/Armour")]
public class Armour : Equipment
{
    public float defense;
    public ArmourType armourType;
}

public enum ArmourType { Headgear, Torso, Legs, Boots }