using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionCanvas : MonoBehaviour
{
    private List<CharacterClass> characterOptions;
    private List<GameObject> characterSlots;

    public GameObject characterSlotsParent;
    public GameObject characterSlotPrefab;

    public void Start()
    {
        characterOptions = new List<CharacterClass>();
        characterSlots = new List<GameObject>();
        characterOptions.AddRange(Resources.LoadAll<CharacterClass>("ScriptableObjects/Characters/"));
        SetCharacterOptions();
    }

    public void BackToMenuOnClick()
    {
        MenuManager.Instance.menuUserInterface.mainMenuCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetCharacterOptions()
    {
        for (int i = 0; i < characterOptions.Count; i++)
        {
            GameObject newCharacterSlot = Instantiate(characterSlotPrefab, characterSlotsParent.transform);
            newCharacterSlot.GetComponent<CharacterSlot>().SetCharacterClass(characterOptions[i]);

            characterSlots.Add(newCharacterSlot);
        }
        RectTransform contentRect = characterSlotsParent.GetComponent<RectTransform>();
        contentRect.sizeDelta = new Vector2(((characterOptions.Count-1) * 1270) + ((characterOptions.Count-1) * 50), contentRect.sizeDelta.y);
    }
}
