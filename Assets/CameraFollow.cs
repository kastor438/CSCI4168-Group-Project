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
    }

    // Update is called once per frame
    void Update()
    {
        if(Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
        else
        {
            FollowTarget = Player.transform;
            vcam.LookAt = FollowTarget;
            vcam.Follow = FollowTarget;
        }
    }
}
