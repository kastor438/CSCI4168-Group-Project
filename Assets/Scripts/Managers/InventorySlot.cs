using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
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
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            quantityText.text = "";
            itemButton.interactable = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.Instance || !GameManager.Instance.playerInput || item == null ||
            !GameManager.Instance.playerInput.currentActionMap.name.Equals("InventoryOpen"))
            return;

        if (GameManager.Instance.playerInput.actions["Ctrl"].IsPressed() && GameManager.Instance.playerInput.actions["LeftClick"].WasPerformedThisFrame())
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if(item != null)
        {
            if(item.itemType == ItemType.Equipment)
            {
                GameManager.Instance.equipmentManager.EquipItem((Equipment)item);
                item = null;
                itemQuantity = 0;
                DisplayItemInfo();
            }
            else if(item.itemType == ItemType.Consumable)
            {
                
                itemQuantity--;
                if (itemQuantity <= 0)
                {
                    item = null;
                }
                DisplayItemInfo();
            }
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        /*if (GameManager.Instance.inventoryManager.movingItem)
        {
            GameManager.Instance.userInterface.inventoryCanvas.DisplayDragItemPopup();
        }*/
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.inventoryManager.movingItem = true;
        Debug.Log("Down: " + eventData.pointerCurrentRaycast.gameObject.transform.parent.name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up: " + eventData.pointerCurrentRaycast.gameObject.transform.parent.name);
        if (GameManager.Instance.inventoryManager.movingItem &&
            eventData.pointerCurrentRaycast.gameObject &&
            eventData.pointerCurrentRaycast.gameObject.transform.parent &&
            eventData.pointerCurrentRaycast.gameObject.transform.parent.TryGetComponent<InventorySlot>(out InventorySlot invSlot) &&
            this != invSlot)
        {
            GameManager.Instance.inventoryManager.SwitchSlots(this, invSlot);
        }
        GameManager.Instance.inventoryManager.movingItem = false;
    }
}