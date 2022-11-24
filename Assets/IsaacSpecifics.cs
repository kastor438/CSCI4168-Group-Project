using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaacSpecifics : MonoBehaviour
{
    public GameObject mariaDrop;
    public GameObject arnoldDrop;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.player)
            return;

        DialogCanvas dialogCanvas = GameManager.Instance.userInterface.dialogCanvas;
        if (dialogCanvas && dialogCanvas.GetDialogArray() != null && dialogCanvas.GetDialogIndex() != 0 &&
            dialogCanvas.interactableNPC.npc.npcName.Equals("Isaac") && dialogCanvas.GetDialogIndex() == dialogCanvas.GetDialogArray().Length-1)
        {
            if (GameManager.Instance.characterClass.characterName.Equals("Maria"))
            {
                Instantiate(mariaDrop, transform.position + ((GameManager.Instance.player.transform.position - transform.position) / 2), Quaternion.identity);
            }
            else if (GameManager.Instance.characterClass.characterName.Equals("Arnold"))
            {
                Instantiate(arnoldDrop, transform.position + ((GameManager.Instance.player.transform.position - transform.position) / 2), Quaternion.identity);
            }
            this.enabled = false;
        }
    }
}
