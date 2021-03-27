using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFox : MonoBehaviour
{
    //Varibles Character
    private string name { get; set; }

    private int maxHeath;
    private float maxSpeed;


    //Selected Character (Samurai)
    public Character samurai;

    //Varibles
    public int currentHp;
    public float currentSpeed;
    public float damage;
    public float attackSpeed;

    public float hookCoolDown = 1.5f;
    public float jumpForce = 20;
   //Transform
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform t_Player;
    [SerializeField] private Transform g_GroundCheck;

    [SerializeField] private GameObject hookPrefab;

    [SerializeField] private LayerMask m_WhatIsGround;


    //Checers
    [SerializeField] private float groundCheckRadius;
    private float movementInputDirection;
    private float faceInputDirection = 1;
    private float inputTimer;
    private bool isMove;
    private bool isGrounded;
    private bool isFacingRight = true;

    //Hook


    //Particls & Objects
   [SerializeField] private GameObject attackParticle;


    void Start()
    {
        //Stats
        this.name = samurai.name;
        this.maxHeath = samurai.maxHealth;
        this.maxSpeed = samurai.speed;
        this.damage = samurai.damage;
        this.attackSpeed = samurai.attackSpeed;
        //Components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //t_Player = GetComponent<Transform>();

        currentHp = maxHeath;
        currentSpeed = maxSpeed;
    }

    void Update()
    {
        CheckInput();
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        MathVelocity();
        GroundCheck();
    }


    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)   //|| (amountOfJumpsLeft > 0 && !isTouchingWall)
            {
               Jump();
            }
            /*
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            } */
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 mousePos = Input.mousePosition;
            Hook(mousePos);
            Debug.Log(mousePos);
        }
    }
    private void MathVelocity()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }
    }
    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(g_GroundCheck.position, groundCheckRadius, m_WhatIsGround);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(currentSpeed * movementInputDirection, rb.velocity.y);
    }

    private void Hook(Vector2 posMouse)
    {
 
    }

    private void Flip()
    {
         //if (!isWallSliding && canFlip && !knockback)
         // {
            faceInputDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        //  }
    }
    private void UpdateAnimations()
    {
        anim.SetBool("Run", isMove);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    #region Debug
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(g_GroundCheck.position, groundCheckRadius);
    }
    #endregion
}
