using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerEnemy : EnemyController
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        Patrol();
    }
}
