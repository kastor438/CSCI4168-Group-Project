using UnityEngine;
using System.Collections;

public class Weapon : Equipment
{
    public int damage;
    public float attackSpeed;
    public bool hasSpecialAttack;
    public bool isTwoHanded;
    public DamageType damageType;
}

public enum DamageType { Piercing, Blunt, Fire, Cold }