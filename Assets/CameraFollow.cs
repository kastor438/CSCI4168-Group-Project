using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player;
    public Transform FollowTarget;
    private CinemachineVirtualCamera vcam;


    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        if (Player == null)
        {
            Player = GameManager.Instance.player;
        }
        else
        {
            FollowTarget = Player.transform;
            vcam.LookAt = FollowTarget;
            vcam.Follow = FollowTarget;
        }
    }
}
