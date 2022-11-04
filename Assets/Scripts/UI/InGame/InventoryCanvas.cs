using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvas : MonoBehaviour
{
    public GameObject dragItemPopup;
    public Image dragItemImage;
    public TextMeshProUGUI dragItemText;

    public void CloseInventoryOnClick()
    {
        if (!GameManager.Instance.userInterface.pauseMenuCanvas.gameObject.activeSelf)
        {
            GameManager.Instance.playerInput.SwitchCurrentActionMap("InGamePlayer");
            gameObject.SetActive(false);
        }
    }

    public void DisplayDragItemPopup(Item item, int quantity)
    {
        dragItemImage.sprite = item.inventorySprite;
        dragItemText.text = quantity.ToString();
        dragItemPopup.SetActive(true);
    }

    public void HideDragItemPopup()
    {
        dragItemImage.sprite = null;
        dragItemText.text = "";
        dragItemPopup.SetActive(false);
    }
}