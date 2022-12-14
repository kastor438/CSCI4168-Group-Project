using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool movingItem;

    public Item item;
    public int itemQuantity;

    public Image itemImage;
    public TextMeshProUGUI quantityText;
    public Button itemButton;

    public void SlotSetupStart()
    {
        item = null;
        itemQuantity = 0;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        quantityText.text = "";
        itemButton.interactable = false;
    }

    public void AddItem(Item item, int addedQuantity)
    {
        if (this.item == null)
        {
            this.item = item;
        }
        itemQuantity += addedQuantity;
        DisplayItemInfo();
    }

    public void SwapItem(Item item, int quantity)
    {
        this.item = item;
        itemQuantity = quantity;
        DisplayItemInfo();
    }

    public void RemoveItem()
    {
        item = null;
        itemQuantity = 0;
        DisplayItemInfo();
    }

    public void DisplayItemInfo()
    {
        if (item)
        {
            itemImage.sprite = item.inventorySprite;
            itemImage.color = Color.white;
            quantityText.text = item.stackable ? itemQuantity.ToString() : "";
            itemButton.interactable = true;
        }
        else
        {
            itemQuantity = 0;
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            quantityText.text = "";
            itemButton.interactable = false;
        }
    }

    public void UseItem()
    {
        if(item != null)
        {
            if(item.itemType == ItemType.Equipment)
            {
                Equipment equipment = (Equipment)item;
                RemoveItem();
                GameManager.Instance.equipmentManager.EquipItem(equipment);
            }
            else if(item.itemType == ItemType.Consumable)
            {
                Consumable consumable = (Consumable)item;

                if (consumable.effectedStat == EffectedStat.Health)
                {
                    GameManager.Instance.player.GetComponent<PlayerStats>().HealthEffect(consumable.effectValue);
                    itemQuantity--;
                    if (itemQuantity <= 0)
                    {
                        item = null;
                    }
                    DisplayItemInfo();
                }
                else if (consumable.effectedStat == EffectedStat.Oxygen)
                {
                    int usableOxygen = (int)Mathf.Clamp(consumable.effectValue * itemQuantity, 1, 100 - (int)GameManager.Instance.player.GetComponent<PlayerStats>().oxygenLevel);
                    GameManager.Instance.player.GetComponent<PlayerStats>().OxygenEffect(usableOxygen);
                    itemQuantity -= usableOxygen;
                    if (itemQuantity <= 0)
                    {
                        item = null;
                    }
                    DisplayItemInfo();
                }
                else if (consumable.effectedStat == EffectedStat.Speed)
                {
                    GameManager.Instance.player.GetComponent<PlayerStats>().SpeedEffect(consumable.effectValue, consumable.effectTime);
                    itemQuantity--;
                    if (itemQuantity <= 0)
                    {
                        item = null;
                    }
                    DisplayItemInfo();
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.playerInput.actions["LeftClick"].WasPerformedThisFrame() && !GameManager.Instance.inventoryManager.movingItem && item)
        {
            GameManager.Instance.inventoryManager.movingItem = true;
            GameManager.Instance.inventoryManager.pickedUpSlot = this;
            GameManager.Instance.userInterface.inventoryCanvas.DisplayDragItemPopup(item, itemQuantity);
        }
        else if (GameManager.Instance.playerInput.actions["RightClick"].WasPerformedThisFrame() &&
             GameManager.Instance.inventoryManager.movingItem &&
             GameManager.Instance.inventoryManager.pickedUpSlot != null &&
             this != GameManager.Instance.inventoryManager.pickedUpSlot && (item == null ||
             (item == GameManager.Instance.inventoryManager.pickedUpSlot.item && itemQuantity < item.stackableAmount)))
        {
             item = GameManager.Instance.inventoryManager.pickedUpSlot.item;
             itemQuantity++;
             GameManager.Instance.inventoryManager.pickedUpSlot.itemQuantity--;

             if (GameManager.Instance.inventoryManager.pickedUpSlot.itemQuantity <= 0)
             {
                 GameManager.Instance.inventoryManager.pickedUpSlot.item = null;
                 GameManager.Instance.inventoryManager.pickedUpSlot.itemQuantity = 0;
                 
                 GameManager.Instance.inventoryManager.pickedUpSlot.DisplayItemInfo();

                GameManager.Instance.inventoryManager.movingItem = false;
                GameManager.Instance.inventoryManager.pickedUpSlot = null;
                GameManager.Instance.userInterface.inventoryCanvas.HideDragItemPopup();
            }
             else
             {
                 GameManager.Instance.inventoryManager.pickedUpSlot.DisplayItemInfo();
                 GameManager.Instance.userInterface.inventoryCanvas.DisplayDragItemPopup(item, itemQuantity);
             }
             DisplayItemInfo();
         }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameManager.Instance.playerInput.actions["LeftClick"].WasPerformedThisFrame())
        {
            if (GameManager.Instance.inventoryManager.movingItem &&
                eventData.pointerCurrentRaycast.gameObject &&
                eventData.pointerCurrentRaycast.gameObject.transform.parent &&
                eventData.pointerCurrentRaycast.gameObject.transform.parent.TryGetComponent(out InventorySlot invSlot))
            {
                if (invSlot != GameManager.Instance.inventoryManager.pickedUpSlot)
                {
                    if (GameManager.Instance.inventoryManager.pickedUpSlot.item == invSlot.item)
                    {
                        int amountToMove = Mathf.Min(GameManager.Instance.inventoryManager.pickedUpSlot.itemQuantity, item.stackableAmount - invSlot.itemQuantity);
                        if (amountToMove > 0)
                        {
                            invSlot.itemQuantity += amountToMove;
                            GameManager.Instance.inventoryManager.pickedUpSlot.itemQuantity -= amountToMove;
                            if (GameManager.Instance.inventoryManager.pickedUpSlot.itemQuantity <= 0)
                            {
                                GameManager.Instance.inventoryManager.pickedUpSlot.item = null;
                                GameManager.Instance.inventoryManager.pickedUpSlot.itemQuantity = 0;
                            }
                        }
                        else
                        {
                            GameManager.Instance.inventoryManager.SwitchSlots(GameManager.Instance.inventoryManager.pickedUpSlot, invSlot);
                        }
                    }
                    else
                    {
                        GameManager.Instance.inventoryManager.SwitchSlots(GameManager.Instance.inventoryManager.pickedUpSlot, invSlot);
                    }
                    GameManager.Instance.inventoryManager.pickedUpSlot.DisplayItemInfo();
                    invSlot.DisplayItemInfo();
                }
            }
            GameManager.Instance.inventoryManager.movingItem = false;
            GameManager.Instance.inventoryManager.pickedUpSlot = null;
            GameManager.Instance.userInterface.inventoryCanvas.HideDragItemPopup();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.Instance || !GameManager.Instance.playerInput || item == null ||
            !GameManager.Instance.playerInput.currentActionMap.name.Equals("InventoryOpen"))
            return;

        if (GameManager.Instance.playerInput.actions["Ctrl"].IsPressed() && GameManager.Instance.playerInput.actions["RightClick"].WasPerformedThisFrame())
        {
            UseItem();
        }
    }
}