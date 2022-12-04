using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    public void openControls()
    {
        SceneManager.LoadScene(4);
    }
    public void openMainMenu()
    {
        SceneManager.LoadScene(0);
    }
} 
