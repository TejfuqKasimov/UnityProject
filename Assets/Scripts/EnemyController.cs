using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb; // кто 
    System.Random rnd; // семя рандома
    Vector2 startPosition; // откуда
    CollisionTouchCheck colTouchCheck; // на земле?

    public float speed = 2;  //  как быстро
    public float jumpHeight = 2; // как высоко
    public int frequency = 10; // как часто
    public float rangeWalking = 10; // как далеко
    public float rangeFlying = 10; // как далеко
    private int directionHor = 1; // куда
    private int directionVert = 1; // куда
    
    public bool isSaw = false;
    [Range(0, 2)]
    public int isVertical = 1;
    
    void Awake()
    {
        rnd = new System.Random();
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        colTouchCheck = GetComponent<CollisionTouchCheck>();

        if (isSaw)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void MoveHor()
    {
        transform.position += directionHor * Vector3.right * speed * Time.deltaTime;
        if (System.Math.Abs(transform.position.x - startPosition.x) >= (rangeWalking / 2))

        {
            directionHor *= -1;
        }
    }

    void MoveVert()
    {
        transform.position += directionVert * Vector3.up * speed * Time.deltaTime;
        if (System.Math.Abs(transform.position.y - startPosition.y) >= (rangeFlying / 2))
        {
            directionVert *= -1;
        }
    }

    void Jump()
    {
        if (colTouchCheck.IsGrounded) // хехе)))))
        {
            int rnd1 = rnd.Next(frequency);
            int rnd2 = rnd.Next(frequency);

            if (rnd1 == rnd2)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + jumpHeight);
            }
        }
    }

    void FixedUpdate()
    {
        if (isSaw)
        {
            if (isVertical == 0)
            {
                MoveHor();
            }
            if (isVertical == 1)
            {
                MoveVert();
            }
            if (isVertical == 2)
            {
                MoveHor();
                MoveVert();
            }
        }
        else
        {
            MoveHor();
            Jump();
        }
    }
}