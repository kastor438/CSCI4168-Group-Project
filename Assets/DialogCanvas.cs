using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Pathfinding;

public class DialogCanvas : MonoBehaviour
{
    private InteractableNPC interactableNPC;
    private int dialogIndex;
    private string[] dialogArray;
    private Coroutine writingCoroutine;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI dialogPageText;

    public void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.playerInput || !GameManager.Instance.playerInput.currentActionMap.name.Equals("ActiveDialog"))
            return;

        if (GameManager.Instance.playerInput.actions["NextDialog"].WasPerformedThisFrame() && interactableNPC)
        {
            if (dialogArray.Length > dialogIndex+1)
            {
                if (writingCoroutine != null)
                {
                    StopCoroutine(writingCoroutine);
                }

                dialogIndex++;
                dialogPageText.text = $"{dialogIndex + 1}/{dialogArray.Length}";
                writingCoroutine = interactableNPC ? StartCoroutine(WriteNPCText()) : StartCoroutine(WriteNarrativeText());
            }
            else
            {
                DisableDialog();
            }
        }
    }

    public void EnableNarrativeDialog(string[] dialogArray)
    {
        this.dialogArray = dialogArray;
        dialogIndex = 0;
        dialogPageText.text = $"{dialogIndex + 1}/{dialogArray.Length}";
        writingCoroutine = StartCoroutine(WriteNarrativeText());
        dialogBox.SetActive(true);
        GameManager.Instance.playerInput.SwitchCurrentActionMap("ActiveDialog");
    }

    public void EnableDialog(InteractableNPC interactableNPC)
    {
        this.interactableNPC = interactableNPC;
        this.dialogArray = interactableNPC.npc.npcDialog;
        dialogIndex = 0;
        dialogPageText.text = $"{dialogIndex + 1}/{dialogArray.Length}";
        writingCoroutine = StartCoroutine(WriteNPCText());
        dialogBox.SetActive(true);
        GameManager.Instance.playerInput.SwitchCurrentActionMap("ActiveDialog");
    }

    public void DisableDialog()
    {
        if (interactableNPC)
        {
            interactableNPC.GetComponent<AIDestinationSetter>().target = GameManager.Instance.player.transform;
            interactableNPC.interacted = false;
            interactableNPC = null;
        }
        dialogBox.SetActive(false);
        dialogArray = null;
        dialogIndex = 0;
        dialogText.text = "";
        GameManager.Instance.playerInput.SwitchCurrentActionMap("InGamePlayer");
        gameObject.SetActive(false);
    }

    public IEnumerator WriteNarrativeText()
    {
        string currentlyWritten = "";
        string currentDialog = dialogArray[dialogIndex];
        while (currentDialog.Length > 0)
        {
            if (currentDialog.Length >= 2 && currentDialog.Substring(0, 2).Equals("\\n"))
            {
                currentlyWritten += "\n";
                dialogText.text = currentlyWritten;
                currentDialog = currentDialog[2..];
            }
            else
            {
                currentlyWritten += currentDialog.Substring(0, 1);
                dialogText.text = currentlyWritten;
                currentDialog = currentDialog[1..];
                yield return new WaitForSeconds(0.04f);
            }
        }
    }

    public IEnumerator WriteNPCText()
    {
        string currentlyWritten = "";
        string currentDialog = dialogArray[dialogIndex];
        while (currentDialog.Length > 0)
        {
            if (currentDialog.Length >= 2 && currentDialog.Substring(0, 2).Equals("\\n"))
            {
                currentlyWritten += "\n";
                dialogText.text = currentlyWritten;
                currentDialog = currentDialog[2..];
            }
            else
            {
                currentlyWritten += currentDialog.Substring(0, 1);
                dialogText.text = currentlyWritten;
                currentDialog = currentDialog[1..];
                yield return new WaitForSeconds(0.04f);
            }
        }
    }
}
