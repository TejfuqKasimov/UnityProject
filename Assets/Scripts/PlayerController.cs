using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

// Code from practice lesson with modifications
[RequireComponent(typeof(Rigidbody2D), typeof(CollisionTouchCheck))]
public class PlayerController: MonoBehaviour
{

    Rigidbody2D rb;
    public Animator anim;
    CollisionTouchCheck colTouchCheck; // checker for moving
    SpriteRenderer m_SpriteRenderer;   // for reversing while move
    Vector2 startPosition;             // for start over
    Vector2 moveInput;                 // for move
    int isJumping = 0;                 // checker for execute double jump 
    bool IsFasingRight;                // checker for reverse
    public float moveSpeed = 300;      // for change player move speed
    public float jumpImpulse = 5;      // for change player jump impulse
    bool Alive = true;                 // for checking player life status

    // for fix multiJumping
    void IsJumping()
    {
        if (rb.linearVelocity.y == 0)
        {
            isJumping = 0;
        }
        if (rb.linearVelocity.y > 0)
        {
            isJumping = 1;
        }
        if (rb.linearVelocity.y < 0)
        {
            isJumping = 2;
        }
        anim.SetFloat("Move_Y", rb.linearVelocity.y);
    }
    
    // reverse while moving
    void Reverse() 
    {
        if (!IsFasingRight && moveInput.x < 0)
        {
            IsFasingRight = true;
        }
        else if (IsFasingRight && moveInput.x > 0)
        {
            IsFasingRight = false;
        }
        m_SpriteRenderer.flipX = IsFasingRight;

    }

    // reading data for moving
    public void onMove(InputAction.CallbackContext context) 
    {
        moveInput = context.ReadValue<Vector2>();
        anim.SetFloat("Move_X", Mathf.Abs(moveInput.x));
    }

    // modificated lesson code with fix stuck bug when button is being held
    void Move() 
    {
        if (colTouchCheck.CanMoveLeft && Input.GetAxis("Horizontal") < 0 || colTouchCheck.CanMoveRight && Input.GetAxis("Horizontal") > 0)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed * Time.fixedDeltaTime, rb.linearVelocity.y);
        }
    }

    // modificated lesson code with normal jump with gravity
    public void onJump(InputAction.CallbackContext context)
    {
        if (colTouchCheck.IsGrounded)
        {
            if (context.started && isJumping == 0)
            {
                rb.AddForce(Vector2.up * jumpImpulse);
            }
        }
        else
        {
            if (context.canceled && isJumping == 1)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            }
        }
    }

    // checker for player "death"
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Bullet"))
        {
            PlayerDead();
        }
    }
    
    //condition for player "death"
    void PlayerFalls()
    {
        if (transform.position.y <= -10)
        {
            transform.position = startPosition;
        }
    }
    

    void PlayerDead() 
    {
        Alive = false;
    }

    void PlayerResurection()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            transform.position = startPosition;
            Alive = true;
        }
    }

    void Awake()
    {   
        rb = GetComponent<Rigidbody2D>();
        colTouchCheck = GetComponent<CollisionTouchCheck>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        IsFasingRight = true;
        startPosition = transform.position;
        anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        Debug.Log(rb.linearVelocity.y);
        Debug.Log(string.Format( " Jump  {0}",isJumping));
        anim.SetBool("Alive", Alive);
        if (Alive)
        {
            Move();
            IsJumping();
            Reverse();
        }
        PlayerFalls();
        PlayerResurection();
    }
}