using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

// 
[RequireComponent(typeof(Rigidbody2D), typeof(CollisionTouchCheck))]
public class PlayerController: MonoBehaviour
{

    GameObject Player;
    Rigidbody2D rb;
    public Animator anim;
    CollisionTouchCheck colTouchCheck;
    SpriteRenderer m_SpriteRenderer;
    public float Gravity = 0.3f;
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
    Vector2 pos = new Vector2(-9, 1);
    public GameObject PlayerPrefab;
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            transform.position = pos;
        }
    }

    Vector2 moveInput;
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        anim.SetFloat("Move_X", Mathf.Abs(moveInput.x));
    }

    [SerializeField]
    float jumpImpulse = 5;
    //public void onJump(InputAction.CallbackContext context)
    //{
    //    if (colTouchCheck.IsGrounded)
    //    {
    //        if (context.started)
    //        {
    //            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + jumpImpulse);
    //        }
    //    }
    //    else
    //    {
    //        if (context.canceled)
    //        {
    //            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y *0.0001f);
    //        }
    //    }
    //}

    public void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && colTouchCheck.IsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpImpulse);
        }
    }
    void Awake()
    {
        Player = GetComponent<GameObject>();
        IsFasingRight = true;
        rb = GetComponent<Rigidbody2D>();
        colTouchCheck = GetComponent<CollisionTouchCheck>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
        transform.position = new Vector2(-9, 1);
    }

    void PlayerDied()
    {
        if (transform.position.y <= -10)
        {
            transform.position = pos;
        }
    }

    private void Update()
    {
        jump();
    }
    [SerializeField]
    private float moveSpeed = 300;
    void FixedUpdate() // ������ ���� ����� ��������� � �����, �� �� �� ����� ������������ ���� � ��� �������
    {
        if (colTouchCheck.CanMoveLeft && Input.GetAxis("Horizontal") < 0 || colTouchCheck.CanMoveRight && Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("Move");
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed * Time.fixedDeltaTime, rb.linearVelocity.y);
        }


        PlayerDied();

        if (!IsFasingRight && moveInput.x < 0)
        {
            IsFasingRight = true;
        }
        else if (IsFasingRight && moveInput.x > 0)
        {
            IsFasingRight = false;
        }
        m_SpriteRenderer.flipX = _IsFacingRight;
    }
    
    
}
