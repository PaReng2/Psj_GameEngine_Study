using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Trace,
        Attack,
        RunAway
    }
    public EnemyState state = EnemyState.Idle;


    public int moveSpeed = 2;
    public float traceRange = 15f;
    public float attackRange = 6f;
    public float attackCooldown = 1.5f;

    public GameObject BulletPrefab;
    public Transform firePoint;

    private Transform player;
    
    private float lastAttackTime;

    [Header("체력")]
    public Slider enemyHpBar;
    public int maxHP = 5;
    private int curHP;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -attackCooldown;
        curHP = maxHP;
        

        
        

        // 체력바 초기화
        if (enemyHpBar != null)
        {
            enemyHpBar.maxValue = maxHP;
            enemyHpBar.value = curHP;
        }

    }

    void Update()
    {
        if (player == null) return;

        if (curHP <= 3 && state != EnemyState.RunAway)
        {
            state = EnemyState.RunAway;
        }

        float dist = Vector3.Distance(player.position, transform.position);

        switch (state)
        {
            case EnemyState.RunAway:
                if(curHP <= 3)
                    RunAway();
                break;

            case EnemyState.Idle:
                if (dist < traceRange)
                    state = EnemyState.Trace;
                break;

            case EnemyState.Trace:
                if (dist < attackRange && curHP > 3)
                    state = EnemyState.Attack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
                    break;
            
            case EnemyState.Attack:
                if (dist > attackRange)
                    state = EnemyState.Trace;
                else
                    AttackPlayer();
                break;

            
        }

        

    }

    void TracePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        if (BulletPrefab != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
            EnemyBullet eb = bullet.GetComponent<EnemyBullet>();

            if (eb != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                eb.SetDirection(dir);
            }
        }
    }

    void RunAway()
    {

            Vector3 dir = (player.position - transform.position).normalized;
            transform.position -= dir * moveSpeed * Time.deltaTime;
            transform.LookAt(player.position);
            transform.Rotate(0, 180, 0);
        
    }
    public void TakeDamage(int damage)
    {
        curHP -= damage;
        if (enemyHpBar != null)
        {
            enemyHpBar.value = curHP;
        }
        if (curHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
