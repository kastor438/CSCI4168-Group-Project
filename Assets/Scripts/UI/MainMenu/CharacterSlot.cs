using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    public CharacterClass characterClass;
    public TextMeshProUGUI characterText;

    public void SetCharacterClass(CharacterClass characterClass)
    {
        this.characterClass = characterClass;
        DisplayCharacterInfo();
    }

    public void DisplayCharacterInfo()
    {
        characterText.text = $"Name: {characterClass.characterName}\nHealth: {characterClass.maxHealth}\nSpeed: {characterClass.characterSpeed}";
    }

    public void ChooseCharacter()
    {
        MenuManager.Instance.StartGame(characterClass);
    }
}