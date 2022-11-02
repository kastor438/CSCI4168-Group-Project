using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override IEnumerator GotHit()
    {
        yield return new WaitForSeconds(0);
    }

    public override IEnumerator Death()
    {
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }
}
