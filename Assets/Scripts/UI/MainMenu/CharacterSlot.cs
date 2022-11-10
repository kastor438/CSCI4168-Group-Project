using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public CharacterClass characterClass;
    public Image characterImage;
    public TextMeshProUGUI characterText;
    public Button chooseCharacterButton;

    public void SetCharacterClass(CharacterClass characterClass)
    {
        this.characterClass = characterClass;
        DisplayCharacterInfo();
    }

    public void DisplayCharacterInfo()
    {
        if (characterClass)
        {
            characterImage.sprite = characterClass.characterSprite;
            characterText.text = $"Name: {characterClass.characterName}\nHealth: {characterClass.maxHealth}\nSpeed: {characterClass.characterSpeed}";
            chooseCharacterButton.interactable = true;
        }
        else
        {
            characterImage.sprite = null;
            characterText.text = "Error...";
            chooseCharacterButton.interactable = false;
        }
    }

    public void ChooseCharacter()
    {
        MenuManager.Instance.StartGame(characterClass);
    }
}