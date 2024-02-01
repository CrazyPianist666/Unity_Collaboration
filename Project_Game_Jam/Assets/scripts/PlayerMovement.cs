using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;



public class PlayerMovement : MonoBehaviour
{
    [Header("Animations")]
    public Animator animator;
    public int xAnimId;
    public int zAnimId;
    public Vector2 CurrentAnimBlendVector;
    public Vector2 animationVelocity;
    public float animationSmooth = 0.05f;
    public float animTrans = 0.15f;
    public int JumpAnim;
    public int PunchAnim;
    public int HookPuchAnim;
    [Header("PlayerMovement")]
    [SerializeField]
    float Speed;
    public CharacterController characterController;
    [Header("Gravity And Jump")]
    public float gravity = -9.81f;
    public Vector3 velocity;
    public Transform GroundCheck;
    public bool isGrounded;
    public LayerMask groundMask;
    public float groundDist = 0.4f;
    public float JumpSpeed = 3f;






    void Awake()
    {
        animator = GetComponent<Animator>();
        xAnimId = Animator.StringToHash("MoveX");
        zAnimId = Animator.StringToHash("MoveZ");
        JumpAnim = Animator.StringToHash("Jump");
        PunchAnim = Animator.StringToHash("Punching");
        HookPuchAnim = Animator.StringToHash("Hook Punch");

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
        Vector3 plmove = new Vector3(x, z, 0);

        CurrentAnimBlendVector = Vector2.SmoothDamp(CurrentAnimBlendVector, plmove, ref animationVelocity, animationSmooth);

        Vector3 Move = transform.right * CurrentAnimBlendVector.x + transform.forward * CurrentAnimBlendVector.y;
        characterController.Move(Move * Speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);



        //jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            velocity.y = Mathf.Sqrt(JumpSpeed * -2f * gravity);
            animator.CrossFade(JumpAnim, animTrans);

        }



        //Animation
        animator.SetFloat(xAnimId, Move.x);
        animator.SetFloat(zAnimId, Move.z);



        if (Input.GetButtonDown("Fire1"))
        {
            animator.CrossFade(HookPuchAnim, animTrans);
            animator.CrossFade(PunchAnim, animTrans);
        }

    }
}
