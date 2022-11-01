using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D RB2D;
    private Vector2 movementInput;
    public float speed;
    
    void Start()
    {
        playerInput = GameManager.Instance.playerInput;
        RB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.player || !GameManager.Instance.playerInput)
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
        RB2D.velocity = movementInput * speed;
    }
}
