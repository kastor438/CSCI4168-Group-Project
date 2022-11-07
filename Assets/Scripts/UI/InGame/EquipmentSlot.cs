using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    public Equipment equipment;
    public EquipmentType slotEquipmentType;

    public Image equipmentImage;
    public Button equipmentButton;

    public bool usable;

    public void SlotSetupStart()
    {
        usable = true;
        equipment = null;
        equipmentImage.sprite = null;
        equipmentImage.color = Color.clear;
        equipmentButton.interactable = false;
    }

    public void EquipItem(Equipment equipment)
    {
        if (usable)
        {
            this.equipment = equipment;
            DisplayEquipmentInfo();
        }
    }

    public void DisplayEquipmentInfo()
    {
        if (equipment)
        {
            equipmentImage.sprite = equipment.inventorySprite;
            equipmentImage.color = Color.white;
            equipmentButton.interactable = true;
        }
        else
        {
            equipmentImage.sprite = null;
            equipmentImage.color = Color.clear;
            equipmentButton.interactable = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.Instance || !GameManager.Instance.playerInput || equipment == null ||
            !GameManager.Instance.playerInput.currentActionMap.name.Equals("InventoryOpen"))
            return;

        if (GameManager.Instance.playerInput.actions["Ctrl"].IsPressed() && 
            GameManager.Instance.playerInput.actions["RightClick"].WasPerformedThisFrame() &&
            equipment != null && usable)
        {
            (bool, int) AddedRemainder = GameManager.Instance.inventoryManager.AddItem(equipment, 1);
            if (!AddedRemainder.Item1)
            {
                // NEEDS IMPLEMENTATION: Must instantiate and drop item on ground.
            }

            equipment = null;
            DisplayEquipmentInfo();
        }
    }
}