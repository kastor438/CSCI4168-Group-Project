using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private List<EquipmentSlot> equipmentSlots;

    public void Start()
    {
        equipmentSlots = new List<EquipmentSlot>();
        equipmentSlots.AddRange(GameManager.Instance.userInterface.inventoryCanvas.GetComponentsInChildren<EquipmentSlot>());
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            equipmentSlots[i].SlotSetupStart();
        }
    }

    public void EquipItem(Equipment newEquipment)
    {
        Debug.Log($"Equipping {newEquipment.itemName}");
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            if (newEquipment.equipmentType == equipmentSlots[i].slotEquipmentType)
            {
                if (newEquipment.equipmentType != EquipmentType.Weapon)
                {
                    if (equipmentSlots[i].equipment != null)
                    {
                        Equipment oldEquipment = equipmentSlots[i].equipment;
                        GameManager.Instance.inventoryManager.AddItem(oldEquipment, 1);
                    }
                    equipmentSlots[i].EquipItem(newEquipment);
                }
                else
                {
                    Weapon newWeapon = (Weapon)newEquipment;
                    if (!newWeapon.isTwoHanded)
                    {
                        if (equipmentSlots[i].equipment != null)
                        {
                            Equipment oldEquipment = equipmentSlots[i].equipment;
                            GameManager.Instance.inventoryManager.AddItem(oldEquipment, 1);
                            Destroy(GameManager.Instance.player.GetComponentInChildren<PlayerWeaponController>().transform.GetChild(0).gameObject);
                        }
                        equipmentSlots[i].EquipItem(newEquipment);
                        GameManager.Instance.player.GetComponentInChildren<PlayerWeaponController>().SetWeapon(newWeapon);
                    }
                    else
                    {
                        for (int j = i + 1; j < equipmentSlots.Count; j++)
                        {
                            if (equipmentSlots[j].slotEquipmentType == EquipmentType.Weapon)
                            {
                                if (equipmentSlots[i].equipment != null)
                                {
                                    Equipment oldEquipment = equipmentSlots[i].equipment;
                                    GameManager.Instance.inventoryManager.AddItem(oldEquipment, 1);
                                    Destroy(GameManager.Instance.player.GetComponentInChildren<PlayerWeaponController>().transform.GetChild(0).gameObject);
                                }
                                if (equipmentSlots[j].equipment != null)
                                {
                                    Equipment oldEquipment = equipmentSlots[j].equipment;
                                    GameManager.Instance.inventoryManager.AddItem(oldEquipment, 1);
                                }
                                equipmentSlots[i].EquipItem(newEquipment);
                                /*equipmentSlots[j].usable = false;*/
                                break;
                            }
                        }
                    }
                }
                break;
            }
        }
    }
}
