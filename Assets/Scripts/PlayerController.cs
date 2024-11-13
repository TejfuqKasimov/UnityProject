using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// код с семинара с фиксом бага
[RequireComponent(typeof(Rigidbody2D), typeof(CollisionTouchCheck))]
public class PlayerController: MonoBehaviour
{
    
    Rigidbody2D rb;
    CollisionTouchCheck colTouchCheck;
    SpriteRenderer m_SpriteRenderer;
    private bool _IsFacingRight;

    bool IsFasingRight
    {
        get
        {
            return _IsFacingRight;
        }
        set
        {
            _IsFacingRight = value;
        }
    }


    Vector2 moveInput;
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    [SerializeField]
    float jumpImpulse = 5;
    public void onJump(InputAction.CallbackContext context)
    {
        if (colTouchCheck.IsGrounded)
        {
            if (context.started)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + jumpImpulse);
            }
        }
        else
        {
            if (context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y *0.0001f);
            }
        }
    }
    void Awake()
    {
        IsFasingRight = true;
        rb = GetComponent<Rigidbody2D>();
        colTouchCheck = GetComponent<CollisionTouchCheck>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }


    [SerializeField]
    private float moveSpeed = 300;
    void FixedUpdate() // теперь если игрок упирается в стену, то он не будет воспринимать ввод в эту сторону
    {
        if (colTouchCheck.CanMoveLeft && Input.GetAxis("Horizontal") < 0 || colTouchCheck.CanMoveRight && Input.GetAxis("Horizontal") > 0)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed * Time.fixedDeltaTime, rb.linearVelocity.y);
        }
 





        if (IsFasingRight && moveInput.x < 0)
        {
            IsFasingRight = false;
        }
        else if (!IsFasingRight && moveInput.x > 0)
        {
            IsFasingRight = true;   
        }
        m_SpriteRenderer.flipX = _IsFacingRight;
    }
    
    
}
