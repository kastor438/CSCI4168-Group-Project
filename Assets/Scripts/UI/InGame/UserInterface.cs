using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    private bool activeDialog;

    // All Canvases will be here.
    public PauseMenuCanvas pauseMenuCanvas;
    public InventoryCanvas inventoryCanvas;
    public InGameUICanvas inGameUICanvas;
    public DialogCanvas dialogCanvas;

    public void Start()
    {
        pauseMenuCanvas.gameObject.SetActive(false);
        inventoryCanvas.gameObject.SetActive(false);
        dialogCanvas.gameObject.SetActive(false);
        inGameUICanvas.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.playerInput)
            return;

        if (GameManager.Instance.playerInput.currentActionMap.name.Equals("InGamePlayer"))
        {
            // Check to pause game
            if (GameManager.Instance.playerInput.actions["PauseGame"].WasPerformedThisFrame())
            {
                OpenPauseMenu();
            }

            // Check for inventory input
            if (GameManager.Instance.playerInput.actions["OpenInventory"].WasPerformedThisFrame())
            {
                OpenInventory();
            }
        }
        else if (GameManager.Instance.playerInput.currentActionMap.name.Equals("PausedGame"))
        {
            // Check to unpause game
            if (GameManager.Instance.playerInput.actions["UnpauseGame"].WasPerformedThisFrame())
            {
                ClosePauseMenu();
            }
        }
        else if (GameManager.Instance.playerInput.currentActionMap.name.Equals("InventoryOpen"))
        {
            // Check to pause game
            if (GameManager.Instance.playerInput.actions["PauseGame"].WasPerformedThisFrame())
            {
                OpenPauseMenu();
            }

            // Check for inventory input
            if (GameManager.Instance.playerInput.actions["CloseInventory"].WasPerformedThisFrame())
            {
                Debug.Log("Closing");
                CloseInventory();
            }
        }
        else if (GameManager.Instance.playerInput.currentActionMap.name.Equals("ActiveDialog"))
        {
            if (GameManager.Instance.playerInput.actions["PauseGame"].WasPerformedThisFrame())
            {
                activeDialog = true;
                OpenPauseMenu();
            }
        }
    }

    public void OpenPauseMenu()
    {
        GameManager.Instance.playerInput.currentActionMap.Disable();
        GameManager.Instance.playerInput.SwitchCurrentActionMap("PausedGame");
        Debug.Log("Huh?");
        pauseMenuCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        GameManager.Instance.playerInput.currentActionMap.Disable();
        if (activeDialog)
        {
            GameManager.Instance.playerInput.SwitchCurrentActionMap("ActiveDialog");
            activeDialog = false;
        }
        else
        {
            GameManager.Instance.playerInput.SwitchCurrentActionMap(inventoryCanvas.gameObject.activeSelf ? "InventoryOpen" : "InGamePlayer");
        }
        pauseMenuCanvas.gameObject.SetActive(false);
        
    }

    public void OpenInventory()
    {
        Debug.Log(GameManager.Instance.playerInput.currentActionMap.name);
        Debug.Log(GameManager.Instance.playerInput.currentActionMap.enabled);
        GameManager.Instance.playerInput.currentActionMap.Disable();
        Debug.Log(GameManager.Instance.playerInput.currentActionMap.name);
        Debug.Log(GameManager.Instance.playerInput.currentActionMap.enabled);
        GameManager.Instance.playerInput.SwitchCurrentActionMap("InventoryOpen");
        Debug.Log(GameManager.Instance.playerInput.currentActionMap.name);
        Debug.Log(GameManager.Instance.playerInput.currentActionMap.enabled);
        inventoryCanvas.gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        GameManager.Instance.playerInput.currentActionMap.Disable();
        GameManager.Instance.playerInput.SwitchCurrentActionMap("InGamePlayer");
        inventoryCanvas.gameObject.SetActive(false);
    }
}