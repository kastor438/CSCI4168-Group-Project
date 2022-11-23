using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    public MenuUserInterface menuUserInterface;

    private void Awake()
    {
        Time.timeScale = 1;
        if(Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void StartGame(CharacterClass characterClass)
    {
        StartCoroutine(GameStartWait(characterClass));
    }

    public IEnumerator GameStartWait(CharacterClass characterClass)
    {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync("Level 1", LoadSceneMode.Single);

        while (!loadingScene.isDone || GameManager.Instance == null)
        {
            yield return new WaitForSeconds(0.1f);
        }
        GameManager.Instance.SetupCharacterStats(characterClass);
    }
}
