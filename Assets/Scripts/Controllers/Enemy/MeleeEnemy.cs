using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : AttackerEnemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        attackRange = 1;
    }

    // Update is called once per frame

    public override void Attack() {
        
    }
}
