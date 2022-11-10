using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUICanvas : MonoBehaviour
{
    public Slider healthBar;
    public Slider oxygenBar;

    public void UISetup()
    {
        healthBar.maxValue = GameManager.Instance.characterClass.maxHealth;
        healthBar.value = GameManager.Instance.characterClass.maxHealth;
        oxygenBar.maxValue = 100;
        oxygenBar.value = 100;
    }

    // Update is called once per frame
    public void UpdateUI(PlayerStats playerStats)
    {
        healthBar.value = playerStats.currHealth;
        oxygenBar.value = playerStats.oxygenLevel;
    }
}
