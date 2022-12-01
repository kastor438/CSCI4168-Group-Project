using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePod : Interactable
{
    private Animator animator;

    public Transform playerWaitTransform;
    public GameObject graphics;
    public GameObject interactableCanvas;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.playerInput || !GameManager.Instance.playerInput.currentActionMap.name.Equals("InGamePlayer") || !GameManager.Instance.player)
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
        StartCoroutine(LoadCrewmates());
    }

    public IEnumerator LoadCrewmates()
    {
        GameManager.Instance.playerInput.SwitchCurrentActionMap("Loading");
        while (GameManager.Instance.player.transform.position != playerWaitTransform.position)
        {
            GameManager.Instance.player.transform.position = Vector3.MoveTowards(GameManager.Instance.player.transform.position, playerWaitTransform.position, (GameManager.Instance.characterClass.characterSpeed / 100));
            yield return new WaitForSeconds(0.03f);
        }

        for (int i = 1; i < GameManager.Instance.followList.Count; i++)
        {
            GameManager.Instance.followList[i].GetComponent<AIPath>().maxSpeed = 3;
            GameManager.Instance.followList[i].GetComponent<AIPath>().endReachedDistance = 0;
            GameManager.Instance.followList[i].GetComponent<AIDestinationSetter>().target = transform;
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.followList[i].GetComponent<SpriteRenderer>().sortingOrder = 3;
            GameManager.Instance.followList[i].parent = transform;
        }

        yield return new WaitForSeconds(2);

        for(int i = 0; i < GameManager.Instance.followList.Count; i++)
        {
            GameManager.Instance.followList[i].GetComponent<SpriteRenderer>().enabled = false;
        }

        GameManager.Instance.player.GetComponent<Animator>().SetFloat("Speed", 1);
        GameManager.Instance.player.GetComponent<Animator>().SetFloat("Horizontal", transform.position.x - GameManager.Instance.player.transform.position.x);
        GameManager.Instance.player.GetComponent<Animator>().SetFloat("Vertical", transform.position.y - GameManager.Instance.player.transform.position.y);
        while (GameManager.Instance.player.transform.position != transform.position)
        {
            GameManager.Instance.player.transform.position = Vector3.MoveTowards(GameManager.Instance.player.transform.position, transform.position, (GameManager.Instance.characterClass.characterSpeed / 100));
            yield return new WaitForSeconds(0.03f);
            if (Vector3.Distance(GameManager.Instance.player.transform.position, gameObject.transform.position) < 1f)
            {
                GameManager.Instance.player.transform.position = transform.position;
            }
        }
        GameManager.Instance.player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        GameManager.Instance.player.transform.parent = transform;
        Camera.main.GetComponent<CinemachineController>().SetFollow(graphics);
        GameManager.Instance.player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().SetTrigger("Launch");
        yield return new WaitForSeconds(2);
        GameManager.Instance.userInterface.gameWonCanvas.gameObject.SetActive(true);
    }
}
