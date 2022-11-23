using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    private Coroutine levelSetupCoroutine;
    private VideoPlayer cutscenePlayer;
    public static GameManager Instance { get; private set; }
    public PlayerInput playerInput { get; private set; }
    public CharacterClass characterClass;
    public UserInterface userInterface;
    public InventoryManager inventoryManager { get; private set; }
    public EquipmentManager equipmentManager { get; private set; }
    public GameObject player;
    public GameObject spawnPosition;
    public bool playerInteracting;


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
        playerInput.SwitchCurrentActionMap("Loading");
        userInterface.userInterfaceSetup();
        this.characterClass = characterClass;
        player = Instantiate(characterClass.characterPrefab, spawnPosition.transform.position, Quaternion.identity);
        player.GetComponent<PlayerStats>().SetCharacterStats(characterClass);
        userInterface.inGameUICanvas.UISetup(characterClass);
        if (MenuManager.Instance)
        {
            Destroy(MenuManager.Instance.gameObject);
        }
        Camera.main.GetComponent<CinemachineController>().SetFollow(player);
        levelSetupCoroutine = StartCoroutine(StartGameSetup());
    }

    public IEnumerator StartGameSetup()
    {
        cutscenePlayer = GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>();
        userInterface.loadingCanvas.gameObject.SetActive(false);
        userInterface.cutsceneCanvas.cutSceneDialogBox.SetActive(false);
        userInterface.cutsceneCanvas.gameObject.SetActive(true);
        cutscenePlayer.Play();
        yield return new WaitForSeconds((float)cutscenePlayer.length/cutscenePlayer.playbackSpeed);
        userInterface.cutsceneCanvas.EnableNarrativeDialog("CryoSleep");
    }

    public void EndCutscene()
    {
        if (levelSetupCoroutine != null)
        {
            StopCoroutine(levelSetupCoroutine);
            levelSetupCoroutine = null;
        }
        if (cutscenePlayer)
        {
            cutscenePlayer.Stop();
            cutscenePlayer = null;
        }
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
        /*VideoPlayer cutscenePlayer = GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>();
        cutscenePlayer.Play();
        yield return new WaitForSeconds((float)cutscenePlayer.length);*/
    }
}