using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public ConsumableType consumableType;
}

public enum ConsumableType { Potion }
