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
        if(Instance != null)
        {
            Destroy(gameObject);
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
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync("Test-Justin", LoadSceneMode.Single);

        while (!loadingScene.isDone)
        {
            yield return new WaitForSeconds(0.1f);
        }
        while (!GameManager.Instance)
        {
            yield return new WaitForSeconds(0.1f);
        }
        GameManager.Instance.SetupCharacterStats(characterClass);
    }
}
