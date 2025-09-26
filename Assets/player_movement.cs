using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class player_movement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
 
 
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float decrease_stamina_rate = 0.4f;
    public float increase_stamina_rate = 0.06f;
 
 
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
 
    public bool canMove = true;

    public Image running_bar;

    bool isRunning = false;
    bool canSprint = true;
    public cameraShaking cameraShakeScript;



    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        running_bar.color = new Color(0.4165183f, 0.6614364f, 0.754717f, 1);
    }
 
    void Update()
    {
 
        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            if (running_bar.fillAmount <= 0)
            {
                isRunning = false;
                canSprint = false;
            }
            else
            {
                isRunning = true;
            }
        }
        else
        {
            isRunning = false;
        }
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        if (Mathf.Abs(curSpeedX + curSpeedY) > 0)
        {
            cameraShakeScript.StartShake();
        }

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (!canSprint)
        {
            running_bar.color = new Color(0.7529412f, 0.4156863f, 0.4616551f, 1);
            if (running_bar.fillAmount > .3)
            {
                canSprint = true;
                running_bar.color = new Color(0.4165183f, 0.6614364f, 0.754717f, 1);
            }
        }
        
        if (isRunning)
        {
            Debug.Log("Running");
            running_bar.fillAmount = Mathf.Clamp01(running_bar.fillAmount - Time.deltaTime * decrease_stamina_rate);
        }
        else
        {
            running_bar.fillAmount = Mathf.Clamp01(running_bar.fillAmount + Time.deltaTime * increase_stamina_rate);
        }
 
        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
 
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
 
        #endregion
 
        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);
 
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
 
        #endregion
    }
}
