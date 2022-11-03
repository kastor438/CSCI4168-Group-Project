using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<InventorySlot> inventorySlots;

    public void AddItem(Item newItem, int addedQuantity)
    {
        if (addedQuantity <= 0)
        {
            Debug.Log("Error: item had quantity set to zero or less.");
            return;
        }

        int originalQuantity = addedQuantity;

        // Check for stackableSlot
        if (newItem.stackable)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].item == newItem && inventorySlots[i].itemQuantity < newItem.stackableAmount)
                {
                    int amountToAdd = Mathf.Min(addedQuantity, newItem.stackableAmount - inventorySlots[i].itemQuantity);
                    inventorySlots[i].AddItem(newItem, amountToAdd);
                    addedQuantity -= amountToAdd;
                    if (addedQuantity <= 0)
                    {
                        Debug.Log($"Added {newItem.itemName}({originalQuantity}) to inventory slot {i}.");
                        return;
                    }
                }
            }
        }

        // Check for first open slot
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].item == null)
            {
                int amountToAdd = Mathf.Min(addedQuantity, newItem.stackableAmount);
                inventorySlots[i].AddItem(newItem, amountToAdd);
                addedQuantity -= amountToAdd;
                if(addedQuantity <= 0)
                {
                    Debug.Log($"Added {newItem.itemName}({originalQuantity}) to inventory slot {i}.");
                    return;
                }               
            }
        }
    }
}
