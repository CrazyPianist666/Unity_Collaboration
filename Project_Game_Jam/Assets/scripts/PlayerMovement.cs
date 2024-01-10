using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerMovement")]
    [SerializeField]
    float Speed;
    public float trueSpeed;
    public float currentSpeed;
    public CharacterController characterController;
    [Header("Gravity And Jump")]
    public float gravity = -9.81f;
    public Vector3 velocity;
    public Transform GroundCheck;
    public bool isGrounded;
    public LayerMask groundMask;
    public float groundDist = 0.4f;
    public float JumpSpeed = 3f;
    



    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

        //GroundCheck And Gravity
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDist, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;       
        }


        //PlayerMovement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 Move = transform.right * x + transform.forward * z;
        characterController.Move(Move * trueSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);


        //jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpSpeed * -2f * gravity);
        }


        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            trueSpeed = currentSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            trueSpeed = Speed;
        }

    }
}
