using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Equipment equipment;

    public Image equipmentImage;
    public Button equipmentButton;

    public void EquipItem(Equipment equipment)
    {
        this.equipment = equipment;
        DisplayEquipmentInfo();
    }

    public void DisplayEquipmentInfo()
    {
        if (equipment)
        {
            equipmentImage.sprite = equipment.inventorySprite;
            equipmentButton.interactable = true;
        }
        else
        {
            equipmentImage.sprite = null;
            equipmentButton.interactable = false;
        }
    }
}