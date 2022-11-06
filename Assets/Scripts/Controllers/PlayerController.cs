using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D RB2D;
    private Animator playerAnimator;
    private Vector2 movementInput;

    public Vector2 forwardVector;
    public float characterSpeed;

    public bool lookingUp { get; private set; }
    public bool lookingDown { get; private set; }
    public bool lookingRight { get; private set; }
    public bool lookingLeft { get; private set; }

    void Start()
    {
        lookingDown = true;
        if(characterSpeed == 0)
        {
            characterSpeed = 4;
        }

        playerInput = GameManager.Instance.playerInput;
        RB2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.player || !GameManager.Instance.playerInput || 
            !GameManager.Instance.playerInput.currentActionMap.name.Equals("InGamePlayer"))
        {
            movementInput = Vector3.zero;
            return;
        }
            

        movementInput = playerInput.actions["Movement"].ReadValue<Vector2>();
        playerAnimator.SetFloat("Horizontal", (movementInput.x));
        playerAnimator.SetFloat("Vertical", (movementInput.y));
        playerAnimator.SetFloat("Speed", (movementInput.sqrMagnitude));
        if (movementInput.magnitude > 0)
        {
            if (movementInput.y > 0)
            {
                lookingUp = true;
                lookingDown = false;
                lookingRight = false;
                lookingLeft = false;
                forwardVector = Vector2.up;
                playerAnimator.SetFloat("LookingUp", 1);
                playerAnimator.SetFloat("LookingDown", 0);
                playerAnimator.SetFloat("LookingRight", 0);
                playerAnimator.SetFloat("LookingLeft", 0);
            }
            else if (movementInput.y < 0)
            {
                lookingUp = false;
                lookingDown = true;
                lookingRight = false;
                lookingLeft = false;
                forwardVector = Vector2.down;
                playerAnimator.SetFloat("LookingUp", 0);
                playerAnimator.SetFloat("LookingDown", 1);
                playerAnimator.SetFloat("LookingRight", 0);
                playerAnimator.SetFloat("LookingLeft", 0);
            }
            else if (movementInput.x > 0)
            {
                lookingUp = false;
                lookingDown = false;
                lookingRight = true;
                lookingLeft = false;
                forwardVector = Vector2.right;
                playerAnimator.SetFloat("LookingUp", 0);
                playerAnimator.SetFloat("LookingDown", 0);
                playerAnimator.SetFloat("LookingRight", 1);
                playerAnimator.SetFloat("LookingLeft", 0);
            }
            else if (movementInput.x < 0)
            {
                lookingUp = false;
                lookingDown = false;
                lookingRight = false;
                lookingLeft = true;
                forwardVector = Vector2.left;
                playerAnimator.SetFloat("LookingUp", 0);
                playerAnimator.SetFloat("LookingDown", 0);
                playerAnimator.SetFloat("LookingRight", 0);
                playerAnimator.SetFloat("LookingLeft", 1);
            }
        }
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
