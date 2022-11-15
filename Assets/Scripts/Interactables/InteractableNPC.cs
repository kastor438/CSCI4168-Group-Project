using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : Interactable
{
    public NPC npc;

    public override void Update()
    {
        base.Update();
        if (GameManager.Instance && GameManager.Instance.player && Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) > interactableRadius*1.5f)
        {
            GameManager.Instance.userInterface.dialogCanvas.DisableDialog();
        }
    }

    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.userInterface.dialogCanvas.gameObject.SetActive(true);
        GameManager.Instance.userInterface.dialogCanvas.EnableDialog(this);
    }
}
