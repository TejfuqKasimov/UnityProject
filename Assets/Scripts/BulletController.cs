using UnityEngine;


public class BulletController : MonoBehaviour
{
    private Vector2 direction; // куда
    public float speed = 2; // как быстро
    private float fireRange = 10; // как далеко
    private float curDistance = 0; // сколько сейчас
    private bool doDamage; // это нью меканикс :)


    // сеттеры аргументов
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


    // столкновение -> разрушение
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (doDamage) // это нью меканикс :)
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
