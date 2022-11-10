using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRangedWeapon", menuName = "Item/Equipment/Weapon/RangedWeapon")]
public class RangedWeapon : Weapon
{
    public float projectileSpeed;
    public float projectileLifetime;
    [Tooltip("The acceleration the gun pushes on the player when firing.")]
    public float recoilAcceleration;
    public GameObject projectilePrefab;
}
