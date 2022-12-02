using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentSystem : Interactable
{
    private bool ventOpen;
    private Animator ventAnimator;

    public GameObject ventCanvas;

    public override void Start()
    {
        base.Start();
        ventOpen = false;
        ventAnimator = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
        if (GameManager.Instance && GameManager.Instance.player && !interacted)
        {
            if (Vector3.Distance(transform.position + offset, GameManager.Instance.player.transform.position) < interactableRadius)
            {
                ventCanvas.gameObject.SetActive(true);
            }
            else
            {
                ventCanvas.gameObject.SetActive(false);
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        if (!ventOpen)
        {
            ventAnimator.SetTrigger("OpenVent");
            ventOpen = true;
            interacted = false;
        }
        else if (ventOpen)
        {
            GameManager.Instance.NextLevel();
        }
    }
}
