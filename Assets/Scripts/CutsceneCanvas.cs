using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneCanvas : MonoBehaviour
{
    private int dialogIndex;
    private string[] dialogArray;
    private Coroutine writingCoroutine;

    public GameObject cutSceneDialogBox;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI dialogPageText;

    public void EnableNarrativeDialog(string cutsceneDialogName)
    {
        dialogArray = Resources.Load<CutsceneDialog>("ScriptableObjects/CutsceneDialogs/" + cutsceneDialogName).dialogArray;
        dialogIndex = 0;
        dialogPageText.text = $"{dialogIndex + 1}/{dialogArray.Length}";
        writingCoroutine = StartCoroutine(WriteNarrativeText());
        cutSceneDialogBox.SetActive(true);
    }

    public void DisableDialog()
    {
        cutSceneDialogBox.SetActive(false);
        dialogArray = null;
        dialogIndex = 0;
        dialogText.text = "";
        GameManager.Instance.playerInput.SwitchCurrentActionMap("InGamePlayer");
        GameManager.Instance.userInterface.inGameUICanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.playerInput || !GameManager.Instance.playerInput.currentActionMap.name.Equals("Loading"))
            return;

        if (GameManager.Instance.playerInput.actions["ExitCutscene"].WasPerformedThisFrame())
        {
            GameManager.Instance.EndCutscene();
            DisableDialog();
        }
        else if (cutSceneDialogBox.activeSelf && GameManager.Instance.playerInput.actions["NextDialog"].WasPerformedThisFrame())
        {
            if (dialogArray.Length > dialogIndex + 1)
            {
                if (writingCoroutine != null)
                {
                    StopCoroutine(writingCoroutine);
                }

                dialogIndex++;
                dialogPageText.text = $"{dialogIndex + 1}/{dialogArray.Length}";
                writingCoroutine = StartCoroutine(WriteNarrativeText());
            }
            else
            {
                GameManager.Instance.EndCutscene();
                DisableDialog();
            }
        }
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
}
