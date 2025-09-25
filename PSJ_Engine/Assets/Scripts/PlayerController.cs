using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 5f;
    public float gravity = -9.81f;
    private CharacterController characterController;
    private Vector3 velocity;
    public bool isGrounded;

    public CinemachineVirtualCamera virtualCam;
    public float rotationSpeed = 10f;
    private CinemachinePOV pov;
    
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        pov = virtualCam.GetCinemachineComponent<CinemachinePOV>();
    }

    // Update is called once per frame
    void Update()
    {
        CinemachineSwitcher switcher = FindObjectOfType<CinemachineSwitcher>();

        isGrounded = characterController.isGrounded;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 camForward = virtualCam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = virtualCam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        if (switcher.usingFreeLook)
        {
            x = 0f;
            z = 0f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 9;
            virtualCam.m_Lens.FieldOfView = 50;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            speed = 5;
            virtualCam.m_Lens.FieldOfView = 36.3f;
        }
            Vector3 move = (camForward * z + camRight * x).normalized;
        characterController.Move(move * speed * Time.deltaTime);
        

        float cameraYaw = pov.m_HorizontalAxis.Value;
        Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpPower;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);


        
    }

   

    
}
