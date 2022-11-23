using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public void Start()
    {

    }

    public void SetFollow(GameObject followObject)
    {
        virtualCamera.Follow = followObject.transform;
    }
}