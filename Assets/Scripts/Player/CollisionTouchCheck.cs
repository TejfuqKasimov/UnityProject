using System.Collections.Generic;
using System.Collections;
using UnityEngine;


// code from practice lesson with modifications 
public class CollisionTouchCheck : MonoBehaviour
{
    bool _IsGrounded;
    bool _CanMoveLeft;
    bool _CanMoveRight;
    BoxCollider2D collision;
    public bool IsGrounded
    {
        get { return _IsGrounded; }
        set { _IsGrounded = value; }
    }
    public bool CanMoveLeft
    {
        get { return _CanMoveLeft; }
        set { _CanMoveLeft = value; }
    }

    public bool CanMoveRight
    {
        get { return _CanMoveRight; }
        set { _CanMoveRight = value; }
    }

    void Awake()
    {
        collision = GetComponent<BoxCollider2D>();
    }

    [SerializeField]
    ContactFilter2D groundFilter;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    float groundCheckDistance = 0.01f;

    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    float wallCheckDistance = 0.01f;
    void FixedUpdate()
    {
        IsGrounded = collision.Cast(Vector2.down, groundFilter, groundHits, groundCheckDistance) > 0;
        CanMoveLeft = collision.Cast(Vector2.left, groundFilter, wallHits, wallCheckDistance) == 0;
        CanMoveRight = collision.Cast(Vector2.right, groundFilter, wallHits, wallCheckDistance) == 0;
    }


}

