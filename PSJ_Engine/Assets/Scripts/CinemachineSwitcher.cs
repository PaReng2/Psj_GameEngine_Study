using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineFreeLook freeLookCam;
    public bool usingFreeLook = false;
    void Start()
    {
        virtualCamera.Priority = 10;
        freeLookCam.Priority = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            usingFreeLook = !usingFreeLook;
            if (usingFreeLook)
            {
                freeLookCam.Priority = 20;
                virtualCamera.Priority = 0;
            }
            else
            {
                virtualCamera.Priority = 20;
                freeLookCam.Priority = 0;
            }
        }
    }
}
