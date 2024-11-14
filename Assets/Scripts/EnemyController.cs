using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb; 
    System.Random rnd; 
    Vector2 startPosition; // for moveing around this point
    CollisionTouchCheck colTouchCheck; 

    public float speed = 2;            // how fast
    public float jumpHeight = 2;       // how high
    public int frequency = 10;         // how frequently
    public float rangeWalking = 10;    // how far horizontal
    public float rangeFlying = 10;     // how far vertical
    private float directionHor=1;
    bool IsFasingRight;                // checker for reverse
    SpriteRenderer m_SpriteRenderer;   // for reversing while move
    private float directionVert=1; 
    public bool isSaw = false;
    [Range(0, 2)]
    public int isVertical = 0;

    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        rnd = new System.Random();
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        colTouchCheck = GetComponent<CollisionTouchCheck>();

        if (isSaw) // for moving without gravity
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    void Reverse()
    {
        if (!IsFasingRight && directionHor < 0)
        {
            IsFasingRight = true;
        }
        else if (IsFasingRight && directionHor > 0)
        {
            IsFasingRight = false;
        }
        m_SpriteRenderer.flipX = IsFasingRight;
    }
    void MoveHorizontal()
    {
        transform.position += directionHor * Vector3.right * speed * Time.deltaTime;
        if ((transform.position.x - startPosition.x) >= (rangeWalking / 2))
        {
            directionHor = -speed/Mathf.Abs(speed);
        }
        if ((transform.position.x - startPosition.x) <= (-1) * (rangeWalking / 2))
        {
            directionHor =  speed / Mathf.Abs(speed);
        }
    }

    void MoveVertical()
    {
        transform.position += directionVert * Vector3.up * speed * Time.deltaTime;
        if ((transform.position.y - startPosition.y) >= (rangeFlying / 2))
        {
            directionVert = -speed / Mathf.Abs(speed);
        }
        if ((transform.position.y - startPosition.y) <= (-1) * (rangeFlying / 2))
        {
            directionVert = speed / Mathf.Abs(speed);
        }
    }


    void Jump() // for trolling gamers with random jumps
    {
        if (colTouchCheck.IsGrounded) 
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
        if (isSaw) // conditions for different types of saws
        {
            if (isVertical == 0)
            {
                MoveHorizontal();
                Reverse();
            }
            if (isVertical == 1)
            {
                MoveVertical();
            }
            if (isVertical == 2)
            {
                MoveHorizontal();
                Reverse();
                MoveVertical();
            }
        }
        else
        {
            Reverse();
            MoveHorizontal();
            Jump();
        }
    }
}