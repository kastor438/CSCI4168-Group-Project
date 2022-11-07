using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRangedWeapon", menuName = "Item/Equipment/Weapon/RangedWeapon")]
public class RangedWeapon : Weapon
{
    public float projectileSpeed;
    public float projectileLifetime;
    public GameObject projectilePrefab;
}
