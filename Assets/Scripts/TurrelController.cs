using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.PlayerLoop;

public class TurrelController : MonoBehaviour
{
    Vector2 turrelSize; // ���
    GameObject bulletPrefab; // ���
    Vector2 bulletSize; // �����
    Vector2 firePoint; // ������
    Vector2 direction; // ����
    Transform player; // � ����

    public bool doDamage = true; // ��� ��� �������� :)
    public float fireRate = 1f; // ��� �����
    public float fireRange = 10f; // ��� ������
    float nextFireTime = 1f; // ����� ���


     
    void Shoot() //������������� ���� � �����������
    {
        firePoint = (Vector2)transform.position + direction;
        GameObject bullet = Instantiate(bulletPrefab, firePoint, transform.rotation);

        bullet.GetComponent<BulletController>().setDir(direction);
        bullet.GetComponent<BulletController>().setDam(doDamage);
        bullet.GetComponent<BulletController>().setRange(fireRange);
    }



    void Awake()
    {
        bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Bullet.prefab"); //������������ ������� ��� ����
        bulletSize = bulletPrefab.GetComponent<Renderer>().bounds.size;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        turrelSize = GetComponent<Renderer>().bounds.size;
    }


    void FixedUpdate()
    {
        //����������� ��� ������� � ������ ����� ������� ��� ���������� ������������� ����
        direction = (player.position - transform.position).normalized * (turrelSize.x/2 + bulletSize.x/2+0.03f);

        // ��� �� ������ � �������������� ������ ���� ����� ����� � ������� �� ���� ��)))))
        Vector2 raycastOrigin = (Vector2)transform.position  + direction;
        RaycastHit2D hit = Physics2D.Linecast(raycastOrigin, player.position, ~LayerMask.GetMask("Bullet"));

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            //������ �� �������
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            
            float cur = Time.time;
            if (cur >= nextFireTime)
            {
                Shoot();
                nextFireTime = (cur + 1f) / fireRate;
            }
        }
    }
}
    
