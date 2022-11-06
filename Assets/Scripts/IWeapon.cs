using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public IEnumerator PerformAttack();
    public IEnumerator PerformSpecialAttack();
}
