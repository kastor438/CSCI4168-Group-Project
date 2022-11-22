using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private Animator breakableAnimator;
    public Item[] droppableItems;

    public void Start()
    {
        breakableAnimator = GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FriendlyProjectile"))
        {
            StartCoroutine(BreakItBro());
        }
    }

    public IEnumerator BreakItBro()
    {
        breakableAnimator.SetTrigger("Break");
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
