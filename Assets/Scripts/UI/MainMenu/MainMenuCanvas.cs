using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : MonoBehaviour
{
    public void PlayGameOnClick()
    {
        MenuManager.Instance.menuUserInterface.characterSelectionCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenControlsOnClick()
    {
        MenuManager.Instance.menuUserInterface.characterSelectionCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
