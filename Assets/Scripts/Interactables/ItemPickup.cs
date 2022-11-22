using UnityEngine;
using System.Collections;
using System;

public class ItemPickup : Interactable
{
    private float collectionSpeed;
    private Rigidbody2D itemRB2D;

    public Item item;
    public int quantity;

    public void Start()
    {
        if (!item.stackable)
            quantity = 1;
        collectionSpeed = 0.001f;
        itemRB2D = GetComponent<Rigidbody2D>();
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(CollectItem());
    }

    public IEnumerator CollectItem()
    {
        int iteration = 1;
        while (Vector3.Distance(GameManager.Instance.player.transform.position, transform.position) > 1f)
        {
            Vector2 normalVector = Vector3.Normalize(GameManager.Instance.player.transform.position - transform.position);
            itemRB2D.velocity += normalVector * collectionSpeed * iteration;
            iteration++;
            yield return new WaitForEndOfFrame();
        }

        (bool, int) addedRemainder = GameManager.Instance.inventoryManager.AddItem(item, quantity);
        if (addedRemainder.Item2 <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            quantity = addedRemainder.Item2;
        }
    }

    internal void SetPickup(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
