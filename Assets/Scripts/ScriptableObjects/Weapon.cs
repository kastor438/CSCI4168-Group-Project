﻿using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/Equipment/Weapon")]
public class Weapon : Equipment
{
    public float damage;
    public DamageType damageType;
}

public enum DamageType { Piercing, Blunt, Fire, Cold }