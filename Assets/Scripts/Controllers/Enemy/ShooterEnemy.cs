using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : AttackerEnemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        attackRange = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack() {
        Debug.Log("Ranged Attack");
    }
}
