using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUserInterface : MonoBehaviour
{
    public MainMenuCanvas mainMenuCanvas;
    public CharacterSelectionCanvas characterSelectionCanvas;

    void Start()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        characterSelectionCanvas.gameObject.SetActive(false);
    }
}
