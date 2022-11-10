using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public Enemy enemy;
    internal string enemyName;

    public override void Start()
    {
        base.Start();
        maxHealth = enemy.maxHealth;
        currHealth = maxHealth; 
    }

    public override IEnumerator GotHit()
    {
        yield return new WaitForSeconds(0);
    }

    public override IEnumerator Death()
    {
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }

    internal void SetCharacterStats(string characterName, int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currHealth = maxHealth;
    }
}
