using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private float oxygenUsageInterval;

    public string characterName { get; private set; }
    /// <summary>
    /// The oxygen remaining for the player, ranging from 0-100.
    /// </summary>
    public float oxygenLevel { get; private set; }
    public float playerSpeed { get; private set; }

    public override void Start()
    {
        maxHealth = maxHealth==0 ? 50 : maxHealth;
        currHealth = maxHealth;
        oxygenLevel = 100;
        oxygenUsageInterval = 3f;
        StartCoroutine(OxygenDepletion());
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        GameManager.Instance.userInterface.inGameUICanvas.UpdateUI(this);
    }

    public IEnumerator OxygenDepletion()
    {
        yield return new WaitForSeconds(oxygenUsageInterval);
        oxygenLevel = Mathf.Clamp(oxygenLevel - GameManager.Instance.characterClass.oxygenConsumptionRate, 0, 100);
        if (oxygenLevel <= 0)
        {
            GameManager.Instance.userInterface.inGameUICanvas.UpdateUI(this);
            StartCoroutine(OxygenDeath());
        }
        else
        {
            GameManager.Instance.userInterface.inGameUICanvas.UpdateUI(this);
            StartCoroutine(OxygenDepletion());
        }
    }

    public override IEnumerator GotHit()
    {
        yield return new WaitForSeconds(0);
    }

    public override IEnumerator Death()
    {
        yield return new WaitForSeconds(0);
        for (int i = 0; i < GameManager.Instance.followList.Count; i++)
        {
            Destroy(GameManager.Instance.followList[i]);
        }
        GameManager.Instance.userInterface.deathCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    public IEnumerator OxygenDeath()
    {
        yield return new WaitForSeconds(0);
        for (int i = 0; i < GameManager.Instance.followList.Count; i++)
        {
            Destroy(GameManager.Instance.followList[i]);
        }
        GameManager.Instance.userInterface.deathCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    public void SetCharacterStats(CharacterClass characterClass)
    {
        this.characterName = characterClass.characterName;
        this.maxHealth = characterClass.maxHealth;
        this.currHealth = characterClass.maxHealth;
        this.playerSpeed = characterClass.characterSpeed;
        GetComponent<PlayerController>().characterSpeed = playerSpeed;
    }

    public void HealthEffect(float effectValue)
    {
        currHealth = (int)Mathf.Clamp(currHealth + (effectValue * maxHealth), 0, maxHealth);
        GameManager.Instance.userInterface.inGameUICanvas.UpdateUI(this);
    }

    public void OxygenEffect(float effectValue)
    {
        oxygenLevel = Mathf.Clamp(oxygenLevel + (int)effectValue, 0, 100);
        GameManager.Instance.userInterface.inGameUICanvas.UpdateUI(this);
    }

    public void SpeedEffect(float effectValue, float effectTime)
    {
        playerSpeed = Mathf.Clamp(playerSpeed * effectValue, 0, 999);
        GetComponent<PlayerController>().characterSpeed = playerSpeed;
        if (effectTime > 0)
        {
            StartCoroutine(ReturnSpeedToNormal(effectValue, effectTime));
        }
    }

    public IEnumerator ReturnSpeedToNormal(float effectValue, float effectTime)
    {
        yield return new WaitForSeconds(effectTime);
        playerSpeed = Mathf.Clamp(playerSpeed / effectValue, 0, 999);
        GetComponent<PlayerController>().characterSpeed = playerSpeed;
    }
}
