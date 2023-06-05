using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpHeight;
    float velX, velY;
    Rigidbody2D rb;
    public Transform groundcheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    Animator anim;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += DisablePlayerMove;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= DisablePlayerMove;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        EnablePlayerMove();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround);
        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }

        FlipCharacter();
        Attack();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    public void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    public void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    public void Movement()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.velocity.y;
        rb.velocity = new Vector2(velX * speed, velY);

        if(rb.velocity.x != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    public void FlipCharacter()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void DisablePlayerMove()
    {
        anim.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void EnablePlayerMove()
    {
        anim.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}