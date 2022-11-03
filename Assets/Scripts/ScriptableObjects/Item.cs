using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;
    public Sprite inventorySprite;
    public bool stackable;
    public int stackableAmount;
}
