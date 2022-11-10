using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvas : MonoBehaviour
{
    public GameObject itemPopup;
    public Image popupItemImage;
    public TextMeshProUGUI popupItemName;

    public void CloseInventoryOnClick()
    {
        if (!GameManager.Instance.userInterface.pauseMenuCanvas.gameObject.activeSelf)
        {
            GameManager.Instance.playerInput.SwitchCurrentActionMap("InGamePlayer");
            gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (GameManager.Instance && GameManager.Instance.playerInput && 
            GameManager.Instance.playerInput.currentActionMap.name.Equals("InventoryOpen") && 
            GameManager.Instance.inventoryManager.movingItem)
        {
            CanvasScaler scaler = GetComponentInParent<CanvasScaler>();
            Vector2 mousePos = GameManager.Instance.playerInput.actions["MousePosition"].ReadValue<Vector2>();
            if (mousePos.x > Screen.width * 0.66f)
            {
                itemPopup.GetComponent<RectTransform>().anchoredPosition = new Vector2((mousePos.x * scaler.referenceResolution.x / Screen.width) - itemPopup.GetComponent<RectTransform>().sizeDelta.x*0.8f, (mousePos.y * scaler.referenceResolution.y / Screen.height));
            }
            else
            {
                itemPopup.GetComponent<RectTransform>().anchoredPosition = new Vector2((mousePos.x * scaler.referenceResolution.x / Screen.width) + itemPopup.GetComponent<RectTransform>().sizeDelta.x * 0.45f, (mousePos.y * scaler.referenceResolution.y / Screen.height));
            }
        }
    }

    public void DisplayDragItemPopup(Item item, int quantity)
    {
        popupItemImage.sprite = item.inventorySprite;
        popupItemName.text = item.itemName;
        itemPopup.SetActive(true);
    }

    public void HideDragItemPopup()
    {
        popupItemImage.sprite = null;
        popupItemName.text = "";
        itemPopup.SetActive(false);
    }
}