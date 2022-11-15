using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerInput playerInput { get; private set; }
    public CharacterClass characterClass;
    public UserInterface userInterface;
    public InventoryManager inventoryManager { get; private set; }
    public EquipmentManager equipmentManager { get; private set; }
    public GameObject player;

    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(Instance);

        inventoryManager = GetComponent<InventoryManager>();
        equipmentManager = GetComponent<EquipmentManager>();
        playerInput = GetComponent<PlayerInput>();
    }

    internal void DestroyGameManager()
    {
        Instance = null;
        Destroy(gameObject);
    }

    public void SetupCharacterStats(CharacterClass characterClass)
    {
        this.characterClass = characterClass;
        player.GetComponent<PlayerStats>().SetCharacterStats(characterClass);
        userInterface.inGameUICanvas.UISetup();
        if (MenuManager.Instance)
        {
            Destroy(MenuManager.Instance.gameObject);
        }
    }
}