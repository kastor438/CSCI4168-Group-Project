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
            Debug.Log(scaler.referenceResolution);
            itemPopup.GetComponent<RectTransform>().anchoredPosition = new Vector2((mousePos.x * scaler.referenceResolution.x / Screen.width)-Screen.width*1f, (mousePos.y * scaler.referenceResolution.y / Screen.height) - Screen.height * 1.1f);
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