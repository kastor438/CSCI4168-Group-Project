using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePod : Interactable
{
    private Animator animator;

    public GameObject interactableCanvas;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.playerInput || !GameManager.Instance.playerInput.currentActionMap.name.Equals("InGamePlayer"))
            return;

        if(Vector3.Distance(GameManager.Instance.player.transform.position, transform.position) <= interactableRadius)
        {
            interactableCanvas.SetActive(true);
        }
        else
        {
            interactableCanvas.SetActive(false);
        }

        base.Update();
    }

    public override void Interact()
    {
        base.Interact();

    }

    public IEnumerator LaunchPod()
    {
        yield return new WaitForEndOfFrame();
    }
}
