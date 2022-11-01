using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerInput playerInput { get; private set; }
    public UserInterface userInterface;
    public GameObject player;

    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
        DontDestroyOnLoad(Instance);

        playerInput = GetComponent<PlayerInput>();
    }
}