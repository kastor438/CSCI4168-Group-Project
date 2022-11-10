using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public EffectedStat effectedStat;
    public float effectValue;
    /// <summary>
    /// The length of time the effect occurs. Leave at 0 if the effect is permanent.
    /// </summary>
    public float effectTime;
}

public enum EffectedStat { Health, Oxygen, Speed, Damage }