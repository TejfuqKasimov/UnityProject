using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.PlayerLoop;

public class TurrelController : MonoBehaviour
{
    Vector2 turrelSize; // who
    GameObject bulletPrefab; // with what
    Vector2 bulletSize; // what`s size
    Vector2 firePoint; // from where
    Vector2 direction; // where
    Transform player; // to who

    public bool doDamage = true; // for mechanics
    public float fireRate = 1f; // how frequently
    public float fireRange = 10f; // fow far
    float nextFireTime = 1f; // when next


     // initialization bullet with arguments
    void Shoot() 
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
        //direction for bullet way with fixing initialization bug
        direction = (player.position - transform.position).normalized * (turrelSize.x/2 + bulletSize.x/2+0.03f);

        // for turrel not have wall hack)))))
        Vector2 raycastOrigin = (Vector2)transform.position  + direction;
        RaycastHit2D hit = Physics2D.Linecast(raycastOrigin, player.position, ~LayerMask.GetMask("Bullet"));

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            //observe player
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
    
