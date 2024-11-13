using UnityEngine;


public class BulletController : MonoBehaviour
{
    private Vector2 direction; // ����
    public float speed = 2; // ��� ������
    private float fireRange = 10; // ��� ������
    private float curDistance = 0; // ������� ������
    private bool doDamage; // ��� ��� �������� :)


    // ������� ����������
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


    // ������������ -> ����������
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (doDamage) // ��� ��� �������� :)
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
