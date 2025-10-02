using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    private int AttackDamage;
    private PlayerShoting playerShoting;
    private Enemy enemy ;

    void Start()
    {
        playerShoting = FindObjectOfType<PlayerShoting>();
        enemy = FindObjectOfType<Enemy>();
        Destroy(gameObject, lifeTime);    
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (playerShoting.nowWeapon == 0)
        {
            AttackDamage = 3;
        }
        else if (playerShoting.nowWeapon == 1)
        {
            AttackDamage = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy.TakeDamage(AttackDamage);
            Destroy(gameObject);
        }
    }
}
