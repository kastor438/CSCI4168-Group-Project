using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GunnerGarySpecifics : MonoBehaviour
{
    private bool savedGary;
    private bool playerNoticedGary;
    public GameObject[] enemiesToDefeat;
    public float dialogDistance;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.player)
            return;

        if (!playerNoticedGary && Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) < dialogDistance)
        {
            playerNoticedGary = true;
            StartCoroutine(NoticeGary());
        }
        for (int i = 0; i < enemiesToDefeat.Length; i++)
        {
            if (enemiesToDefeat[i] != null)
            {
                break;
            }
            else if (i == enemiesToDefeat.Length-1)
            {
                GetComponent<InteractableNPC>().canInteract = true;
                this.enabled = false;
            }
        }
    }

    public IEnumerator NoticeGary()
    {
        float currSpeed = GameManager.Instance.player.GetComponent<PlayerController>().characterSpeed;
        GameManager.Instance.player.GetComponent<PlayerController>().characterSpeed = 0;
        Camera.main.GetComponent<CinemachineController>().SetFollow(gameObject);
        GameManager.Instance.userInterface.dialogCanvas.gameObject.SetActive(true);
        GameManager.Instance.userInterface.dialogCanvas.EnableNarrativeDialog(Resources.Load<CutsceneDialog>("ScriptableObjects/CutsceneDialogs/NoticeGary").dialogArray);
        while (GameManager.Instance.userInterface.dialogCanvas.gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.2f);
        }
        Camera.main.GetComponent<CinemachineController>().SetFollow(GameManager.Instance.player);
        GameManager.Instance.player.GetComponent<PlayerController>().characterSpeed = currSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, dialogDistance);
    }
}
