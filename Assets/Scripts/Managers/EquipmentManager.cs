using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private List<EquipmentSlot> equipmentSlots;

    public void EquipItem(Equipment equipment)
    {
        Debug.Log($"Equipping {equipment.itemName}");
    }
}
