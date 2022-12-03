using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCanvas : MonoBehaviour
{
    public void ReturnToMenuOnClick()
    {
        for (int i = 0; i < GameManager.Instance.followList.Count; i++)
        {
            Destroy(GameManager.Instance.followList[i].gameObject);
        }
        GameManager.Instance.DestroyGameManager();
        SceneManager.LoadSceneAsync("_MainMenu");
    }
}
