using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUICanvas : MonoBehaviour
{
    public Slider healthBar;
    public Slider oxygenBar;
    public Image characterImage;

    public void UISetup(CharacterClass characterClass)
    {
        healthBar.maxValue = characterClass.maxHealth;
        healthBar.value = characterClass.maxHealth;
        oxygenBar.maxValue = 100;
        oxygenBar.value = 100;
        characterImage.sprite = characterClass.characterHeadshot;
    }

    // Update is called once per frame
    public void UpdateUI(PlayerStats playerStats)
    {
        healthBar.value = playerStats.currHealth;
        oxygenBar.value = playerStats.oxygenLevel;
    }
}
