using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerInput playerInput { get; private set; }
    public CharacterClass characterClass;
    public UserInterface userInterface;
    public InventoryManager inventoryManager { get; private set; }
    public EquipmentManager equipmentManager { get; private set; }
    public GameObject player;
    public GameObject spawnPosition;

    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(Instance);

        inventoryManager = GetComponent<InventoryManager>();
        equipmentManager = GetComponent<EquipmentManager>();
        playerInput = GetComponent<PlayerInput>();
    }

    internal void DestroyGameManager()
    {
        Instance = null;
        Destroy(userInterface.gameObject);
        Destroy(gameObject);
    }

    public void SetupCharacterStats(CharacterClass characterClass)
    {
        this.characterClass = characterClass;
        player = Instantiate(characterClass.characterPrefab, spawnPosition.transform.position, Quaternion.identity);
        player.GetComponent<PlayerStats>().SetCharacterStats(characterClass);
        userInterface.inGameUICanvas.UISetup(characterClass);
        if (MenuManager.Instance)
        {
            Destroy(MenuManager.Instance.gameObject);
        }
        Camera.main.GetComponent<CinemachineController>().SetFollow(player);
    }

    public void NextLevel()
    {
        StartCoroutine(WaitNextLevel());
    }

    public IEnumerator WaitNextLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int.TryParse(sceneName.Substring(6), out int levelNumber);
        levelNumber++;
        AsyncOperation loadedLevel =  SceneManager.LoadSceneAsync("Level " + levelNumber, LoadSceneMode.Single);

        while (!loadedLevel.isDone)
        {
            yield return new WaitForSeconds(0.05f);
        }
        spawnPosition = GameObject.Find("SpawnPosition");

        player = Instantiate(characterClass.characterPrefab, spawnPosition.transform.position, Quaternion.identity);
    }
}