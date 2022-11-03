using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D RB2D;
    private Vector2 movementInput;
    private float characterSpeed;
    
    void Start()
    {
        characterSpeed = (GameManager.Instance && GameManager.Instance.characterClass && GameManager.Instance.characterClass.characterSpeed > 0) ? 
            GameManager.Instance.characterClass.characterSpeed : 4;

        playerInput = GameManager.Instance.playerInput;
        RB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.player || !GameManager.Instance.playerInput || GameManager.Instance.playerInput.currentActionMap.name.Equals("InGamePlayer"))
            return;

        movementInput = playerInput.actions["Movement"].ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {
        if (movementInput.magnitude > 0)
        {
            MovePlayer(movementInput);
        }
        else
        {
            // Not moving
            RB2D.velocity = Vector3.zero;
        }
    }

    public void MovePlayer(Vector2 movementInput)
    {
        RB2D.velocity = movementInput * characterSpeed;
    }
}
