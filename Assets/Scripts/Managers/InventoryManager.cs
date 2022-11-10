using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    private List<InventorySlot> inventorySlots;
    internal bool movingItem;
    internal InventorySlot pickedUpSlot;

    public void Start()
    {
        inventorySlots = new List<InventorySlot>();
        inventorySlots.AddRange(GameManager.Instance.userInterface.inventoryCanvas.GetComponentsInChildren<InventorySlot>());
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].SlotSetupStart();
        }
    }

    public void SwitchSlots(InventorySlot from, InventorySlot to)
    {
        if (movingItem && from.item != null)
        {
            Item toItem = to.item;
            int toQuantity = to.itemQuantity;

            to.SwapItem(from.item, from.itemQuantity);
            from.SwapItem(toItem, toQuantity);
        }
    }

    public (bool, int) AddItem(Item newItem, int newQuantity)
    {
        if (newQuantity <= 0)
        {
            Debug.Log("Error: item had quantity set to zero or less.");
            return (false, 0);
        }

        int originalQuantity = newQuantity;

        // Check for stackableSlot
        if (newItem.stackable)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].item == newItem && inventorySlots[i].itemQuantity < newItem.stackableAmount)
                {
                    int amountToAdd = Mathf.Min(newQuantity, newItem.stackableAmount - inventorySlots[i].itemQuantity);
                    inventorySlots[i].AddItem(newItem, amountToAdd);
                    newQuantity -= amountToAdd;

                    Debug.Log($"Added {newItem.itemName}({amountToAdd}) to inventory slot {i}.");
                    if (newQuantity <= 0)
                        return (true, 0);
                }
            }
        }

        // Check for first open slot
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].item == null)
            {
                inventorySlots[i].AddItem(newItem, newQuantity);
                Debug.Log($"Added {newItem.itemName}({newQuantity}) to inventory slot {i}.");
                return (true, 0);
            }
        }

        return (false, newQuantity);
    }
}
