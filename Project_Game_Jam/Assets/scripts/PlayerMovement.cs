using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    /*[SerializeField]
    CharacterController characterController;*/
    [SerializeField]
    float Speed;
    public float SprintSpeed;
    public float trueSpeed;
    [SerializeField]
    float smoothTime = 0.1f;
    [SerializeField]
    float smoothVelocity;
    [SerializeField]
    Transform cam;
    [Header("Gravity And Jump")]
    [SerializeField]
    float Gravity = -7.5f;
    [SerializeField]
    Vector3 velocity;
    [SerializeField]
    float JumpSpeed = 3f;
    [SerializeField]
    Vector3 force;
    public bool isJumping;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        float horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector3 Deraction = new Vector3(horizontal, 0, Vertical).normalized;

        if (Deraction.magnitude >= 0.1f)
        {
            float targetAngel = Mathf.Atan2(Deraction.x, Deraction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angel = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angel, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * trueSpeed * Time.deltaTime);

        }



        if (!characterController.isGrounded)
        {
            velocity.y += Gravity * Time.deltaTime;
            characterController.Move(velocity);
        }

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            velocity.y = 0f * Time.deltaTime;
            characterController.Move(force);
        }
        isJumping = true;

       
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            trueSpeed = SprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            trueSpeed = Speed; ;
        }


    }
}
