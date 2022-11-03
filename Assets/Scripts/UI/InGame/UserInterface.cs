using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    // All Canvases will be here.
    public PauseMenuCanvas pauseMenuCanvas;

    public void Start()
    {
        
    }

    public void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.playerInput)
            return;

        if (GameManager.Instance.playerInput.currentActionMap.name.Equals("InGamePlayer") && 
            GameManager.Instance.playerInput.actions["PauseGame"].WasPerformedThisFrame())
        {
            OpenPauseMenu();
        }
        else if(GameManager.Instance.playerInput.currentActionMap.name.Equals("PausedGame") && 
            GameManager.Instance.playerInput.actions["UnpauseGame"].WasPerformedThisFrame())
        {
            ClosePauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        GameManager.Instance.playerInput.SwitchCurrentActionMap("PasuedGame");
        pauseMenuCanvas.gameObject.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        GameManager.Instance.playerInput.SwitchCurrentActionMap("InGamePlayer");
        pauseMenuCanvas.gameObject.SetActive(false);
    }
}