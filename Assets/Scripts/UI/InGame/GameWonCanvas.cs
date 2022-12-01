using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonCanvas : MonoBehaviour
{
    public void ReturnToMenuOnClick()
    {
        for (int i = 0; i < GameManager.Instance.followList.Count; i++)
        {
            Destroy(GameManager.Instance.followList[i]);
        }
        GameManager.Instance.DestroyGameManager();
        SceneManager.LoadSceneAsync("_MainMenu");
    }
}