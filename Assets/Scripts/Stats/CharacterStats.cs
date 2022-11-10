using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    public int maxHealth { get; internal set; }
    public int currHealth { get; internal set; }

    public virtual void Start()
    {
        maxHealth = 10;
        currHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currHealth = Mathf.Clamp(currHealth - damage, 0, maxHealth);
        Coroutine gotHitRoutine = StartCoroutine(GotHit());
        if (currHealth <= 0)
        {
            StopCoroutine(gotHitRoutine);
            StartCoroutine(Death());
        }
    }

    public abstract IEnumerator GotHit();

    public abstract IEnumerator Death();
}
