using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuCanvas : MonoBehaviour
{
    public void ResumeGameOnClick()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ReturnToMenuOnClick()
    {
        Time.timeScale = 0;
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadSceneAsync("_MainMenu");
    }
}
