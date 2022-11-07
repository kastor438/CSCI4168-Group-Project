using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    // All Canvases will be here.
    public PauseMenuCanvas pauseMenuCanvas;
    public InventoryCanvas inventoryCanvas;

    public void Start()
    {
        pauseMenuCanvas.gameObject.SetActive(false);
        inventoryCanvas.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.playerInput)
            return;

        if (GameManager.Instance.playerInput.currentActionMap.name.Equals("InGamePlayer"))
        {
            // Check to pause game
            if(GameManager.Instance.playerInput.actions["PauseGame"].WasPerformedThisFrame()){
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
            if(GameManager.Instance.playerInput.actions["UnpauseGame"].WasPerformedThisFrame())
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
                CloseInventory();
            }
        }
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        GameManager.Instance.playerInput.SwitchCurrentActionMap("PausedGame");
        pauseMenuCanvas.gameObject.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        GameManager.Instance.playerInput.SwitchCurrentActionMap(inventoryCanvas.gameObject.activeSelf ? "InventoryOpen" : "InGamePlayer");
        pauseMenuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenInventory()
    {
        GameManager.Instance.playerInput.SwitchCurrentActionMap("InventoryOpen");
        inventoryCanvas.gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        GameManager.Instance.playerInput.SwitchCurrentActionMap("InGamePlayer");
        inventoryCanvas.gameObject.SetActive(false);
    }
}