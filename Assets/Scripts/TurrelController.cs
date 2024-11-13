using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.PlayerLoop;

public class TurrelController : MonoBehaviour
{
    Vector2 turrelSize; // кто
    GameObject bulletPrefab; // чем
    Vector2 bulletSize; // каким
    Vector2 firePoint; // откуда
    Vector2 direction; // куда
    Transform player; // в кого

    public bool doDamage = true; // это нью меканикс :)
    public float fireRate = 1f; // как часто
    public float fireRange = 10f; // как далеко
    float nextFireTime = 1f; // когда еще


     
    void Shoot() //Инициализация пули с аргументами
    {
        firePoint = (Vector2)transform.position + direction;
        GameObject bullet = Instantiate(bulletPrefab, firePoint, transform.rotation);

        bullet.GetComponent<BulletController>().setDir(direction);
        bullet.GetComponent<BulletController>().setDam(doDamage);
        bullet.GetComponent<BulletController>().setRange(fireRange);
    }



    void Awake()
    {
        bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Bullet.prefab"); //Подтягивание префаба для пули
        bulletSize = bulletPrefab.GetComponent<Renderer>().bounds.size;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        turrelSize = GetComponent<Renderer>().bounds.size;
    }


    void FixedUpdate()
    {
        //Направление для туррели с учетом длины вектора для нормальной инициализации пули
        direction = (player.position - transform.position).normalized * (turrelSize.x/2 + bulletSize.x/2+0.03f);

        // Луч до игрока с игнорированием других пуль нужен чтобы у туррели не было вх)))))
        Vector2 raycastOrigin = (Vector2)transform.position  + direction;
        RaycastHit2D hit = Physics2D.Linecast(raycastOrigin, player.position, ~LayerMask.GetMask("Bullet"));

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            //Слежка за игроком
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
    
