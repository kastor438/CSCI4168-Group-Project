using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : AttackerEnemy
{
    Vector2 currPosition;
    // Start is called before the first frame update
    public override void Start()
    {
        collisionDamage = 10;
        attackRange = 1;
        base.Start();
    }

    // Update is called once per frame

    public override void Attack() {
        currPosition = this.transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(this.transform.position, currPosition, speed * Time.deltaTime);

    }
}
