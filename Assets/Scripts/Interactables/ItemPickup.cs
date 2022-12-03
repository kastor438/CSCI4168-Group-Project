using UnityEngine;
using System.Collections;

public class ItemPickup : Interactable
{
    private float collectionSpeed;
    private Rigidbody2D itemRB2D;
    private bool collisionDisabled;

    public Item item;
    public int quantity;

    public override void Start()
    {
        base.Start();
        if (!item.stackable)
            quantity = 1;
        collectionSpeed = 0.05f;
        itemRB2D = GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        base.Update();
        if (!collisionDisabled && GameManager.Instance && GameManager.Instance.player)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.player.GetComponent<Collider2D>());
            collisionDisabled = true;
        }
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(CollectItem());
    }

    public IEnumerator CollectItem()
    {
        int iteration = 1;
        while (Vector3.Distance(GameManager.Instance.player.transform.position, transform.position) > 0.3f)
        {
            Vector2 normalVector = Vector3.Normalize(GameManager.Instance.player.transform.position - transform.position);
            itemRB2D.velocity = normalVector * collectionSpeed * iteration;
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

    public IEnumerator BreakableLaunch()
    {
        int directionX = Random.Range(-1, 1);
        int directionY = Random.Range(-1, 1);
        itemRB2D.velocity = new Vector2((directionX == 0 ? 1 : -1) * Random.Range(0.8f, 3.0f), (directionY == 0 ? 1 : -1) * Random.Range(0.8f, 3.0f));
        while (itemRB2D.velocity.magnitude > 0.2f)
        {
            yield return new WaitForEndOfFrame();
            itemRB2D.velocity *= 0.96f;
        }
        itemRB2D.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
    }

    internal void SetPickup(Item item, int quantity)
    {
        itemRB2D = GetComponent<Rigidbody2D>();
        this.item = item;
        this.quantity = quantity;
        StartCoroutine(BreakableLaunch());
    }
}
