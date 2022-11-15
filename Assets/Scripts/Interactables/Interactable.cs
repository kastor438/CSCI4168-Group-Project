using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    internal bool interacted;

    public float interactableRadius;
    
    void Start()
    {
        interacted = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.player || !GameManager.Instance.playerInput)
            return;

        if (GameManager.Instance.playerInput.currentActionMap.name.Equals("InGamePlayer") && 
            GameManager.Instance.playerInput.actions["Interact"].WasPerformedThisFrame() && 
            Vector3.Distance(GameManager.Instance.player.transform.position, transform.position) <= interactableRadius &&
            !interacted)
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
        interacted = true;
        Debug.Log("Interacting with " + gameObject.name);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactableRadius);
    }
}
