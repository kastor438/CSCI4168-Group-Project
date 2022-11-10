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

    public void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.playerInput || !GameManager.Instance.player ||
            !GameManager.Instance.playerInput.currentActionMap.name.Equals("InGamePlayer"))
            return;

        if (GameManager.Instance.playerInput.actions["RefuelOxygen"].WasPerformedThisFrame())
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots != null && inventorySlots[i].item && inventorySlots[i].item.itemType == ItemType.Consumable)
                {
                    Consumable consumable = (Consumable)inventorySlots[i].item;
                    if (consumable.effectedStat == EffectedStat.Oxygen)
                    {
                        int usableOxygen = (int)Mathf.Clamp(consumable.effectValue * inventorySlots[i].itemQuantity, 1, 100 - (int)GameManager.Instance.player.GetComponent<PlayerStats>().oxygenLevel);
                        GameManager.Instance.player.GetComponent<PlayerStats>().OxygenEffect(usableOxygen);
                        inventorySlots[i].itemQuantity -= usableOxygen;
                        if (inventorySlots[i].itemQuantity <= 0)
                        {
                            inventorySlots[i].item = null;
                        }
                        inventorySlots[i].DisplayItemInfo();
                        if (GameManager.Instance.player.GetComponent<PlayerStats>().oxygenLevel >= 100)
                        {
                            break;
                        }
                    }
                }
            }
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
