using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentSystem : Interactable
{
    private bool ventOpen;
    private Animator ventAnimator;

    public override void Start()
    {
        base.Start();
        ventOpen = false;
        ventAnimator = GetComponent<Animator>();
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
