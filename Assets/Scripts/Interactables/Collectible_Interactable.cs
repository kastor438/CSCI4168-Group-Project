using UnityEngine;
using System.Collections;

public class Collectible_Interactable : Interactable
{
    public override void Interact()
    {
        base.Interact();
        Destroy(gameObject);
    }
}
