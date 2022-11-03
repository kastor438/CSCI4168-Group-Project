using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerUpHandler
{
    public Item item;
    public int itemQuantity;

    public Image itemImage;
    public TextMeshProUGUI quantityText;
    public Button itemButton;

    public void AddItem(Item item)
    {
        this.item = item;
        itemQuantity++;
        DisplayItemInfo();
    }

    public void AddItem(Item item, int addedQuantity)
    {
        this.item = item;
        itemQuantity += addedQuantity;
    }

    public void DisplayItemInfo()
    {
        if (item)
        {
            itemImage.sprite = item.inventorySprite;
            quantityText.text = itemQuantity.ToString();
            itemButton.interactable = true;
        }
        else
        {
            itemImage.sprite = null;
            quantityText.text = "";
            itemButton.interactable = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(eventData.selectedObject.gameObject.name);
        if (!GameManager.Instance || !GameManager.Instance.playerInput ||
            !GameManager.Instance.playerInput.currentActionMap.name.Equals("InGamePlayer"))
            return;

        if(GameManager.Instance.playerInput.actions["Ctrl"].IsPressed() && GameManager.Instance.playerInput.actions["LeftClick"].WasPerformedThisFrame())
        {
            Debug.Log($"Attempting Use of item.");
        }
    }

    public void UseItem()
    {
        if(item != null)
        {
            if(item.GetType() == typeof(Equipment))
            {

            }
        }
    }
}