using System;
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
    private bool sufferingRecoil;

    internal Vector3 forwardVector;
    public float characterSpeed;

    void Start()
    {
        if(characterSpeed == 0)
        {
            characterSpeed = 4;
        }

        playerInput = GameManager.Instance.playerInput;
        RB2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        forwardVector = Vector2.down;
        playerAnimator.SetFloat("Horizontal", 0);
        playerAnimator.SetFloat("Vertical", -1);
        playerAnimator.SetFloat("Speed", 0);
    }

    // Update is called once per frame
    public void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.player || !GameManager.Instance.playerInput || 
            (!GameManager.Instance.playerInput.currentActionMap.name.Equals("InGamePlayer") && !GameManager.Instance.playerInput.currentActionMap.name.Equals("ActiveDialog")))
        {
            Debug.Log("Heh?");
            movementInput = Vector3.zero;
            playerAnimator.SetFloat("Speed", (movementInput.sqrMagnitude));
            return;
        }
            

        movementInput = playerInput.actions["Movement"].ReadValue<Vector2>();
        Debug.Log(gameObject.name + " " + movementInput + " " + playerInput.actions["Movement"].ReadValue<Vector2>() + " " + playerInput.gameObject.name);
        playerAnimator.SetFloat("Horizontal", (movementInput.x));
        playerAnimator.SetFloat("Vertical", (movementInput.y));
        playerAnimator.SetFloat("Speed", (movementInput.sqrMagnitude));
        if (movementInput.magnitude > 0)
        {
            if (movementInput.y > 0)
            {
                forwardVector = Vector2.up;
                playerAnimator.SetFloat("LookingUp", 1);
                playerAnimator.SetFloat("LookingDown", 0);
                playerAnimator.SetFloat("LookingRight", 0);
                playerAnimator.SetFloat("LookingLeft", 0);
            }
            else if (movementInput.y < 0)
            {
                forwardVector = Vector2.down;
                playerAnimator.SetFloat("LookingUp", 0);
                playerAnimator.SetFloat("LookingDown", 1);
                playerAnimator.SetFloat("LookingRight", 0);
                playerAnimator.SetFloat("LookingLeft", 0);
            }
            else if (movementInput.x > 0)
            {
                forwardVector = Vector2.right;
                playerAnimator.SetFloat("LookingUp", 0);
                playerAnimator.SetFloat("LookingDown", 0);
                playerAnimator.SetFloat("LookingRight", 1);
                playerAnimator.SetFloat("LookingLeft", 0);
            }
            else if (movementInput.x < 0)
            {
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
        if (!sufferingRecoil)
        {
            if (movementInput.magnitude > 0)
            {
                MovePlayer(movementInput);
            }
            else if (RB2D.velocity.magnitude > 1f)
            {
                // Not moving
                RB2D.velocity *= 0.85f;
            }
            else
            {
                RB2D.velocity = Vector2.zero;
            }
        }
        else
        {
            if (RB2D.velocity.magnitude > 1f)
            {
                // Not moving
                RB2D.velocity *= 0.9f;
            }
            else
            {
                RB2D.velocity = Vector2.zero;
                sufferingRecoil = false;
            }
        }
    }

    public void MovePlayer(Vector2 movementInput)
    {
        RB2D.velocity = movementInput * characterSpeed;
    }

    public void RangedRecoil(Vector3 direction, float recoilAcceleration)
    {
        if (direction == Vector3.zero || recoilAcceleration == 0 || !GameManager.Instance || !GameManager.Instance.characterClass || GameManager.Instance.characterClass.characterWeight == 0)
            return;
        sufferingRecoil = true;
        RB2D.AddForce(direction * (recoilAcceleration * GameManager.Instance.characterClass.characterWeight));
    }
}
