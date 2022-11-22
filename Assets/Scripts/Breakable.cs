using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private Animator breakableAnimator;
    private bool broken;

    public Item[] droppableItems;
    public int[] minItemQuantities;
    public int[] maxItemQuantities;
    public int minDropItems;
    public int maxDropItems;

    public void Start()
    {
        breakableAnimator = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!broken && collision.CompareTag("FriendlyProjectile"))
        {
            broken = true;
            DropItems();
            StartCoroutine(BreakItBro());
        }
    }

    public void DropItems()
    {
        List<Item> potentialItems = new List<Item>();
        potentialItems.AddRange(droppableItems);

        List<Item> itemsToDrop = new List<Item>();
        List<int> quantities = new List<int>();
        int numberOfDroppedItems = Random.Range(minDropItems, maxDropItems + 1);
        do
        {
            if (potentialItems.Count <= 0)
                break;

            int index = Random.Range(0, potentialItems.Count - 1);
            Item nextItem = potentialItems[index];
            if (!itemsToDrop.Contains(nextItem)){
                itemsToDrop.Add(nextItem);
                quantities.Add(Random.Range(minItemQuantities[index], maxItemQuantities[index] + 1));
                potentialItems.Remove(nextItem);
            }

        } while (itemsToDrop.Count < numberOfDroppedItems);

        for(int i = 0; i < itemsToDrop.Count; i++)
        {
            GameObject pickupItem = Instantiate(itemsToDrop[i].pickupPrefab, transform.position, Quaternion.identity);
            pickupItem.GetComponent<ItemPickup>().SetPickup(itemsToDrop[i], quantities[i]);
/*            pickupItem.GetComponent<Rigidbody2D>().AddForce
*/        }
    }

    public IEnumerator BreakItBro()
    {
        breakableAnimator.SetTrigger("Break");
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
