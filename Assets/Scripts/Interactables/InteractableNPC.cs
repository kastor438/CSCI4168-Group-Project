using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : Interactable
{
    private bool followingPlayer;
    public NPC npc;
    public GameObject interactCanvasPopup;
    public bool canInteract;

    public override void Start()
    {
        base.Start();
        interactCanvasPopup.gameObject.SetActive(false);
    }

    public override void Update()
    {
        base.Update();
        if (followingPlayer)
            return;

        if (GameManager.Instance && GameManager.Instance.player && !followingPlayer && canInteract)
        {
            if(Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) < interactableRadius)
            {
                interactCanvasPopup.gameObject.SetActive(true);
            }
            else
            {
                interactCanvasPopup.gameObject.SetActive(false);
            }
        }


        if (interacted && GameManager.Instance && GameManager.Instance.player && 
            Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) > interactableRadius*1.5f)
        {
            GameManager.Instance.userInterface.dialogCanvas.DisableDialog();
        }
    }

    public override void Interact()
    {
        if (canInteract)
        {
            base.Interact();
            if (npc.hasDialog && npc.npcDialog.Length > 0 && !followingPlayer)
            {
                GameManager.Instance.userInterface.dialogCanvas.gameObject.SetActive(true);
                GameManager.Instance.userInterface.dialogCanvas.EnableDialog(this);
            }
            followingPlayer = true;
            interactCanvasPopup.gameObject.SetActive(false);
        }
    }
}
