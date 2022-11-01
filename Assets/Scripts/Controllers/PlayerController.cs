using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    public float speed;

    void Start()
    {
        playerInput = GameManager.Instance.playerInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.player || !GameManager.Instance.playerInput)
            return;

        Vector2 movementInput = playerInput.actions["Movement"].ReadValue<Vector2>();
        if (movementInput.magnitude > 0)
        {
            MovePlayer(movementInput);
        }
        else
        {
            // Not moving

        }
    }

    public void MovePlayer(Vector2 movementInput)
    {
        gameObject.transform.position += new Vector3(movementInput.x * speed * Time.deltaTime, movementInput.y * speed * Time.deltaTime, 0);
    }
}
