using UnityEngine;


public class BulletController : MonoBehaviour
{
    private Vector2 direction;      // where
    public float speed = 2;         // how fast
    private float fireRange = 10;   //haw far 
    private float curDistance = 0;  // fow much now
    private bool doDamage;          // for mechanics

    // setters for arguments
    public void setRange(float range) 
    {
        fireRange = range;
    }

    public void setDam(bool doDam)
    {
        doDamage = doDam;
    }

    public void setDir(Vector2 dir)
    {
        direction = dir;
    }

    // collision -> destroy
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (doDamage) // for mechanics
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        curDistance += speed * Time.deltaTime;

        if (curDistance >= fireRange) 
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

   

    
}
